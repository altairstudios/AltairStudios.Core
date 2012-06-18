using System;
using System.Data;
using System.Data.SqlClient;



namespace AltairStudios.Core.Orm.Providers {
	/// <summary>
	/// SqlServer provider.
	/// </summary>
	public class SqlServerProvider : SqlProvider {
		/// <summary>
		/// Creates the connection.
		/// </summary>
		/// <returns>
		/// The connection.
		/// </returns>
		public override IDbConnection createConnection() {
			IDbConnection connection = new SqlConnection(this.connectionString);
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
			IDbCommand command = new SqlCommand();
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
			IDbDataParameter parameter = new SqlParameter();

			return parameter;
		}
		
		
		
		/// <summary>
		/// Gets the inserted identifier.
		/// </summary>
		/// <returns>
		/// The inserted identifier.
		/// </returns>
		protected override string getInsertedId() {
			return "SELECT SCOPE_IDENTITY();";
		}
	}
}