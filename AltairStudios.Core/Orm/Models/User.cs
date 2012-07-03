using System;
using AltairStudios.Core.Orm;


namespace AltairStudios.Core.Orm.Models {
	/// <summary>
	/// User.
	/// </summary>
	public class User : Model {
		#region Attributes
		/// <summary>
		/// The identifier.
		/// </summary>
		protected int? id;
		/// <summary>
		/// The email.
		/// </summary>
		protected string email;
		/// <summary>
		/// The password.
		/// </summary>
		protected string password;
		/// <summary>
		/// The remember.
		/// </summary>
		protected bool remember;
		/// <summary>
		/// The name.
		/// </summary>
		protected string name;
		/// <summary>
		/// The surname.
		/// </summary>
		protected string surname;
		#endregion
		
		
		
		
		
		#region Properties
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		[PrimaryKey(true)]
		public int? Id {
			get {
				return this.id;
			}
			set {
				id = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[Templatize]
		public string Name {
			get {
				return this.name;
			}
			set {
				name = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets the surname.
		/// </summary>
		/// <value>
		/// The surname.
		/// </value>
		[Templatize]
		public string Surname {
			get {
				return this.surname;
			}
			set {
				surname = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="AltairStudios.Core.Orm.Models.User"/> is remember.
		/// </summary>
		/// <value>
		/// <c>true</c> if remember; otherwise, <c>false</c>.
		/// </value>
		public bool Remember {
			get {
				return this.remember;
			}
			set {
				remember = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>
		/// The email.
		/// </value>
		[Index]
		public string Email {
			get {
				return this.email;
			}
			set {
				email = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>
		/// The password.
		/// </value>
		public string Password {
			get {
				return this.password;
			}
			set {
				password = value;
			}
		}
		#endregion
		
		
		
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Orm.Models.User"/> class.
		/// </summary>
		public User() {
		}
		#endregion
	}
}