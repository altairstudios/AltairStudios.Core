using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using MySql.Data.MySqlClient;


namespace AltairStudios.Core.Orm {
	public class Model {
		public List<T> getBy<T>() {
			Type type = this.GetType();
			PropertyInfo[] properties = this.GetType().GetProperties();
			StringBuilder sql = new StringBuilder();
			List<PropertyInfo> parameters = new List<PropertyInfo>();
			string fields = this.getFields(properties);
			
			sql.Append("SELECT " + fields + " FROM " + type.Name + " WHERE ");
			
			for(int i = 0; i < properties.Length; i++) {
				if(properties[i].GetValue(this, null) != null && properties[i].PropertyType.ToString() == "System.String") {
					//sql.Append(properties[i].Name + " = '" + properties[i].GetValue(this, null) + "' AND " );
					
					TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
					if(attributes[0].Templatize) {
						sql.Append(properties[i].Name + " = @" + properties[i].Name + " AND " );
						parameters.Add(properties[i]);
					}
				}
			}
			
			sql.Append("1 = 1");
			
			MySqlCommand command = ConnectionFactory.createCommand();
			command.CommandText = sql.ToString();
			
			for(int i = 0; i < parameters.Count; i++) {
				command.Parameters.Add(parameters[i].Name, ConnectionFactory.resolveType(properties[i].PropertyType)).Value = parameters[i].GetValue(this, null);
			}
			
			MySqlDataReader reader = command.ExecuteReader();
			List<T> models = new List<T>();
			ConstructorInfo constructor = type.GetConstructor(new Type[0]);
			int counter = 0;
			
			while(reader.Read()) {
				object instance = constructor.Invoke(new Object[0]);
				
				counter = 0;
				for(int i = 0; i < properties.Length; i++) {	
					TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
					if(attributes[0].Templatize) {
						type.GetProperty(properties[i].Name).SetValue(instance, reader[counter], null);
						counter++;
					}
				}
				
				models.Add(this.cast<T>(instance));
			}
			
			reader.Close();
			command.Connection.Close();
			
			return models;
		}
		
		
		public string save() {
			Type type = this.GetType();
			PropertyInfo[] properties = this.GetType().GetProperties();
			StringBuilder sql = new StringBuilder();
			List<PropertyInfo> parameters = new List<PropertyInfo>();
			
			sql.Append("UPDATE " + type.Name + " SET ");
			
			for(int i = 0; i < properties.Length; i++) {
				if(properties[i].PropertyType.ToString() == "System.String") {					
					TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);					
					if(attributes.Length > 0 && attributes[0].Templatize) {
						sql.Append(properties[i].Name + " = @" + properties[i].Name + ", " );
						parameters.Add(properties[i]);
					}
				}
			}
			
			sql.Append("1 = 1");
			sql.Append(" WHERE id = @Id");
			
			MySqlCommand command = ConnectionFactory.createCommand();
			command.CommandText = sql.ToString();
			
			for(int i = 0; i < parameters.Count; i++) {
				command.Parameters.Add(parameters[i].Name, ConnectionFactory.resolveType(properties[i].PropertyType)).Value = parameters[i].GetValue(this, null);
			}
			
			//MySqlDataReader reader = command.ExecuteReader();
			return sql.ToString();
		}
		
		
		
		protected string getFields(PropertyInfo[] properties) {
			StringBuilder fields = new StringBuilder();
			
			for(int i = 0; i < properties.Length; i++) {	
				TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
				if(attributes[0].Templatize) {
					fields.Append(properties[i].Name + ", ");
				}
			}
			
			return fields.ToString().Substring(0, fields.Length - 2);
		}
		
				
		public T cast<T>(object o) {
			return (T)o;
		}
	}
}