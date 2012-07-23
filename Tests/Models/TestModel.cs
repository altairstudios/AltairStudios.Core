using System;
using AltairStudios.Core.Orm;
using AltairStudios.Core.Orm.Models;


namespace AltairStudios.Core.Tests.Web.Models {
	public class TestModel : Orm.Model {
		protected int? id;
		protected string name;
		protected Orm.ModelList<int> intList;
		protected Orm.ModelList<User> userList;
		protected Address address;
		protected string passwordMd5;
		protected string passwordSha1;
		
		[PrimaryKey(true)]
		public int? Id {
			get {
				return this.id;
			}
			set {
				id = value;
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

		[Templatize]
		public Address Address {
			get {
				return address;
			}
			set {
				address = value;
			}
		}
		
		[Encrypted(EncryptationType.MD5)]
		public string PasswordMd5 {
			get {
				return passwordMd5;
			}
			set {
				passwordMd5 = value;
			}
		}
		
		[Encrypted(EncryptationType.SHA1)]
		public string PasswordSha1 {
			get {
				return passwordSha1;
			}
			set {
				passwordSha1 = value;
			}
		}
	}
}