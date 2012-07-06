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
			text.Append("1.  - Dummy");
			
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
			return "";
		}
		
		
		//protected static string run
	}
}