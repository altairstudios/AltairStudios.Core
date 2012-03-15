using System;


namespace AltairStudios.Core.Orm {
	public class TemplatizeAttribute : AltairStudios.Core.Mvc.TemplatizeAttribute {
		protected bool isSubtable = false;
		protected bool isList = false;

		public bool IsList {
			get {
				return this.isList;
			}
			set {
				isList = value;
			}
		}
		
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
		}
		
		public TemplatizeAttribute(bool isSubtable) {
			this.templatize = true;
			this.isSubtable = isSubtable;
		}
		
		public TemplatizeAttribute(bool isSubtable, bool isList) {
			this.templatize = true;
			this.isSubtable = isSubtable;
			this.isList = isList;
		}
	}
}