using System;


namespace AltairStudios.Core.Orm.Models.Admin {
	public class Notice : Model {
		string title;
		string text;
		Link link;
		NoticeType type;
		
		[Templatize(true)]
		public Link Link {
			get {
				return this.link;
			}
			set {
				link = value;
			}
		}
		
		[Templatize]
		public string Text {
			get {
				return this.text;
			}
			set {
				text = value;
			}
		}
		
		[Templatize]
		public string Title {
			get {
				return this.title;
			}
			set {
				title = value;
			}
		}
		
		[Templatize]
		public NoticeType Type {
			get {
				return this.type;
			}
			set {
				type = value;
			}
		}
	}
}