using System;


namespace AltairStudios.Core.Orm {
	/// <summary>
	/// Primary key attribute.
	/// </summary>
	public class PrimaryKeyAttribute : AltairStudios.Core.Mvc.TemplatizeAttribute {
		#region Fields
		protected bool autoincrement = false;
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