using System;
using AltairStudios.Core.Orm;


namespace Invoices.Models {
	public class Business : Model {
		protected int id;
		protected string number;
		protected string name;
		protected int userId;
		
		[PrimaryKey(true)]
		public int Id {
			get {
				return id;
			}
			set {
				id = value;
			}
		}

		[Templatize()]
		public string Number {
			get {
				return number;
			}
			set {
				number = value;
			}
		}

		[Templatize()]
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}

		[Index()]
		public int UserId {
			get {
				return userId;
			}
			set {
				userId = value;
			}
		}
	}
}