using System;
using AltairStudios.Core.Orm;
using AltairStudios.Core.Orm.Models;


namespace AltairStudios.Core.ConsoleTests.Models {
	public class TestModel : Model {
		protected int id;
		protected ModelList<int> intList;
		protected ModelList<User> userList;
		
		
		public int Id {
			get {
				return this.id;
			}
			set {
				id = value;
			}
		}

		public ModelList<int> IntList {
			get {
				return this.intList;
			}
			set {
				intList = value;
			}
		}

		public ModelList<User> UserList {
			get {
				return this.userList;
			}
			set {
				userList = value;
			}
		}
	}
}