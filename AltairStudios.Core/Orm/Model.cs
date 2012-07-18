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
		
		public ModelList<T> getBy<T>() {
			return this.getBy<T>(false);
		}
		
		
		public ModelList<T> getBy<T>(bool subqueries) {
			Type type = this.GetType();
			PropertyInfo[] properties = this.GetType().GetProperties();
			ModelList<PropertyInfo> parameters = new ModelList<PropertyInfo>();
			List<string> fields = this.getFields(properties);
			List<string> primaryFields = new ModelList<string>();
			List<string> indexFields = new ModelList<string>();
			ModelList<PropertyInfo> primaryKeys = new ModelList<PropertyInfo>();
			ModelList<PropertyInfo> indexes = new ModelList<PropertyInfo>();
			ModelList<PropertyInfo> fieldParams = new ModelList<PropertyInfo>();
			ModelList<PropertyInfo> sublistParams = new ModelList<PropertyInfo>();
			
			for(int i = 0; i < properties.Length; i++) {
				TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
				
				if(attributes.Length > 0 && properties[i].PropertyType.GetInterface("IModelizable") == null && properties[i].PropertyType.GetInterface("IList") == null) {
					fieldParams.Add(properties[i]);
				} else {
					sublistParams.Add(properties[i]);
				}
				
				if(properties[i].GetValue(this, null) != null) {
					if(attributes.Length > 0 && properties[i].PropertyType.GetInterface("IModelizable") == null && properties[i].PropertyType.GetInterface("IList") == null) {
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
					parameter.ParameterName = primaryKeys[i].Name;
					parameter.Value = primaryKeys[i].GetValue(this, null);
					
					command.Parameters.Add(parameter);
				}
			} else if(indexes.Count > 0) {
				command.CommandText = SqlProvider.getProvider().sqlString(this, type, fields, indexes.ToArray());
			
				for(int i = 0; i < indexes.Count; i++) {
					IDbDataParameter parameter = SqlProvider.getProvider().createParameter();
					parameter.ParameterName = indexes[i].Name;
					parameter.Value = indexes[i].GetValue(this, null);
					
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
			
			while(reader.Read()) {
				object instance = constructor.Invoke(new Object[0]);
				
				for(int i = 0; i < fieldParams.Count; i++) {	
					if(reader[fieldParams[i].Name] != DBNull.Value) {
						PropertyInfo property = type.GetProperty(fieldParams[i].Name);
						
						if(fieldParams[i].GetValue(this, null) != null && fieldParams[i].GetValue(this, null).GetType() == typeof(double)) {
							property.SetValue(instance, double.Parse(reader[fieldParams[i].Name].ToString()), null);
						} else {
							property.SetValue(instance, reader[fieldParams[i].Name], null);
						}
					}
				
				}
				
				models.Add(this.cast<T>(instance));
			}
			
			reader.Close();
			
			if(subqueries) {
				for(int i = 0; i < models.Count; i++) {
					for(int j = 0; j < sublistParams.Count; j++) {
						if(sublistParams[j].PropertyType.GetInterface("IModelizable") != null && sublistParams[j].PropertyType.GetInterface("IList") == null) {
							//Modelo simple
							//models[i].GetType().GetProperty(sublistParams[j].Name).SetValue(models[i], this.getByForeignModel<IModelizable>(primaryKeys[0], sublistParams[j])[0], null);
							models[i].GetType().GetProperty(sublistParams[j].Name).SetValue(models[i], this.GetType().GetMethod("getByForeignModel").MakeGenericMethod(sublistParams[j].PropertyType).Invoke(this, new object[]{primaryKeys[0], sublistParams[j]}), null);
						} else if(sublistParams[j].PropertyType.GetInterface("IModelizable") != null && sublistParams[j].PropertyType.GetInterface("IList") != null && sublistParams[j].PropertyType.GetGenericArguments()[0].GetInterface("IModelizable") != null) {
							//Model list
							//models[i].GetType().GetProperty(sublistParams[j].Name).SetValue(models[i], this.getByForeignModel<IModelizable>(primaryKeys[0], sublistParams[j]), null);
							models[i].GetType().GetProperty(sublistParams[j].Name).SetValue(models[i], this.GetType().GetMethod("getByForeignModels").MakeGenericMethod(sublistParams[j].PropertyType.GetGenericArguments()[0]).Invoke(this, new object[]{primaryKeys[0], sublistParams[j]}), null);
						} else if(sublistParams[j].PropertyType.GetInterface("IModelizable") != null && sublistParams[j].PropertyType.GetInterface("IList") != null && sublistParams[j].PropertyType.GetGenericArguments()[0].GetInterface("IModelizable") == null) {
							models[i].GetType().GetProperty(sublistParams[j].Name).SetValue(models[i], this.GetType().GetMethod("getByForeignSimple").MakeGenericMethod(sublistParams[j].PropertyType.GetGenericArguments()[0]).Invoke(this, new object[]{primaryKeys[0], sublistParams[j]}), null);
						} else {
							//Lista simple
							/*Type ctype = this.GetType();
							MethodInfo meth = ctype.GetMethod("getByForeignSimple");
							MethodInfo meth2 = meth.MakeGenericMethod(sublistParams[j].PropertyType.GetGenericArguments()[0]);
							
							object res = meth2.Invoke(this, new object[]{primaryKeys[0], sublistParams[j]});
							
							PropertyInfo prop = models[i].GetType().GetProperty(sublistParams[j].Name);
							prop.SetValue(models[i], res, null);
							*/
							models[i].GetType().GetProperty(sublistParams[j].Name).SetValue(models[i], this.GetType().GetMethod("getByForeignSimple").MakeGenericMethod(sublistParams[j].PropertyType.GetGenericArguments()[0]).Invoke(this, new object[]{primaryKeys[0], sublistParams[j]}), null);
						}
					}
				}
			}
			
			command.Connection.Close();
			
			return models;
		}
		
		
		
		public ModelList<T> getByForeignSimple<T>(PropertyInfo primary, PropertyInfo type) {
			ModelList<T> result = new ModelList<T>();
			List<string> fields = new List<string>();
			fields.Add(type.Name);
			
			List<string> conditions = new List<string>();
			conditions.Add(this.GetType().Name + "_" + primary.Name);
			
			
			IDbCommand command = SqlProvider.getProvider().createCommand();
			command.CommandText = SqlProvider.getProvider().sqlString(fields, this.GetType().Name + "_" + type.Name, conditions);
			
			IDbDataParameter parameter = SqlProvider.getProvider().createParameter();
			parameter.ParameterName = this.GetType().Name + "_" + primary.Name;
			parameter.Value = primary.GetValue(this, null);
				
			command.Parameters.Add(parameter);
			
			IDataReader reader = command.ExecuteReader();
			
			while(reader.Read()) {
				result.Add((T)reader[0]);
			}
			
			reader.Close();
			command.Connection.Close();
			
			return result;
		}
		
		
		public T getByForeignModel<T>(PropertyInfo primary, PropertyInfo type) {
			T result = default(T);
			ModelList<T> results = this.getByForeignModels<T>(primary, type);
			
			if(results.Count > 0) {
				result = results[0];
			}
			
			return result;
		}
		
		
		public ModelList<T> getByForeignModels<T>(PropertyInfo primary, PropertyInfo type) {
			ModelList<T> result = new ModelList<T>();
			List<string> fields = new List<string>();
			//fields.Add(type.Name);
			PropertyInfo[] properties = null;
			PropertyInfo subPrimary = null;
			Type subType = null;
			
			if(type.PropertyType.IsGenericType) {
				properties = type.PropertyType.GetGenericArguments()[0].GetProperties();
				subType = type.PropertyType.GetGenericArguments()[0];
			} else {
				properties = type.PropertyType.GetProperties();
				subType = type.PropertyType;
			}
			
			for(int i = 0; i < properties.Length; i++) {
				PrimaryKeyAttribute[] primarys = (PrimaryKeyAttribute[])properties[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
				if(primarys.Length > 0) {
					subPrimary = properties[i];
					i = properties.Length;
				}
				/*if(type.PropertyType.IsGenericType) {
					PropertyInfo[] subProperties = properties[i].PropertyType.GetGenericArguments()[0].GetProperties();
					for(int j = 0; j < subProperties.Length; j++) {
						PrimaryKeyAttribute[] primarys = (PrimaryKeyAttribute[])subProperties[j].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
						if(primarys.Length > 0) {
							subPrimary = subProperties[j].PropertyType.GetGenericArguments()[0];
							j = subProperties.Length;
							i = properties.Length;
						}
					}
				} else {
					PrimaryKeyAttribute[] primarys = (PrimaryKeyAttribute[])properties[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
					if(primarys.Length > 0) {
						subPrimary = properties[i].PropertyType;
						i = properties.Length;
					}
				}*/
			}
			
			fields.Add(subType.Name + "_" + subPrimary.Name);
			
			List<string> conditions = new List<string>();
			conditions.Add(this.GetType().Name + "_" + primary.Name);
			
			IDbCommand command = SqlProvider.getProvider().createCommand();
			
			string table = this.GetType().Name + "_";
			if(type.PropertyType.IsGenericType) {
				table += type.PropertyType.GetGenericArguments()[0].Name;
			} else {
				table += type.PropertyType.Name;
			}
			
			command.CommandText = SqlProvider.getProvider().sqlString(fields, table, conditions);
			
			IDbDataParameter parameter = SqlProvider.getProvider().createParameter();
			parameter.ParameterName = this.GetType().Name + "_" + primary.Name;
			parameter.Value = primary.GetValue(this, null);
				
			command.Parameters.Add(parameter);
			
			IDataReader reader = command.ExecuteReader();
			
			while(reader.Read()) {
				//object key = reader[type.PropertyType.Name + "_" + subPrimary.Name];
				object key = reader[0];
				object instance;
				
				if(type.PropertyType.IsGenericType) {
					ConstructorInfo constructor = type.PropertyType.GetGenericArguments()[0].GetConstructor(new Type[0]);
					instance = constructor.Invoke(new Object[0]);
					instance.GetType().GetProperty(subPrimary.Name).SetValue(instance, key, null);
				} else {
					ConstructorInfo constructor = type.PropertyType.GetConstructor(new Type[0]);
					instance = constructor.Invoke(new Object[0]);
					/*instance.GetType().GetProperty(subPrimary.Name).SetValue(instance, key, null);
					result.Add(this.cast<T>(instance));*/
				}
				
				 
				if(instance.GetType().GetInterface("IModelizable") != null && instance.GetType().GetInterface("IList") == null) {
					instance = ((Model)instance).getBy<T>()[0];
				}
				
				result.Add(this.cast<T>(instance));
				
				/*
				instance.GetType().GetProperty(subPrimary.Name).SetValue(instance, key, null);
			
				result.Add(this.cast<T>(instance));
				*/
			}
			
			reader.Close();
			command.Connection.Close();
			
			return result;
		}
		
		
		
		/// <summary>
		/// Insert this model instance into database.
		/// </summary>
		public long insert() {
			PropertyInfo[] properties = this.GetType().GetProperties();
			ModelList<PropertyInfo> parameters = new ModelList<PropertyInfo>();
			Dictionary<string, Dictionary<long, Model>> ids = new Dictionary<string, Dictionary<long, Model>>();
			Dictionary<string, List<object>> basicIds = new Dictionary<string, List<object>>();
			
			for(int i = 0; i < properties.Length; i++) {
				if(properties[i].GetValue(this, null) != null) {
					TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
					PrimaryKeyAttribute[] primarys = (PrimaryKeyAttribute[])properties[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
					
					if((primarys.Length > 0 && !primarys[0].AutoIncrement) || (primarys.Length == 0 && attributes.Length > 0)) {
						if(properties[i].PropertyType.GetInterface("IModelizable") != null) {
							if(properties[i].PropertyType.GetInterface("IList") != null) {
								if(properties[i].PropertyType.GetGenericArguments()[0].GetInterface("IModelizable") != null) {
									foreach(Model iterModel in (IEnumerable<Model>)properties[i].GetValue(this, null)) {
										long subId = iterModel.insert();
										string name = properties[i].PropertyType.ToString();
										
										if(!ids.ContainsKey(name)) {
											ids.Add(name, new Dictionary<long, Model>());
										}
										
										ids[name].Add(subId, iterModel);
									}
								} else {
									string name = properties[i].PropertyType.Name;
									
									if(!basicIds.ContainsKey(name)) {
										basicIds.Add(name, new List<object>());
									}
									
									basicIds[name].Add(properties[i].GetValue(this, null));
								}
							} else {
								long subId = ((Model)properties[i].GetValue(this, null)).insert();
								string name = properties[i].PropertyType.ToString();
								
								if(!ids.ContainsKey(name)) {
									ids.Add(name, new Dictionary<long, Model>());
								}
								
								ids[name].Add(subId, ((Model)properties[i].GetValue(this, null)));
							}
						} else {
							parameters.Add(properties[i]);
						}
					
					
						/*
						if(Reflection.Instance.isChildOf(properties[i].PropertyType, typeof(Model))) {
							long subId = ((Model)properties[i].GetValue(this, null)).insert();
							string name = properties[i].PropertyType.ToString();
							
							if(!ids.ContainsKey(name)) {
								ids.Add(name, new Dictionary<long, Model>());
							}
							
							ids[name].Add(subId, ((Model)properties[i].GetValue(this, null)));
						} else {
							parameters.Add(properties[i]);
						}*/
					}
				}
			}
			
			IDbCommand command = SqlProvider.getProvider().createCommand();
			command.CommandText = SqlProvider.getProvider().sqlInsert(this.GetType(), parameters);
			
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
			
			
			foreach(string key in basicIds.Keys) {
 				for(int i = 0; i < basicIds[key].Count; i++) {
					Dictionary<string, object> sqlParams = new Dictionary<string, object>();
					PropertyInfo[] properties1 = this.GetType().GetProperties();
					
					for(int j = 0; j < properties1.Length; j++) {
						PrimaryKeyAttribute[] primaryKeys = (PrimaryKeyAttribute[])properties1[j].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
						
						if(primaryKeys.Length > 0) {
							sqlParams.Add(this.GetType().Name + "_" + properties1[j].Name, id);
						}
					}
					
					sqlParams.Add(key, basicIds[key][i]);
					
					this.query(SqlProvider.getProvider().sqlInsertBasicForeign(this.GetType(), key, basicIds[key][i].GetType()), sqlParams);
				}
			}
			
			command.Connection.Close();
			
			return id;
		}
		
		
		
		/// <summary>
		/// Update this instance.
		/// </summary>
		public void update() {
			PropertyInfo[] properties = this.GetType().GetProperties();
			ModelList<PropertyInfo> parameters = new ModelList<PropertyInfo>();
			ModelList<PropertyInfo> primaryKeys = new ModelList<PropertyInfo>();
			
			for(int i = 0; i < properties.Length; i++) {
				//if(properties[i].GetValue(this, null) != null) {
					TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
					PrimaryKeyAttribute[] primarys = (PrimaryKeyAttribute[])properties[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
					
					if(primarys.Length > 0) {
						primaryKeys.Add(properties[i]);
					}
					
					
					if(attributes.Length > 0) {
						if(!Reflection.Instance.isChildOf(properties[i].PropertyType, typeof(Model))) {
							parameters.Add(properties[i]);
						}
					}
				//}
			}
			
			IDbCommand command = SqlProvider.getProvider().createCommand();
			command.CommandText = SqlProvider.getProvider().sqlUpdate(this.GetType());
			
			for(int i = 0; i < parameters.Count; i++) {
				IDbDataParameter parameter = SqlProvider.getProvider().createParameter();
				parameter.ParameterName = parameters[i].Name;
				parameter.Value = parameters[i].GetValue(this, null);
				
				command.Parameters.Add(parameter);
			}
			
			command.ExecuteNonQuery();
			
			command.Connection.Close();
			
			return;
		}
		
		
		
		/// <summary>
		/// Delete this instance.
		/// </summary>
		public void delete() {
			PropertyInfo[] properties = this.GetType().GetProperties();
			ModelList<PropertyInfo> primaryKeys = new ModelList<PropertyInfo>();
			
			for(int i = 0; i < properties.Length; i++) {
				PrimaryKeyAttribute[] primarys = (PrimaryKeyAttribute[])properties[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
				
				if(primarys.Length > 0) {
					primaryKeys.Add(properties[i]);
				}
			}
			
			IDbCommand command = SqlProvider.getProvider().createCommand();
			command.CommandText = SqlProvider.getProvider().sqlDelete(this.GetType());
			
			for(int i = 0; i < primaryKeys.Count; i++) {
				IDbDataParameter parameter = SqlProvider.getProvider().createParameter();
				parameter.ParameterName = primaryKeys[i].Name;
				parameter.Value = primaryKeys[i].GetValue(this, null);
				
				command.Parameters.Add(parameter);
			}
			
			command.ExecuteNonQuery();
			
			command.Connection.Close();
			
			return;
		}
		
		
		
		
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
			return this.query(sql, null);
		}
		
		
		
		/// <summary>
		/// Query the specified sql and parameters.
		/// </summary>
		/// <param name='sql'>
		/// Sql.
		/// </param>
		/// <param name='parameters'>
		/// Parameters.
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
	}
}