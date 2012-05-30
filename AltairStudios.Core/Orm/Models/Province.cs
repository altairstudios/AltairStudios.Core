using System;
using AltairStudios.Core.Orm;


namespace AltairStudios.Core.Orm.Models {
	public class Province {
		#region Attributes
		/// <summary>
		/// The identifier.
		/// </summary>
		protected int id;
		/// <summary>
		/// The name.
		/// </summary>
		protected string name;
		/// <summary>
		/// The region.
		/// </summary>
		protected Region region;
		#endregion
		
		
		
		
		
		#region Properties
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
		
		
		
		/// <summary>
		/// Gets or sets the region.
		/// </summary>
		/// <value>
		/// The region.
		/// </value>
		[Templatize(true)]
		public Region Region {
			get {
				return this.region;
			}
			set {
				region = value;
			}
		}
		#endregion
		
		
		
		
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Orm.Models.Province"/> class.
		/// </summary>
		public Province() {
		}
		#endregion
	}
}