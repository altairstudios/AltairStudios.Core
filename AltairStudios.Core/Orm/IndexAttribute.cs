using System;


namespace AltairStudios.Core.Orm {
	/// <summary>
	/// Index attribute.
	/// </summary>
	public class IndexAttribute : AltairStudios.Core.Mvc.TemplatizeAttribute {
		#region Fields
		/// <summary>
		/// The unique.
		/// </summary>
		protected bool unique = false;
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