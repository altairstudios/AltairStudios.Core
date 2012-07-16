using System;
using System.Text;


namespace AltairStudios.Core.ConsoleTests.Tests {
	/// <summary>
	/// Test01.
	/// </summary>
	public class Test02 {
		/// <summary>
		/// The name.
		/// </summary>
		protected static string name = "ORM tests";
		
		
		/// <summary>
		/// The description.
		/// </summary>
		protected static string description = "Test orm system to diferents sql";
		
		
		
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public static string Description {
			get {
				return description;
			}
			set {
				description = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public static string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets the tests.
		/// </summary>
		/// <returns>
		/// The tests.
		/// </returns>
		public static string getTests() {
			StringBuilder text = new StringBuilder();
			text.Append("1.  - Create table TestModel\n");
			text.Append("2.  - Insert simple TestModel\n");
			text.Append("3.  - Insert complex basic TestModel\n");
			text.Append("4.  - Insert complex TestModel\n");
			
			return text.ToString();
		}
		
		
		
		/// <summary>
		/// Executes the test.
		/// </summary>
		/// <returns>
		/// The test.
		/// </returns>
		/// <param name='test'>
		/// Test.
		/// </param>
		public static string executeTest(int test) {
			string output = "";
			
			if(test == 1) {
				output = test01();	
			} else if(test == 2) {
				output = test02();
			} else if(test == 3) {
				output = test03();
			}
			
			return output;
		}
		
		
		
		/// <summary>
		/// Run this instance.
		/// </summary>
		public static void run() {
			Console.WriteLine(getTests());
			string option = Console.ReadLine();
			
			string output = executeTest(int.Parse(option));
			
			Console.WriteLine(output);
		}
		
		
		
		/// <summary>
		/// Test01 test create table sql.
		/// </summary>
		protected static string test01() {
			return AltairStudios.Core.Orm.Providers.SqlProvider.getProvider("MySql.Data.MySqlClient").sqlCreateTable(typeof(AltairStudios.Core.ConsoleTests.Models.TestModel));
		}
		
		
		
		protected static string test02() {
			AltairStudios.Core.ConsoleTests.Models.TestModel test = new AltairStudios.Core.ConsoleTests.Models.TestModel();
			
			test.Name = "testname";
			test.PasswordMd5 = "password";
			test.PasswordSha1 = "password";
			
			return AltairStudios.Core.Orm.Providers.SqlProvider.getProvider("MySql.Data.MySqlClient").sqlInsert(test.GetType());
		}
		
		
		
		protected static string test03() {
			AltairStudios.Core.ConsoleTests.Models.TestModel test = new AltairStudios.Core.ConsoleTests.Models.TestModel();
			
			test.Name = "testname";
			test.PasswordMd5 = "password";
			test.PasswordSha1 = "password";
			
			test.IntList = new AltairStudios.Core.Orm.ModelList<int>();
			test.IntList.Add(1);
			test.IntList.Add(2);
			
			return AltairStudios.Core.Orm.Providers.SqlProvider.getProvider("MySql.Data.MySqlClient").sqlInsert(test.GetType());
		}
		
		
		
		protected static string test04() {
			AltairStudios.Core.ConsoleTests.Models.TestModel test = new AltairStudios.Core.ConsoleTests.Models.TestModel();
			
			test.Name = "testname";
			test.PasswordMd5 = "password";
			test.PasswordSha1 = "password";
			
			test.IntList = new AltairStudios.Core.Orm.ModelList<int>();
			test.IntList.Add(1);
			test.IntList.Add(2);
			
			test.UserList = new AltairStudios.Core.Orm.ModelList<AltairStudios.Core.Orm.Models.User>();
			AltairStudios.Core.Orm.Models.User user = new AltairStudios.Core.Orm.Models.User();
			user.Email = "test@test.com";
			user.Name = "test";
			user.Surname = "surtest";
			test.UserList.Add(user);
			
			return AltairStudios.Core.Orm.Providers.SqlProvider.getProvider("MySql.Data.MySqlClient").sqlInsert(test.GetType());
		}
	}
}