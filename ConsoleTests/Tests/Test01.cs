using System;
using System.Text;


namespace AltairStudios.Core.ConsoleTests.Tests {
	/// <summary>
	/// Test01.
	/// </summary>
	public class Test01 {
		/// <summary>
		/// The name.
		/// </summary>
		protected static string name = "JSON test";
		
		
		/// <summary>
		/// The description.
		/// </summary>
		protected static string description = "Test json, xml, string, trace models serialization";
		
		
		
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
			text.Append("1.  - Simple serialization\n");
			text.Append("2.  - Simple serialization with basic data\n");
			text.Append("3.  - Simple serialization with inner data\n");
			text.Append("4.  - TestModel blank serialization\n");
			text.Append("5.  - TestModel simple data serialization\n");
			text.Append("6.  - TestModel all data serialization\n");
			
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
			} else if(test == 4) {
				output = test04();	
			} else if(test == 5) {
				output = test05();	
			} else if(test == 6) {
				output = test06();	
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
		/// Test01 test simple json serialization.
		/// </summary>
		protected static string test01() {
			AltairStudios.Core.Orm.Models.Address address = new AltairStudios.Core.Orm.Models.Address();
			
			return address.ToJson();
		}
		
		
		
		/// <summary>
		/// Test02 test simple json serialization with basic data.
		/// </summary>
		protected static string test02() {
			AltairStudios.Core.Orm.Models.Address address = new AltairStudios.Core.Orm.Models.Address();
			
			address.Block = "1a";
			address.Floor = "6";
			address.Id = 1111;
			address.Number = "37";
			address.Stair = "left";
			address.Street = "Sessame street";
			address.Zip = "28723";
			address.Type = null;
			
			return address.ToJson();
		}
		
		
		
		protected static string test03() {
			AltairStudios.Core.Orm.Models.Address address = new AltairStudios.Core.Orm.Models.Address();
			
			address.Block = "1a";
			address.Floor = "6";
			address.Id = 1111;
			address.Number = "37";
			address.Stair = "left";
			address.Street = "Sessame street";
			address.Zip = "28723";
			address.Type = null;
			
			address.City = new AltairStudios.Core.Orm.Models.City();
			address.City.Id = 5;
			address.City.Name = "Madrid";
			
			return address.ToJson();
		}
		
		
		
		protected static string test04() {
			Models.TestModel test = new Models.TestModel();
			
			return test.ToJson();
		}
		
		
		
		protected static string test05() {
			Models.TestModel test = new Models.TestModel();
			
			test.Id = 5;
			
			test.IntList = new AltairStudios.Core.Orm.ModelList<int>();
			test.IntList.Add(5);
			test.IntList.Add(7);
			test.IntList.Add(8);
			test.IntList.Add(3);
			
			return test.ToJson();
		}
		
		
		
		protected static string test06() {
			Models.TestModel test = new Models.TestModel();
			
			test.Id = 5;
			
			test.IntList = new AltairStudios.Core.Orm.ModelList<int>();
			test.IntList.Add(5);
			test.IntList.Add(7);
			test.IntList.Add(8);
			test.IntList.Add(3);
			
			test.UserList = new AltairStudios.Core.Orm.ModelList<AltairStudios.Core.Orm.Models.User>();
			
			AltairStudios.Core.Orm.Models.User userA = new AltairStudios.Core.Orm.Models.User();
			userA.Name = "Test";
			userA.Surname = "Test";
			userA.Email = "test@test.lol";
			userA.Password = "my_secret";
			test.UserList.Add(userA);
			
			return test.ToJson();
		}
	}
}