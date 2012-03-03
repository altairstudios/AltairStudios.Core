using System;


namespace AltairStudios.Core.Orm {
	public class TemplatizeAttribute : AltairStudios.Core.Mvc.TemplatizeAttribute {
		protected bool isSubtable;
		
		public bool IsSubtable {
			get {
				return this.isSubtable;
			}
			set {
				isSubtable = value;
			}
		}
		
		public TemplatizeAttribute() {
			this.templatize = true;
			this.isSubtable = false;
		}
		
		public TemplatizeAttribute(bool templatize, bool isSubtable) {
			this.templatize = templatize;
			this.isSubtable = isSubtable;
		}
	}
}

