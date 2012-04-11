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
			
			sql.Append("CREATE TABLE IF NOT EXISTS `" + type.Name + "` (");
			
			for(int i = 0; i < properties.Length; i++) {
				string sqlType = "varchar(255)";
				
				switch(properties[i].PropertyType.ToString()) {
					case "System.Int32": sqlType = "int(11)"; break;
				}
				
				sqlFields.Add("`" + properties[i].Name + "` " + sqlType + " NOT NULL");
			}
			
			sql.Append(string.Join(",", sqlFields.ToArray()));
			sql.Append(") ENGINE=InnoDB DEFAULT CHARSET=utf8");
			
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