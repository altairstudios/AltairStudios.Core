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
			Console.Clear();
			Console.Title = "AltairStudios.Core: Tests application";
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(Console.Title + "\n");
			
			Console.ForegroundColor = ConsoleColor.White;
			
			for(int i = 0; i < Console.BufferHeight - 4; i++) {
				Console.Write("\n");
			}
			
			Console.WriteLine("Press any key to continue");
			Console.ReadKey();
			
			testSelection();
			
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("Thanks for test AltairStudios.Core\n\n");
		}
		
		
		protected static void testSelection() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine("Select test:");
			
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("  1. " + Tests.Test01.Name + ": " + Tests.Test01.Description);
			Console.WriteLine("  q. Exit");
			
			string option = Console.ReadLine();
			
			if(option == "1") {
				Tests.Test01.run();
			} else if(option == "q") {
				return;
			} else {
				testSelection();	
			}
		}
	}
}