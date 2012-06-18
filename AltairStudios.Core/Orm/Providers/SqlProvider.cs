using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Text;


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
			this.connectionString = ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString;
		}
		
		
		
		/// <summary>
		/// Gets the provider.
		/// </summary>
		/// <returns>
		/// The provider.
		/// </returns>
		public static SqlProvider getProvider() {
			if(provider != null) {
				return provider;
			}
			
			string providerName = ConfigurationManager.ConnectionStrings["SqlServerConnection"].ProviderName;
			
			switch(providerName) {
				case "MySql.Data.MySqlClient": provider = new MySqlProvider(); break;
				case "System.Data.SqlClient": provider = new SqlServerProvider(); break;
			}
			
			return provider;
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
			ModelList<string> keys = new ModelList<string>();
			
			sql.Append("CREATE TABLE IF NOT EXISTS " + type.Name + " (");
			
			for(int i = 0; i < properties.Length; i++) {
				TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
				PrimaryKeyAttribute[] primaryKeys = (PrimaryKeyAttribute[])properties[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
				IndexAttribute[] indexes = (IndexAttribute[])properties[i].GetCustomAttributes(typeof(IndexAttribute), true);
				
				if(primaryKeys.Length > 0) {
					primaryFields.Add(properties[i].Name);
				} else if(indexes.Length > 0 && indexes[0].Unique) {
					uniqueFields.Add(properties[i].Name);
				} else if(indexes.Length > 0) {
					indexFields.Add(properties[i].Name);
				}
				
				if((attributes.Length > 0 && attributes[0].Templatize) || (primaryKeys.Length > 0 && primaryKeys[0].Templatize) || (indexes.Length > 0 && indexes[0].Templatize)) {
					string sqlType = "varchar(255)";
					
					switch(properties[i].PropertyType.ToString()) {
						case "System.Int32": sqlType = "int(11)"; break;
					}
					
					if(primaryKeys.Length > 0 && primaryKeys[0].AutoIncrement) {
						sqlFields.Add(properties[i].Name + " " + sqlType + " NOT NULL AUTO_INCREMENT");
					} else {
						sqlFields.Add(properties[i].Name + " " + sqlType + " NOT NULL");
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
			
			sql.Append("INSERT INTO " + type.Name);
			
			for(int i = 0; i < properties.Length; i++) {
				TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
				PrimaryKeyAttribute[] primaryKeys = (PrimaryKeyAttribute[])properties[i].GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
				IndexAttribute[] indexes = (IndexAttribute[])properties[i].GetCustomAttributes(typeof(IndexAttribute), true);
				
				if((primaryKeys.Length > 0 && primaryKeys[0].AutoIncrement == false) || (indexes.Length > 0) || (attributes.Length > 0 && attributes[0].Templatize && !attributes[0].IsSubtable)) {
					sqlNames.Add(properties[i].Name);
					sqlFields.Add("@" + properties[i].Name);
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
		/// Gets the inserted identifier.
		/// </summary>
		/// <returns>
		/// The inserted identifier.
		/// </returns>
		protected abstract string getInsertedId();
		
		
		
		
		
		/*
		public string sqlUpdate(Type type) {
			StringBuilder sql = new StringBuilder();
			PropertyInfo[] properties = type.GetProperties();
			ModelList<string> sqlFields = new ModelList<string>();
			ModelList<string> sqlNames = new ModelList<string>();
			
			sql.Append("UPDATE " + type.Name);
			
			for(int i = 0; i < properties.Length; i++) {
				TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
				if(attributes.Length > 0 && attributes[0].Templatize) {
					sqlNames.Add(properties[i].Name);
					sqlFields.Add(properties[i].Name + " = @" + properties[i].Name);
				}
			}
			
			sql.Append("SET " + string.Join(",", sqlFields) + "");
			sql.Append(" WHERE ");
			sql.Append("(" + string.Join(",", sqlFields) + ")");
			
			return sql.ToString();
		}
		*/
		
		
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
			
			
			sql.Append("SELECT " + string.Join(",", fields.ToArray()) + " FROM " + type.Name + " WHERE ");
			
			for(int i = 0; i < properties.Length; i++) {
				if(properties[i].GetValue(model, null) != null && properties[i].PropertyType.ToString() == "System.String") {					
					TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
					if(attributes.Length > 0 && attributes[0].Templatize) {
						sql.Append(properties[i].Name + " = @" + properties[i].Name + " AND " );
						parameters.Add(properties[i]);
					}
				}
			}
			
			sql.Append("1 = 1");
			
			return sql.ToString();
		}
	}
}