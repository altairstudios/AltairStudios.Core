using System;


namespace AltairStudios.Core.ConsoleTests {
	/// <summary>
	/// Main class.
	/// </summary>
	class MainClass {
		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments.
		/// </param>
		public static void Main (string[] args) {
			Console.Title = "AltairStudios.Core: Tests application";
			Console.WriteLine("Select test:");
			
			Console.WriteLine("  1. " + Tests.Test01.Name + ": " + Tests.Test01.Description);
			
			string option = Console.ReadLine();
			
			if(option == "1") {
			}
		}
	}
}