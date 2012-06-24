using System;


namespace AltairStudios.Core.Orm {
	/// <summary>
	/// Primary key attribute.
	/// </summary>
	public class PrimaryKeyAttribute : TemplatizeAttribute {
		#region Fields
		/// <summary>
		/// The autoincrement.
		/// </summary>
		protected bool autoincrement = false;
		#endregion
		
		
		
		#region Properties
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="AltairStudios.Core.Orm.PrimaryKeyAttribute"/> auto increment.
		/// </summary>
		/// <value>
		/// <c>true</c> if auto increment; otherwise, <c>false</c>.
		/// </value>
		public bool AutoIncrement {
			get {
				return this.autoincrement;
			} set {
				this.autoincrement = value;
			}
		}
		#endregion
		
		
		
		#region Contructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Orm.PrimaryKeyAttribute"/> class.
		/// </summary>
		public PrimaryKeyAttribute() {
		}
		
		
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Orm.PrimaryKeyAttribute"/> class.
		/// </summary>
		/// <param name='autoincrement'>
		/// Autoincrement.
		/// </param>
		public PrimaryKeyAttribute(bool autoincrement) {
			this.autoincrement = autoincrement;
		}
		#endregion
	}
}