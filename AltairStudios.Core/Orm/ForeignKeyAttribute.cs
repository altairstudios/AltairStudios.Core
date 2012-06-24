using System;


namespace AltairStudios.Core.Orm {
	/// <summary>
	/// Index attribute.
	/// </summary>
	public class ForeignKeyAttribute : TemplatizeAttribute {
		#region Fields
		#endregion
		
		

		#region Properties
		#endregion
		
		
		
		#region Contructors
		public ForeignKeyAttribute() {
		
		}
		
		public ForeignKeyAttribute(ForeignKeyTypes type) {
		
		}
		
		public ForeignKeyAttribute(ForeignKeyTypes type, ForeignKeyActions action) {
		
		}
		#endregion
	}
}