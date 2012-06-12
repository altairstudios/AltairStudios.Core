using System;
using AltairStudios.Core.Orm;


namespace AltairStudios.Core.Orm.Models {
	/// <summary>
	/// Phone.
	/// </summary>
	public class Phone : Model {
		#region Attributes
		/// <summary>
		/// The identifier.
		/// </summary>
		protected int id;
		/// <summary>
		/// The prefix.
		/// </summary>
		protected string prefix;
		/// <summary>
		/// The number.
		/// </summary>
		protected string number;
		/// <summary>
		/// The extension.
		/// </summary>
		protected string extension;
		#endregion
		
		
		
		
		
		#region Properties
		/// <summary>
		/// Gets or sets the extension.
		/// </summary>
		/// <value>
		/// The extension.
		/// </value>
		[Templatize]
		public string Extension {
			get {
				return this.extension;
			}
			set {
				extension = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		[Templatize]
		public int Id {
			get {
				return this.id;
			}
			set {
				id = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets the number.
		/// </summary>
		/// <value>
		/// The number.
		/// </value>
		[Templatize]
		public string Number {
			get {
				return this.number;
			}
			set {
				number = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets the prefix.
		/// </summary>
		/// <value>
		/// The prefix.
		/// </value>
		[Templatize]
		public string Prefix {
			get {
				return this.prefix;
			}
			set {
				prefix = value;
			}
		}
		#endregion
		
		
		
		
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Orm.Models.Phone"/> class.
		/// </summary>
		public Phone() {
		}
		#endregion
	}
}