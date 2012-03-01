using System;
using MySql.Data.MySqlClient;
using AltairStudios.Core.Mvc;


namespace AltairStudios.Core.Orm {
	static class ConnectionFactory {
		public static MySqlConnection createConnection() {
			MySqlConnection connection = new MySqlConnection(MvcApplication.ConnectionString);
			connection.Open();
			
			return connection;
		}
		
		
		public static MySqlCommand createCommand() {
			MySqlCommand command = new MySqlCommand();
			command.Connection = ConnectionFactory.createConnection();

			return command;
		}
		
		
		public static MySqlDbType resolveType(Type type) {
			MySqlDbType resolve = MySqlDbType.String;
			
			switch(type.ToString()) {
				case "System.String": resolve = MySqlDbType.VarChar; break;
			}
			
			return resolve;
		}
	}
}