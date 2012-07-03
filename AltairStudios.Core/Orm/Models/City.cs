using System;
using AltairStudios.Core.Orm;


namespace AltairStudios.Core.Orm.Models {
	/// <summary>
	/// City.
	/// </summary>
	public class City : Model {
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
		/// The province.
		/// </summary>
		protected Province province;
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
		/// Gets or sets the province.
		/// </summary>
		/// <value>
		/// The province.
		/// </value>
		[Templatize]
		public Province Province {
			get {
				return this.province;
			}
			set {
				province = value;
			}
		}
		#endregion
		
		
		
		
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Orm.Models.City"/> class.
		/// </summary>
		public City() {
		}
		#endregion
	}
}