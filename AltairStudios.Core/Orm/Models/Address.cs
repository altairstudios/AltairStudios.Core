using System;
using AltairStudios.Core.Orm;


namespace AltairStudios.Core.Orm.Models {
	public class Address : Model {
		#region Attributes
		/// <summary>
		/// The identifier.
		/// </summary>
		protected int id;
		/// <summary>
		/// The type.
		/// </summary>
		protected int type;
		/// <summary>
		/// The street.
		/// </summary>
		protected string street;
		/// <summary>
		/// The number.
		/// </summary>
		protected string number;
		/// <summary>
		/// The block.
		/// </summary>
		protected string block;
		/// <summary>
		/// The floor.
		/// </summary>
		protected string floor;
		/// <summary>
		/// The stair.
		/// </summary>
		protected string stair;
		/// <summary>
		/// The zip.
		/// </summary>
		protected string zip;
		/// <summary>
		/// The city.
		/// </summary>
		protected City city;
		#endregion
		
		
		
		
		
		#region Properties
		/// <summary>
		/// Gets or sets the block.
		/// </summary>
		/// <value>
		/// The block.
		/// </value>
		[Templatize]
		public string Block {
			get {
				return this.block;
			}
			set {
				block = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets the city.
		/// </summary>
		/// <value>
		/// The city.
		/// </value>
		[Templatize(true)]
		public City City {
			get {
				return this.city;
			}
			set {
				city = value;
			}
		}

		
		
		/// <summary>
		/// Gets or sets the floor.
		/// </summary>
		/// <value>
		/// The floor.
		/// </value>
		[Templatize]
		public string Floor {
			get {
				return this.floor;
			}
			set {
				floor = value;
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
		/// Gets or sets the stair.
		/// </summary>
		/// <value>
		/// The stair.
		/// </value>
		[Templatize]
		public string Stair {
			get {
				return this.stair;
			}
			set {
				stair = value;
			}
		}

		
		
		/// <summary>
		/// Gets or sets the street.
		/// </summary>
		/// <value>
		/// The street.
		/// </value>
		[Templatize]
		public string Street {
			get {
				return this.street;
			}
			set {
				street = value;
			}
		}

		
		
		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		[Templatize]
		public int Type {
			get {
				return this.type;
			}
			set {
				type = value;
			}
		}

		
		
		/// <summary>
		/// Gets or sets the zip.
		/// </summary>
		/// <value>
		/// The zip.
		/// </value>
		[Templatize]
		public string Zip {
			get {
				return this.zip;
			}
			set {
				zip = value;
			}
		}
		#endregion
		
		
		
		
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Orm.Models.Address"/> class.
		/// </summary>
		public Address() {
		}
		#endregion
	}
}