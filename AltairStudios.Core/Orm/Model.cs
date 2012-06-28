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
			List<string> primaryFields = new ModelList<string>();
			List<string> indexFields = new ModelList<string>();
			ModelList<PropertyInfo> primaryKeys = new ModelList<PropertyInfo>();
			ModelList<PropertyInfo> indexes = new ModelList<PropertyInfo>();
			
			for(int i = 0; i < properties.Length; i++) {
				if(properties[i].GetValue(this, null) != null) {
					TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
					if(attributes.Length > 0 && attributes[0].Templatize && !Reflection.Instance.isChildOf(properties[i].PropertyType, typeof(Model))) {
						parameters.Add(properties[i]);
					}
					
					PrimaryKeyAttribute[] attributesPrimaryKey = (PrimaryKeyAttribute[])properties[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
					if(attributesPrimaryKey.Length > 0 && ((attributesPrimaryKey[0].AutoIncrement && (int)properties[i].GetValue(this, null) != 0) || (attributesPrimaryKey[0].AutoIncrement == false && properties[i].GetValue(this, null) != ""))) {
						primaryKeys.Add(properties[i]);
						primaryFields.Add(properties[i].Name);
					}
					
					IndexAttribute[] attributesIndex = (IndexAttribute[])properties[i].GetCustomAttributes(typeof(IndexAttribute), true);
					if(attributesIndex.Length > 0 && properties[i].GetValue(this, null) != "") {
						indexes.Add(properties[i]);
						indexFields.Add(properties[i].Name);
					}
				}
			}
			
			IDbCommand command = SqlProvider.getProvider().createCommand();
			
			if(primaryKeys.Count > 0) {
				command.CommandText = SqlProvider.getProvider().sqlString(this, type, fields, primaryKeys.ToArray());
			
				for(int i = 0; i < primaryKeys.Count; i++) {
					IDbDataParameter parameter = SqlProvider.getProvider().createParameter();
					parameter.ParameterName = parameters[i].Name;
					parameter.Value = parameters[i].GetValue(this, null);
					
					command.Parameters.Add(parameter);
				}
			} else if(indexes.Count > 0) {
				command.CommandText = SqlProvider.getProvider().sqlString(this, type, fields, indexes.ToArray());
			
				for(int i = 0; i < indexes.Count; i++) {
					IDbDataParameter parameter = SqlProvider.getProvider().createParameter();
					parameter.ParameterName = parameters[i].Name;
					parameter.Value = parameters[i].GetValue(this, null);
					
					command.Parameters.Add(parameter);
				}
			} else {
				command.CommandText = SqlProvider.getProvider().sqlString(this, type, fields, parameters.ToArray());
			
				for(int i = 0; i < parameters.Count; i++) {
					IDbDataParameter parameter = SqlProvider.getProvider().createParameter();
					parameter.ParameterName = parameters[i].Name;
					parameter.Value = parameters[i].GetValue(this, null);
					
					command.Parameters.Add(parameter);
				}
			}
			
			IDataReader reader = command.ExecuteReader();
			ModelList<T> models = new ModelList<T>();
			ConstructorInfo constructor = type.GetConstructor(new Type[0]);
			int counter = 0;
			
			while(reader.Read()) {
				object instance = constructor.Invoke(new Object[0]);
				
				for(int i = 0; i < parameters.Count; i++) {	
					if(reader[parameters[i].Name] != DBNull.Value) {
						PropertyInfo property = type.GetProperty(parameters[i].Name);
						
						if(parameters[i].GetValue(this, null).GetType() == typeof(double)) {
							property.SetValue(instance, double.Parse(reader[parameters[i].Name].ToString()), null);
						} else {
							property.SetValue(instance, reader[parameters[i].Name], null);
						}
					}
				
				}
				
				models.Add(this.cast<T>(instance));
			}
			
			reader.Close();
			command.Connection.Close();
			
			return models;
		}
		
		
		
		public ModelList<T> getByPk<T>() {
			return null;
		}
		
		
		public ModelList<T> getByIndex<T>() {
			return null;
		}
		
		
		
		/// <summary>
		/// Insert this model instance into database.
		/// </summary>
		public long insert() {
			PropertyInfo[] properties = this.GetType().GetProperties();
			ModelList<PropertyInfo> parameters = new ModelList<PropertyInfo>();
			Dictionary<string, Dictionary<long, Model>> ids = new Dictionary<string, Dictionary<long, Model>>();
			
			for(int i = 0; i < properties.Length; i++) {
				if(properties[i].GetValue(this, null) != null) {
					TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
					PrimaryKeyAttribute[] primarys = (PrimaryKeyAttribute[])properties[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
					IndexAttribute[] indexes = (IndexAttribute[])properties[i].GetCustomAttributes(typeof(IndexAttribute), true);
					
					if((primarys.Length > 0 && !primarys[0].AutoIncrement) || (primarys.Length == 0 && attributes.Length > 0)) {
						if(Reflection.Instance.isChildOf(properties[i].PropertyType, typeof(Model))) {
							long subId = ((Model)properties[i].GetValue(this, null)).insert();
							string name = properties[i].PropertyType.ToString();
							
							if(!ids.ContainsKey(name)) {
								ids.Add(name, new Dictionary<long, Model>());
							}
							
							ids[name].Add(subId, ((Model)properties[i].GetValue(this, null)));
						} else {
							parameters.Add(properties[i]);
						}
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
			
			long id = (long)command.ExecuteScalar();
			
			foreach(string key in ids.Keys) {
 				foreach(long longId in ids[key].Keys) {
					Dictionary<string, object> sqlParams = new Dictionary<string, object>();
					PropertyInfo[] properties1 = this.GetType().GetProperties();
					PropertyInfo[] properties2 = ids[key][longId].GetType().GetProperties();
					
					for(int i = 0; i < properties1.Length; i++) {
						PrimaryKeyAttribute[] primaryKeys = (PrimaryKeyAttribute[])properties1[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
						
						if(primaryKeys.Length > 0) {
							sqlParams.Add(this.GetType().Name + "_" + properties1[i].Name, id);
						}
					}
					
					for(int i = 0; i < properties2.Length; i++) {
						PrimaryKeyAttribute[] primaryKeys = (PrimaryKeyAttribute[])properties2[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
						
						if(primaryKeys.Length > 0) {
							sqlParams.Add(ids[key][longId].GetType().Name + "_" + properties2[i].Name, longId);
						}
					}
					
					this.query(SqlProvider.getProvider().sqlInsertForeign(this.GetType(), ids[key][longId].GetType()), sqlParams);
				}
			}
			
			return id;
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
		
		
		
		public List<Dictionary<string, string>> query(string sql) {
			return this.query(sql, null);
		}
		
		
		/// <summary>
		/// Query the specified sql.
		/// </summary>
		/// <param name='sql'>
		/// Sql.
		/// </param>
		public List<Dictionary<string, string>> query(string sql, Dictionary<string, object> parameters) {
			IDbCommand command = SqlProvider.getProvider().createCommand();
			command.CommandText = sql;
			
			if(parameters != null) {
				foreach(string key in parameters.Keys) {
					IDbDataParameter parameter = SqlProvider.getProvider().createParameter();
					parameter.ParameterName = "@" + key;
					parameter.Value = parameters[key];
				
					command.Parameters.Add(parameter);
				}
			}
			
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
		/*public override string ToJson() {
			PropertyInfo[] properties = this.GetType().GetProperties();
			StringBuilder json = new StringBuilder();
			ModelList<string> jsonProperties = new ModelList<string>();
			StringConverter converter = new StringConverter();
			
			json.Append("{");
			
			for(int i = 0; i < properties.Length; i++) {
				if(properties[i].GetValue(this, null) != null) {
					TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
					if(attributes.Length > 0 && attributes[0].Templatize) {*/
						/*if(attributes[0].IsSubtable) {
							jsonProperties.Add("\"" + properties[i].Name + "\"" + ":" + (this.castList<Model>(properties[i].GetValue(this, null))).ToJson());
						} else if(attributes[0].IsSubtable) {
							jsonProperties.Add("\"" + properties[i].Name + "\"" + ":" + this.cast<Model>(properties[i].GetValue(this, null)).ToJson());
						} else {
							jsonProperties.Add("\"" + properties[i].Name + "\"" + ":" + converter.convert(properties[i].GetValue(this, null), properties[i].PropertyType));						
						}*/
					/*}
				}
			}
			
			json.Append(string.Join(",", jsonProperties.ToArray()));
			json.Append("}");
			
			return json.ToString();
		}*/
	}
}