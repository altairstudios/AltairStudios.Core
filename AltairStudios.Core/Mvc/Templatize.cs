using System;


namespace AltairStudios.Core.Mvc {
	public class TemplatizeAttribute : Attribute {
		protected bool templatize;
		
		public bool Templatize {
			get {
				return this.templatize;
			}
			set {
				templatize = value;
			}
		}
		
		public TemplatizeAttribute() {
			this.templatize = true;
		}
	}
}