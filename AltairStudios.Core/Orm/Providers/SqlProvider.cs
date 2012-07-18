using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Text;
using AltairStudios.Core.Util;


namespace AltairStudios.Core.Orm.Providers {
	/// <summary>
	/// Sql provider.
	/// </summary>
	public abstract class SqlProvider {
		/// <summary>
		/// The provider.
		/// </summary>
		protected static SqlProvider provider;
		
		
		
		/// <summary>
		/// The connection string.
		/// </summary>
		protected string connectionString;
	
	
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Orm.Providers.SqlProvider"/> class.
		/// </summary>
		protected SqlProvider() {
			if(ConfigurationManager.ConnectionStrings["SqlServerConnection"] != null) {
				this.connectionString = ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString;
			}
		}
		
		
		
		/// <summary>
		/// Gets the provider.
		/// </summary>
		/// <returns>
		/// The provider.
		/// </returns>
		public static SqlProvider getProvider(string providerName) {
			if(provider != null) {
				return provider;
			}
			
			switch(providerName) {
				case "MySql.Data.MySqlClient": provider = new MySqlProvider(); break;
				case "System.Data.SqlClient": provider = new SqlServerProvider(); break;
			}
			
			return provider;
		}
		
		
		
		public static SqlProvider getProvider() {
			string providerName = ConfigurationManager.ConnectionStrings["SqlServerConnection"].ProviderName;
			return getProvider(providerName);
		}
		
		
		
		/// <summary>
		/// Creates the connection.
		/// </summary>
		/// <returns>
		/// The connection.
		/// </returns>
		public abstract IDbConnection createConnection();
		
		
		
		/// <summary>
		/// Creates the command.
		/// </summary>
		/// <returns>
		/// The command.
		/// </returns>
		public abstract IDbCommand createCommand();
		
		
		
		/// <summary>
		/// Creates the parameter.
		/// </summary>
		/// <returns>
		/// The parameter.
		/// </returns>
		public abstract IDbDataParameter createParameter();
		
		
		
