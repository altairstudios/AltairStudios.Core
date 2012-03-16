using System;
using System.Reflection;
using System.Text;
using MySql.Data.MySqlClient;
using AltairStudios.Core.Mvc;
using AltairStudios.Core.Orm.Models;
using AltairStudios.Core.Util;


namespace AltairStudios.Core.Orm {
	public class Model : AltairStudios.Core.Mvc.Model {
		public ModelList<T> getBy<T>() {
			Type type = this.GetType();
			PropertyInfo[] properties = this.GetType().GetProperties();
			StringBuilder sql = new StringBuilder();
			ModelList<PropertyInfo> parameters = new ModelList<PropertyInfo>();
			AltairStudios.Core.Mvc.ModelList<string> fields = this.getFields(properties);
			
			sql.Append("SELECT " + string.Join(",", fields.ToArray()) + " FROM " + type.Name + " WHERE ");
			
			for(int i = 0; i < properties.Length; i++) {
				if(properties[i].GetValue(this, null) != null && properties[i].PropertyType.ToString() == "System.String") {
					//sql.Append(properties[i].Name + " = '" + properties[i].GetValue(this, null) + "' AND " );
					
					TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
					if(attributes.Length > 0 && attributes[0].Templatize) {
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
			ModelList<T> models = new ModelList<T>();
			ConstructorInfo constructor = type.GetConstructor(new Type[0]);
			int counter = 0;
			
			while(reader.Read()) {
				object instance = constructor.Invoke(new Object[0]);
				
				counter = 0;
				for(int i = 0; i < properties.Length; i++) {	
					TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
					if(attributes.Length > 0 && attributes[0].Templatize) {
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
			ModelList<PropertyInfo> parameters = new ModelList<PropertyInfo>();
			
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
		
		
		public string ToJson() {
			PropertyInfo[] properties = this.GetType().GetProperties();
			StringBuilder json = new StringBuilder();
			ModelList<string> jsonProperties = new ModelList<string>();
			StringConverter converter = new StringConverter();
			
			json.Append("{");
			
			for(int i = 0; i < properties.Length; i++) {
				if(properties[i].GetValue(this, null) != null) {
					TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
					if(attributes.Length > 0 && attributes[0].Templatize) {
						if(attributes[0].IsSubtable && attributes[0].IsList) {
							//Type listType = properties[i].PropertyType;
							//listType.MakeGenericType
							//properties[i].PropertyType
							//ModelList<Model> model = ((ModelList<Model>)this.cast<System.Collections.Generic.IList>()).ToJson()
							//jsonProperties.Add("\"" + properties[i].Name + "\"" + ":" + ((ModelList<Model>)this.cast<System.Collections.Generic.IList>(properties[i].GetValue(this, null))).ToJson());
							
							//ModelList<string> model = new ModelList<string>();
							//model.ConvertAll<Model>(x => x.ToJson());
							//((System.Collections.Generic.IList)this.cast<System.Collections.Generic.IList>(properties[i].GetValue(this, null))).ConvertAll()
							//((System.Collections.Generic.IList<Model>)properties[i].GetValue(this, null)).ConvertAll<Model>(x => x.ToJson());
							 //jsonProperties.Add((this.castList<Model>(properties[i].GetValue(this, null))).ToJson());
							//jsonProperties.Add("\"" + properties[i].Name + "\"" + ":" + ((ModelList<Model>)this.castList<Model>(properties[i].GetValue(this, null))).ToJson());
							jsonProperties.Add("\"" + properties[i].Name + "\"" + ":" + (this.castList<Model>(properties[i].GetValue(this, null))).ToJson());
						} else if(attributes[0].IsSubtable && !attributes[0].IsList) {
							jsonProperties.Add("\"" + properties[i].Name + "\"" + ":" + this.cast<Model>(properties[i].GetValue(this, null)).ToJson());
						} else {
							jsonProperties.Add("\"" + properties[i].Name + "\"" + ":" + converter.convert(properties[i].GetValue(this, null), properties[i].PropertyType));						
						}
					}
				}
			}
			
			json.Append(string.Join(",", jsonProperties.ToArray()));
			json.Append("}");
			
			return json.ToString();
		}
	}
}