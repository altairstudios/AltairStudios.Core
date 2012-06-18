using System;
using System.Data;
using MySql.Data.MySqlClient;


namespace AltairStudios.Core.Orm.Providers {
	/// <summary>
	/// MySql provider.
	/// </summary>
	public class MySqlProvider : SqlProvider {
		/// <summary>
		/// Creates the connection.
		/// </summary>
		/// <returns>
		/// The connection.
		/// </returns>
		public override IDbConnection createConnection() {
			IDbConnection connection = new MySqlConnection(this.connectionString);
			connection.Open();
			
			return connection;
		}
		
		
		
		/// <summary>
		/// Creates the command.
		/// </summary>
		/// <returns>
		/// The command.
		/// </returns>
		public override IDbCommand createCommand() {
			IDbCommand command = new MySqlCommand();
			command.Connection = this.createConnection();

			return command;
		}
		
		
		
		/// <summary>
		/// Creates the parameter.
		/// </summary>
		/// <returns>
		/// The parameter.
		/// </returns>
		public override IDbDataParameter createParameter() {
			IDbDataParameter parameter = new MySqlParameter();

			return parameter;
		}
		
		
		
		/// <summary>
		/// Gets the inserted identifier.
		/// </summary>
		/// <returns>
		/// The inserted identifier.
		/// </returns>
		protected override string getInsertedId() {
			return "select last_insert_id();";
		}
	}
}