using System;
using AltairStudios.Core.Orm;
using AltairStudios.Core.Orm.Models;


namespace Invoices.Models {
	public class Contact : Model, IContactable {
		protected int? id;
		protected string number;
		protected string name;
		protected string surname;
		protected string email;
		protected ModelList<Phone> phones;
		protected int? userId;
		
		[PrimaryKey(true)]
		public int? Id {
			get {
				return id;
			}
			set {
				id = value;
			}
		}

		[Templatize]
		public string Number {
			get {
				return number;
			}
			set {
				number = value;
			}
		}

		[Templatize]
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}

		[Templatize]
		public string Surname {
			get {
				return surname;
			}
			set {
				surname = value;
			}
		}

		[Templatize]
		public string Email {
			get {
				return email;
			}
			set {
				email = value;
			}
		}

		[Templatize]
		public ModelList<Phone> Phones {
			get {
				return phones;
			}
			set {
				phones = value;
			}
		}
		
		[Index]
		public int? UserId {
			get {
				return userId;
			}
			set {
				userId = value;
			}
		}
		
		
		public string getName() {
			return this.name + " " + this.surname;
		}
		
		public string getPhone() {
			if(this.phones != null && this.phones.Count > 0) {
				return this.phones[0].Number;
			} else {
				return "";
			}
		}
		
		public string getEmail() {
			return this.email;
		}
	}
}