using System;


namespace AltairStudios.Core.Orm {
	public class EncryptedAttribute : TemplatizeAttribute {
		protected EncryptationType method;

		public EncryptationType Method {
			get {
				return method;
			}
			set {
				method = value;
			}
		}
		
		public EncryptedAttribute() : this(EncryptationType.MD5) {
		}
		
		
		public EncryptedAttribute(EncryptationType type) {
			this.method = type;
			this.templatize = true;
		}
	}
}