		/// <summary>
		/// Sqls the create table.
		/// </summary>
		/// <returns>
		/// The create table.
		/// </returns>
		/// <param name='type'>
		/// Type.
		/// </param>
		public string sqlCreateTable(Type type) {
			StringBuilder sql = new StringBuilder();
			PropertyInfo[] properties = type.GetProperties();
			ModelList<string> sqlFields = new ModelList<string>();
			ModelList<string> primaryFields = new ModelList<string>();
			ModelList<string> indexFields = new ModelList<string>();
			ModelList<string> uniqueFields = new ModelList<string>();
			ModelList<string> foreignFields = new ModelList<string>();
			//ModelList<string> keys = new ModelList<string>();
			ModelList<string> createdModelsName = new ModelList<string>();
			
			sql.Append("CREATE TABLE IF NOT EXISTS " + this.sqlEscapeTable(type.Name) + " (");
			
			for(int i = 0; i < properties.Length; i++) {
				TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
				PrimaryKeyAttribute[] primaryKeys = (PrimaryKeyAttribute[])properties[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
				IndexAttribute[] indexes = (IndexAttribute[])properties[i].GetCustomAttributes(typeof(IndexAttribute), true);
				//ForeignKeyAttribute[] foreigns = (ForeignKeyAttribute[])properties[i].GetCustomAttributes(typeof(ForeignKeyAttribute), true);
				
				if(primaryKeys.Length > 0) {
					primaryFields.Add(this.sqlEscapeField(properties[i].Name));
				} else if(indexes.Length > 0 && indexes[0].Unique) {
					uniqueFields.Add(this.sqlEscapeField(properties[i].Name));
				} else if(indexes.Length > 0) {
					indexFields.Add(this.sqlEscapeField(properties[i].Name));
				}
				
				if(attributes.Length > 0) {
					string sqlType = this.convertTypeToSql(properties[i].PropertyType);
					
					if(primaryKeys.Length > 0 && primaryKeys[0].AutoIncrement) {
						sqlFields.Add(this.sqlEscapeField(properties[i].Name) + " " + sqlType + " NOT NULL AUTO_INCREMENT");
					} else {
						if(properties[i].PropertyType.GetInterface("IModelizable") != null && !createdModelsName.Contains(properties[i].PropertyType.ToString())) {
							if(properties[i].PropertyType.GetInterface("IList") != null) {
								if(properties[i].PropertyType.GetGenericArguments()[0].GetInterface("IModelizable") != null) {
									foreignFields.Add(this.sqlCreateTable(properties[i].PropertyType.GetGenericArguments()[0]));
									foreignFields.Add(this.sqlCreateForeignTable(type, properties[i].PropertyType.GetGenericArguments()[0]));
								} else {
									foreignFields.Add(this.sqlCreateForeignBasicTable(type, properties[i].Name, properties[i].PropertyType.GetGenericArguments()[0]));
								}
							} else {
								foreignFields.Add(this.sqlCreateTable(properties[i].PropertyType));
								foreignFields.Add(this.sqlCreateForeignTable(type, properties[i].PropertyType));
							}
							
							createdModelsName.Add(properties[i].PropertyType.ToString());
						} else {
							sqlFields.Add(this.sqlEscapeField(properties[i].Name) + " " + sqlType + " NOT NULL");
						}
					}
				}
			}
			
			if(primaryFields.Count > 0) {
				sqlFields.Add("PRIMARY KEY (" + string.Join(",", primaryFields.ToArray()) + ")");
			}
			
			if(uniqueFields.Count > 0) {
				sqlFields.Add("UNIQUE KEY (" + string.Join(",", uniqueFields.ToArray()) + ")");
			}
			
			if(indexFields.Count > 0) {
				sqlFields.Add("KEY (" + string.Join(",", indexFields.ToArray()) + ")");
			}
			
			sql.Append(string.Join(",", sqlFields.ToArray()));
			
			sql.Append(") ENGINE=InnoDB DEFAULT CHARSET=utf8");
			
			if(foreignFields.Count > 0) {
				sql.Append(";" + string.Join(";", foreignFields.ToArray()));
			}
			
			return sql.ToString();
		}
		
		
		
		/// <summary>
		/// Sqls the create foreign table.
		/// </summary>
		/// <returns>
		/// The create foreign table.
		/// </returns>
		/// <param name='type1'>
		/// Type1.
		/// </param>
		/// <param name='type2'>
		/// Type2.
		/// </param>
		public string sqlCreateForeignTable(Type type1, Type type2) {
			StringBuilder sql = new StringBuilder();
			PropertyInfo[] properties1 = type1.GetProperties();
			PropertyInfo[] properties2 = type2.GetProperties();
			ModelList<string> fields = new ModelList<string>();
			ModelList<string> keys = new ModelList<string>();
			
			for(int i = 0; i < properties1.Length; i++) {
				PrimaryKeyAttribute[] primaryKeys = (PrimaryKeyAttribute[])properties1[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
				
				if(primaryKeys.Length > 0) {
					string sqlType = this.convertTypeToSql(properties1[i].PropertyType);
					string name = this.sqlEscapeField(type1.Name + "_" + properties1[i].Name);
					
					fields.Add(name + " " + sqlType + " NOT NULL");
					keys.Add(name);
				}
			}
			
			for(int i = 0; i < properties2.Length; i++) {
				PrimaryKeyAttribute[] primaryKeys = (PrimaryKeyAttribute[])properties2[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
				
				if(primaryKeys.Length > 0) {
					string sqlType = this.convertTypeToSql(properties2[i].PropertyType);
					string name = this.sqlEscapeField(type2.Name + "_" + properties2[i].Name);
					
					fields.Add(name + " " + sqlType + " NOT NULL");
					keys.Add(name);
				}
			}
			
			if(keys.Count > 0) {
				fields.Add("PRIMARY KEY (" + string.Join(",", keys.ToArray()) + ")");
			}
			
			sql.Append("CREATE TABLE IF NOT EXISTS " + this.sqlEscapeTable(type1.Name + "_" + type2.Name) + " (");
			sql.Append(string.Join(",", fields.ToArray()));
			
			
			
			sql.Append(") ENGINE=InnoDB DEFAULT CHARSET=utf8");
			
			return sql.ToString();
		}
		
		
		
		/// <summary>
		/// Sqls the create foreign basic table.
		/// </summary>
		/// <returns>
		/// The create foreign basic table.
		/// </returns>
		/// <param name='type1'>
		/// Type1.
		/// </param>
		/// <param name='type2'>
		/// Type2.
		/// </param>
		public string sqlCreateForeignBasicTable(Type type1, string basicName, Type type2) {
			StringBuilder sql = new StringBuilder();
			PropertyInfo[] properties1 = type1.GetProperties();
			ModelList<string> fields = new ModelList<string>();
			ModelList<string> keys = new ModelList<string>();
			
			for(int i = 0; i < properties1.Length; i++) {
				PrimaryKeyAttribute[] primaryKeys = (PrimaryKeyAttribute[])properties1[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
				
				if(primaryKeys.Length > 0) {
					string sqlType = this.convertTypeToSql(properties1[i].PropertyType);
					string name = this.sqlEscapeField(type1.Name + "_" + properties1[i].Name);
					
					fields.Add(name + " " + sqlType + " NOT NULL");
					keys.Add(name);
				}
			}
			
			string sqlBasicType = this.convertTypeToSql(type2);
			//basicName = this.sqlEscapeField(basicName);
					
			fields.Add(this.sqlEscapeField(basicName) + " " + sqlBasicType + " NOT NULL");
			
			
			if(keys.Count > 0) {
				fields.Add("KEY (" + string.Join(",", keys.ToArray()) + ")");
			}
			
			sql.Append("CREATE TABLE IF NOT EXISTS " + this.sqlEscapeTable(type1.Name + "_" + basicName) + " (");
			sql.Append(string.Join(",", fields.ToArray()));
			
			sql.Append(") ENGINE=InnoDB DEFAULT CHARSET=utf8");
			
			return sql.ToString();
		}
		
		
		
		/// <summary>
		/// Sqls the insert.
		/// </summary>
		/// <returns>
		/// The insert.
		/// </returns>
		/// <param name='type'>
		/// Type.
		/// </param>
		public string sqlInsert(Type type) {
			StringBuilder sql = new StringBuilder();
			PropertyInfo[] properties = type.GetProperties();
			ModelList<string> sqlFields = new ModelList<string>();
			ModelList<string> sqlNames = new ModelList<string>();
			
			sql.Append("INSERT INTO " + this.sqlEscapeTable(type.Name));
			
			for(int i = 0; i < properties.Length; i++) {
				TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
				PrimaryKeyAttribute[] primaryKeys = (PrimaryKeyAttribute[])properties[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
				
				if((primaryKeys.Length > 0 && primaryKeys[0].AutoIncrement == false) || (primaryKeys.Length == 0 && attributes.Length > 0)) {
					if(properties[i].PropertyType.GetInterface("IModelizable") == null) {
						sqlNames.Add(this.sqlEscapeField(properties[i].Name));
						sqlFields.Add("@" + properties[i].Name);
					}
				}
			}
			
			sql.Append("(" + string.Join(",", sqlNames.ToArray()) + ")");
			sql.Append(" VALUES ");
			sql.Append("(" + string.Join(",", sqlFields.ToArray()) + ")");
			
			sql.Append(";");
			
			sql.Append(this.getInsertedId());
			
			return sql.ToString();
		}
		
		
		
		public string sqlInsert(Type type, ModelList<PropertyInfo> parameters) {
			StringBuilder sql = new StringBuilder();
			ModelList<string> sqlFields = new ModelList<string>();
			ModelList<string> sqlNames = new ModelList<string>();
			
			sql.Append("INSERT INTO " + this.sqlEscapeTable(type.Name));
			
			for(int i = 0; i < parameters.Count; i++) {
				TemplatizeAttribute[] attributes = (TemplatizeAttribute[])parameters[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
				PrimaryKeyAttribute[] primaryKeys = (PrimaryKeyAttribute[])parameters[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
				
				if((primaryKeys.Length > 0 && primaryKeys[0].AutoIncrement == false) || (primaryKeys.Length == 0 && attributes.Length > 0)) {
					if(parameters[i].PropertyType.GetInterface("IModelizable") == null) {
						sqlNames.Add(this.sqlEscapeField(parameters[i].Name));
						sqlFields.Add("@" + parameters[i].Name);
					}
				}
			}
			
			sql.Append("(" + string.Join(",", sqlNames.ToArray()) + ")");
			sql.Append(" VALUES ");
			sql.Append("(" + string.Join(",", sqlFields.ToArray()) + ")");
			
			sql.Append(";");
			
			sql.Append(this.getInsertedId());
			
			return sql.ToString();
		}
		
		
		
		/// <summary>
		/// Sqls the insert foreign.
		/// </summary>
		/// <returns>
		/// The insert foreign.
		/// </returns>
		/// <param name='type1'>
		/// Type1.
		/// </param>
		/// <param name='type2'>
		/// Type2.
		/// </param>
		public string sqlInsertForeign(Type type1, Type type2) {
			StringBuilder sql = new StringBuilder();
			PropertyInfo[] properties1 = type1.GetProperties();
			PropertyInfo[] properties2 = type2.GetProperties();
			ModelList<string> fields = new ModelList<string>();
			ModelList<string> parameters = new ModelList<string>();
			string name = type1.Name + "_" + type2.Name;
			
			for(int i = 0; i < properties1.Length; i++) {
				PrimaryKeyAttribute[] primaryKeys = (PrimaryKeyAttribute[])properties1[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
				
				if(primaryKeys.Length > 0) {
					fields.Add(this.sqlEscapeField(type1.Name + "_" + properties1[i].Name));
					parameters.Add("@" + type1.Name + "_" + properties1[i].Name);
				}
			}
			
			for(int i = 0; i < properties2.Length; i++) {
				PrimaryKeyAttribute[] primaryKeys = (PrimaryKeyAttribute[])properties2[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
				
				if(primaryKeys.Length > 0) {
					fields.Add(this.sqlEscapeField(type2.Name + "_" + properties2[i].Name));
					parameters.Add("@" + type2.Name + "_" + properties2[i].Name);
				}
			}
			
			sql.Append("INSERT INTO " + this.sqlEscapeTable(name));
			sql.Append("(" + string.Join(",", fields.ToArray()) + ") VALUES (" + string.Join(",", parameters.ToArray()) + ")");
			
			return sql.ToString();
		}
		
		
		
		public string sqlInsertBasicForeign(Type type1, string basicName, Type type2) {
			StringBuilder sql = new StringBuilder();
			PropertyInfo[] properties1 = type1.GetProperties();
			PropertyInfo[] properties2 = type2.GetProperties();
			ModelList<string> fields = new ModelList<string>();
			ModelList<string> parameters = new ModelList<string>();
			string name = type1.Name + "_" + type2.Name;
			
			for(int i = 0; i < properties1.Length; i++) {
				PrimaryKeyAttribute[] primaryKeys = (PrimaryKeyAttribute[])properties1[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
				
				if(primaryKeys.Length > 0) {
					fields.Add(this.sqlEscapeField(type1.Name + "_" + properties1[i].Name));
					parameters.Add("@" + type1.Name + "_" + properties1[i].Name);
				}
			}
			
			fields.Add(this.sqlEscapeField(basicName));
			parameters.Add("@" + basicName);
			
			sql.Append("INSERT INTO " + this.sqlEscapeTable(name));
			sql.Append("(" + string.Join(",", fields.ToArray()) + ") VALUES (" + string.Join(",", parameters.ToArray()) + ")");
			
			return sql.ToString();
		}
		
		
		
		/// <summary>
		/// Gets the inserted identifier.
		/// </summary>
		/// <returns>
		/// The inserted identifier.
		/// </returns>
		protected abstract string getInsertedId();
		
		
		
		/// <summary>
		/// Sqls the update.
		/// </summary>
		/// <returns>
		/// The update.
		/// </returns>
		/// <param name='type'>
		/// Type.
		/// </param>
		public string sqlUpdate(Type type) {
			StringBuilder sql = new StringBuilder();
			PropertyInfo[] properties = type.GetProperties();
			ModelList<string> sqlFields = new ModelList<string>();
			ModelList<string> sqlNames = new ModelList<string>();
			ModelList<string> whereFields = new ModelList<string>();
			
			sql.Append("UPDATE " + this.sqlEscapeTable(type.Name));
			
			for(int i = 0; i < properties.Length; i++) {
				TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
				PrimaryKeyAttribute[] primaryKeys = (PrimaryKeyAttribute[])properties[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
				
				if(attributes.Length > 0 && attributes[0].Templatize && primaryKeys.Length == 0 && properties[i].PropertyType.GetInterface("IModelizable") == null) {
					sqlNames.Add(properties[i].Name);
					sqlFields.Add(this.sqlEscapeField(properties[i].Name) + " = @" + properties[i].Name);
				} else if(primaryKeys.Length > 0 && properties[i].PropertyType.GetInterface("IModelizable") == null) {
					whereFields.Add(this.sqlEscapeField(properties[i].Name) + " = @" + properties[i].Name);
				}
			}
			
			sql.Append(" SET " + string.Join(",", sqlFields.ToArray()) + "");
			sql.Append(" WHERE ");
			sql.Append("(" + string.Join(",", whereFields.ToArray()) + ")");
			
			return sql.ToString();
		}
		
		
		
		/// <summary>
		/// Sqls the delete.
		/// </summary>
		/// <returns>
		/// The delete.
		/// </returns>
		/// <param name='type'>
		/// Type.
		/// </param>
		public string sqlDelete(Type type) {
			StringBuilder sql = new StringBuilder();
			PropertyInfo[] properties = type.GetProperties();
			ModelList<string> whereFields = new ModelList<string>();
			
			sql.Append("DELETE FROM " + this.sqlEscapeTable(type.Name));
			
			for(int i = 0; i < properties.Length; i++) {
				PrimaryKeyAttribute[] primaryKeys = (PrimaryKeyAttribute[])properties[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
				
				if(primaryKeys.Length > 0 && properties[i].PropertyType.GetInterface("IModelizable") == null) {
					whereFields.Add(this.sqlEscapeField(properties[i].Name) + " = @" + properties[i].Name);
				}
			}
			
			sql.Append(" WHERE ");
			sql.Append("(" + string.Join(",", whereFields.ToArray()) + ")");
			
			return sql.ToString();
		}
		
		
		
		/// <summary>
		/// Sqls the string.
		/// </summary>
		/// <returns>
		/// The string.
		/// </returns>
		/// <param name='model'>
		/// Model.
		/// </param>
		/// <param name='type'>
		/// Type.
		/// </param>
		/// <param name='fields'>
		/// Fields.
		/// </param>
		/// <param name='properties'>
		/// Properties.
		/// </param>
		public string sqlString(Model model, Type type, List<string> fields, PropertyInfo[] properties) {
			StringBuilder sql = new StringBuilder();
			ModelList<PropertyInfo> parameters = new ModelList<PropertyInfo>();
			
			for(int i = 0; i < fields.Count; i++) {
				fields[i] = this.sqlEscapeField(fields[i]);
			}
			
			sql.Append("SELECT " + string.Join(",", fields.ToArray()) + " FROM " + this.sqlEscapeTable(type.Name) + " WHERE ");
			
			for(int i = 0; i < properties.Length; i++) {
				if(properties[i].GetValue(model, null) != null) {					
					TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
					if(attributes.Length > 0 && properties[i].PropertyType.GetInterface("IModelizable") == null) {
						sql.Append(this.sqlEscapeField(properties[i].Name) + " = @" + properties[i].Name + " AND " );
						parameters.Add(properties[i]);
					}
				}
			}
			
			sql.Append("1 = 1");
			
			return sql.ToString();
		}
		
		
		
		public string sqlString(List<string> fields, string table, List<string> conditions) {
			StringBuilder sql = new StringBuilder();
			
			for(int i = 0; i < fields.Count; i++) {
				fields[i] = this.sqlEscapeField(fields[i]);
			}
			
			for(int i = 0; i < conditions.Count; i++) {
				conditions[i] = this.sqlEscapeField(conditions[i]) + " = @" + conditions[i];
			}
			
			sql.Append("SELECT " + string.Join(",", fields.ToArray()) + " FROM " + this.sqlEscapeTable(table));
			if(conditions.Count > 0) {
				sql.Append(" WHERE " + string.Join(" AND ", conditions.ToArray()));
			}
			
			return sql.ToString();
		}
		
		
		
		/// <summary>
		/// Sqls the escape field.
		/// </summary>
		/// <returns>
		/// The escape field.
		/// </returns>
		/// <param name='field'>
		/// Field.
		/// </param>
		protected virtual string sqlEscapeField(string field) {
			return field;
		}
		
		
		
		/// <summary>
		/// Sqls the escape table.
		/// </summary>
		/// <returns>
		/// The escape table.
		/// </returns>
		/// <param name='table'>
		/// Table.
		/// </param>
		protected virtual string sqlEscapeTable(string table) {
			return table;
		}
		
		
		
		protected virtual string convertTypeToSql(Type type) {
			string sqlType = "varchar(255)";
			
			if(type == typeof(int) || type == typeof(int?)) {
				sqlType = "int(11)";
			} else if(type == typeof(double)) {
				sqlType = "double(11)";
			} else if(type == typeof(decimal)) {
				sqlType = "decimal(10,2)";
			}
			
			return sqlType;
		}
	}
}