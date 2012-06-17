using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using AltairStudios.Core.Mvc;
using AltairStudios.Core.Orm.Models;
using AltairStudios.Core.Orm.Providers;
using AltairStudios.Core.Util;


namespace AltairStudios.Core.Orm {
	/// <summary>
	/// Model.
	/// </summary>
	public class Model : AltairStudios.Core.Mvc.Model {
		/// <summary>
		/// Gets the by.
		/// </summary>
		/// <returns>
		/// The by.
		/// </returns>
		/// <typeparam name='T'>
		/// The 1st type parameter.
		/// </typeparam>
		public ModelList<T> getBy<T>() {
			Type type = this.GetType();
			PropertyInfo[] properties = this.GetType().GetProperties();
			ModelList<PropertyInfo> parameters = new ModelList<PropertyInfo>();
			List<string> fields = this.getFields(properties);
			ModelList<PropertyInfo> primaryKeys = new ModelList<PropertyInfo>();
			ModelList<PropertyInfo> indexes = new ModelList<PropertyInfo>();
			
			for(int i = 0; i < properties.Length; i++) {
				if(properties[i].GetValue(this, null) != null && properties[i].PropertyType.ToString() == "System.String") {
					TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
					if(attributes.Length > 0 && attributes[0].Templatize) {
						parameters.Add(properties[i]);
					}
					
					PrimaryKeyAttribute[] attributesPrimaryKey = (PrimaryKeyAttribute[])properties[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
					if(attributesPrimaryKey.Length > 0) {
						primaryKeys.Add(properties[i]);
					}
					
					IndexAttribute[] attributesIndex = (IndexAttribute[])properties[i].GetCustomAttributes(typeof(IndexAttribute), true);
					if(attributesPrimaryKey.Length > 0) {
						indexes.Add(properties[i]);
					}
				}
			}
			
			if(primaryKeys.Count > 0) {
				for(int i = 0; i < primaryKeys.Count; i++) {
					//primaryKeys[0].PropertyType.
				}
			}
			
			IDbCommand command = SqlProvider.getProvider().createCommand();
			command.CommandText = SqlProvider.getProvider().sqlString(this, type, fields, properties);
			
			for(int i = 0; i < parameters.Count; i++) {
				IDbDataParameter parameter = SqlProvider.getProvider().createParameter();
				parameter.ParameterName = parameters[i].Name;
				parameter.Value = parameters[i].GetValue(this, null);
				
				command.Parameters.Add(parameter);
			}
			
			IDataReader reader = command.ExecuteReader();
			ModelList<T> models = new ModelList<T>();
			ConstructorInfo constructor = type.GetConstructor(new Type[0]);
			int counter = 0;
			
			while(reader.Read()) {
				object instance = constructor.Invoke(new Object[0]);
				
				counter = 0;
				for(int i = 0; i < properties.Length; i++) {	
					TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
					if(attributes.Length > 0 && attributes[0].Templatize) {
						if(reader[counter] != DBNull.Value) {
							type.GetProperty(properties[i].Name).SetValue(instance, reader[counter], null);
						}
						counter++;
					}
				}
				
				models.Add(this.cast<T>(instance));
			}
			
			reader.Close();
			command.Connection.Close();
			
			return models;
		}
		
		
		
		/*public ModelList<T> getByPk<T>() {
			
		}
		
		
		public ModelList<T> getByIndex<T>() {
			
		}*/
		
		
		
		/// <summary>
		/// Insert this model instance into database.
		/// </summary>
		public void insert() {
			PropertyInfo[] properties = this.GetType().GetProperties();
			ModelList<PropertyInfo> parameters = new ModelList<PropertyInfo>();
			
			for(int i = 0; i < properties.Length; i++) {
				if(properties[i].GetValue(this, null) != null && (properties[i].PropertyType.ToString() == "System.String" || properties[i].PropertyType.ToString() == "System.Int32" || properties[i].PropertyType.ToString() == "System.Double" || properties[i].PropertyType.ToString() == "System.Decimal")) {
					TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
					if(attributes.Length > 0 && attributes[0].Templatize) {
						parameters.Add(properties[i]);
					}
				}
			}
			
			IDbCommand command = SqlProvider.getProvider().createCommand();
			command.CommandText = SqlProvider.getProvider().sqlInsert(this.GetType());
			
			for(int i = 0; i < parameters.Count; i++) {
				IDbDataParameter parameter = SqlProvider.getProvider().createParameter();
				parameter.ParameterName = parameters[i].Name;
				parameter.Value = parameters[i].GetValue(this, null);
				
				command.Parameters.Add(parameter);
			}
			
			command.ExecuteNonQuery();
		}
		
		
		/*public void save() {
			Type type = this.GetType();
			PropertyInfo[] properties = this.GetType().GetProperties();
		}*/
		
		
		
		/// <summary>
		/// Save this instance.
		/// </summary>
		/*public string save() {
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
			
			return sql.ToString();
		}*/
		
		
		
		/// <summary>
		/// Creates the table.
		/// </summary>
		/// <returns>
		/// The table.
		/// </returns>
		public string createTable() {
			return SqlProvider.getProvider().sqlCreateTable(this.GetType());
		}
		
		
		
		/// <summary>
		/// Query the specified sql.
		/// </summary>
		/// <param name='sql'>
		/// Sql.
		/// </param>
		public List<Dictionary<string, string>> query(string sql) {
			IDbCommand command = SqlProvider.getProvider().createCommand();
			command.CommandText = sql;
			IDataReader reader = command.ExecuteReader();
			
			List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
			
			while(reader.Read()) {
				Dictionary<string, string> row = new Dictionary<string, string>();
				
				for(int i = 0; i < reader.FieldCount; i++) {
					row.Add(reader.GetName(i), reader[i].ToString());
				}
				
				result.Add(row);
			}
			
			reader.Close();
			command.Connection.Close();
			
			return result;
		}
		
		
		
		/// <summary>
		///  Tos the json. 
		/// </summary>
		/// <returns>
		///  The json. 
		/// </returns>
		public override string ToJson() {
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