using System;


namespace AltairStudios.Core.Orm {
	/// <summary>
	/// Index attribute.
	/// </summary>
	public class IndexAttribute : TemplatizeAttribute {
		#region Fields
		/// <summary>
		/// The unique.
		/// </summary>
		protected bool unique = false;
		#endregion
		
		

		#region Properties
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="AltairStudios.Core.Orm.IndexAttribute"/> is unique.
		/// </summary>
		/// <value>
		/// <c>true</c> if unique; otherwise, <c>false</c>.
		/// </value>
		public bool Unique {
			get {
				return unique;
			}
			set {
				unique = value;
			}
		}
		#endregion
		
		
		
		#region Contructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Orm.IndexAttribute"/> class.
		/// </summary>
		public IndexAttribute() {
		}
		
		
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Orm.IndexAttribute"/> class.
		/// </summary>
		/// <param name='unique'>
		/// Unique.
		/// </param>
		public IndexAttribute(bool unique) {
			this.unique = unique;
		}
		#endregion
	}
}