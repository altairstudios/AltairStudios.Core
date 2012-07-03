using System;
using AltairStudios.Core.Orm;


namespace AltairStudios.Core.Orm.Models {
	/// <summary>
	/// Region.
	/// </summary>
	public class Region : Model {
		#region Attributes
		/// <summary>
		/// The identifier.
		/// </summary>
		protected int? id;
		/// <summary>
		/// The name.
		/// </summary>
		protected string name;
		/// <summary>
		/// The country.
		/// </summary>
		protected Country country;
		#endregion
		
		
		
		
		
		#region Properties
		/// <summary>
		/// Gets or sets the country.
		/// </summary>
		/// <value>
		/// The country.
		/// </value>
		[Templatize]
		public Country Country {
			get {
				return this.country;
			}
			set {
				country = value;
			}
		}
		
		
		
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
		#endregion
		
		
		
		
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Orm.Models.Region"/> class.
		/// </summary>
		public Region() {
		}
		#endregion
	}
}