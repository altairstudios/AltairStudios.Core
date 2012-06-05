using System;
using AltairStudios.Core.Orm;


namespace AltairStudios.Core.Orm.Models.I18n {
	public class TranslateItem : Model {
		protected string code;
		protected string domain;
		protected string text;
		protected string pluralText;

		[Templatize]
		public string Code {
			get {
				return code;
			}
			set {
				code = value;
			}
		}	
		
		[Templatize]
		public string Text {
			get {
				return text;
			}
			set {
				text = value;
			}
		}

		[Templatize]
		public string PluralText {
			get {
				return pluralText;
			}
			set {
				pluralText = value;
			}
		}

		public string Domain {
			get {
				return domain;
			}
			set {
				domain = value;
			}
		}
		
		public TranslateItem() {
		}
	}
}