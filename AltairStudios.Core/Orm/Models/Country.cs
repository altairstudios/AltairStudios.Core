using System;
using AltairStudios.Core.Orm;


namespace AltairStudios.Core.Orm.Models {
	public class Country : Model {
		#region Attributes
		/// <summary>
		/// The identifier.
		/// </summary>
		protected int id;
		/// <summary>
		/// The code.
		/// </summary>
		protected string code;
		/// <summary>
		/// The name.
		/// </summary>
		protected string name;
		#endregion
		
		
		
		
		
		#region Properties
		/// <summary>
		/// Gets or sets the code.
		/// </summary>
		/// <value>
		/// The code.
		/// </value>
		[Templatize]
		public string Code {
			get {
				return this.code;
			}
			set {
				code = value;
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
		#endregion
		
		
		
		
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Orm.Models.Country"/> class.
		/// </summary>
		public Country() {
		}
		#endregion
	}
}