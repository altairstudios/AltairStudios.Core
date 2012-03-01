using System;


namespace AltairStudios.Core.Orm {
	public class TemplatizeAttribute : Attribute {
		protected bool isSubtable;
		protected bool templatize;

		public bool IsSubtable {
			get {
				return this.isSubtable;
			}
			set {
				isSubtable = value;
			}
		}

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
			this.isSubtable = false;
		}
		
		public TemplatizeAttribute(bool templatize) {
			this.templatize = templatize;
			this.isSubtable = false;
		}
		
		public TemplatizeAttribute(bool templatize, bool isSubtable) {
			this.templatize = templatize;
			this.isSubtable = isSubtable;
		}
	}
}

