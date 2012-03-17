using System;


namespace AltairStudios.Core.Mvc {
	/// <summary>
	/// Templatize attribute.
	/// </summary>
	public class TemplatizeAttribute : Attribute {
		/// <summary>
		/// The templatize.
		/// </summary>
		protected bool templatize;
		
		
		
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="AltairStudios.Core.Mvc.TemplatizeAttribute"/> is templatize.
		/// </summary>
		/// <value>
		/// <c>true</c> if templatize; otherwise, <c>false</c>.
		/// </value>
		public bool Templatize {
			get {
				return this.templatize;
			}
			set {
				templatize = value;
			}
		}
		
		
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Mvc.TemplatizeAttribute"/> class.
		/// </summary>
		public TemplatizeAttribute() {
			this.templatize = true;
		}
	}
}