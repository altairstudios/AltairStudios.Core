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
	}
}