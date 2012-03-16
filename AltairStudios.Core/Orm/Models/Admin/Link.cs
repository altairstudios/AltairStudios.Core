using System;


namespace AltairStudios.Core.Orm.Models.Admin {
	public class Link : Model {
		string name;
		string title;
		string anchor;
		
		[Templatize]
		public string Anchor {
			get {
				return this.anchor;
			}
			set {
				anchor = value;
			}
		}
		
		[Templatize]
		public string Name {
			get {
				return this.name;
			}
			set {
				name = value;
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
	}
}