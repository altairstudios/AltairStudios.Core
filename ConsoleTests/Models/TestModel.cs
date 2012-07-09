using System;
using AltairStudios.Core.Mvc;
using AltairStudios.Core.Orm;
using AltairStudios.Core.Orm.Models;


namespace AltairStudios.Core.ConsoleTests.Models {
	public class TestModel : Orm.Model {
		protected int? id;
		protected Orm.ModelList<int> intList;
		protected Orm.ModelList<User> userList;
		
		[Templatize]
		public int? Id {
			get {
				return this.id;
			}
			set {
				id = value;
			}
		}
		
		[Templatize]
		public Orm.ModelList<int> IntList {
			get {
				return this.intList;
			}
			set {
				intList = value;
			}
		}
		
		[Templatize]
		public Orm.ModelList<User> UserList {
			get {
				return this.userList;
			}
			set {
				userList = value;
			}
		}
	}
}