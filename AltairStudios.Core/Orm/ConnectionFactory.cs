using System;
using MySql.Data.MySqlClient;
using AltairStudios.Core.Mvc;


namespace AltairStudios.Core.Orm {
	/// <summary>
	/// Connection factory.
	/// </summary>
	static class ConnectionFactory2 {
		/// <summary>
		/// Creates the connection.
		/// </summary>
		/// <returns>
		/// The connection.
		/// </returns>
		public static MySqlConnection createConnection() {
			MySqlConnection connection = new MySqlConnection(MvcApplication.ConnectionString);
			connection.Open();
			
			return connection;
		}
		
		
		
		/// <summary>
		/// Creates the command.
		/// </summary>
		/// <returns>
		/// The command.
		/// </returns>
		/*public static MySqlCommand createCommand() {
			MySqlCommand command = new MySqlCommand();
			command.Connection = ConnectionFactory.createConnection();

			return command;
		}*/
		
		
		
		/// <summary>
		/// Resolves the type.
		/// </summary>
		/// <returns>
		/// The type.
		/// </returns>
		/// <param name='type'>
		/// Type.
		/// </param>
		public static MySqlDbType resolveType(Type type) {
			MySqlDbType resolve = MySqlDbType.String;
			
			switch(type.ToString()) {
				case "System.String": resolve = MySqlDbType.VarChar; break;
			}
			
			return resolve;
		}
	}
}