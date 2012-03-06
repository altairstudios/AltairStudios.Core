using System;
using AltairStudios.Core.Orm;


namespace AltairStudios.Core.Orm.Models {
	public class User : Model {
		protected int id;
		protected string email;
		protected string password;
		protected bool remember;
		protected string name;
		protected string surname;
		
		[Templatize]
		public string Name {
			get {
				return this.name;
			}
			set {
				name = value;
			}
		}
		
		[Templatize]
		public string Surname {
			get {
				return this.surname;
			}
			set {
				surname = value;
			}
		}
		
		public bool Remember {
			get {
				return this.remember;
			}
			set {
				remember = value;
			}
		}
		
		[Templatize]
		public string Email {
			get {
				return this.email;
			}
			set {
				email = value;
			}
		}
		
		[Templatize]
		public int Id {
			get {
				return this.id;
			}
			set {
				id = value;
			}
		}
		
		[Templatize]
		public string Password {
			get {
				return this.password;
			}
			set {
				password = value;
			}
		}
		
		public User() {
		}
	}
}