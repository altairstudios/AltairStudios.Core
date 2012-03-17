using System;


namespace AltairStudios.Core.Orm.Models.Admin {
	/// <summary>
	/// Notice.
	/// </summary>
	public class Notice : Model {
		/// <summary>
		/// The title.
		/// </summary>
		string title;
		/// <summary>
		/// The text.
		/// </summary>
		string text;
		/// <summary>
		/// The link.
		/// </summary>
		Link link;
		/// <summary>
		/// The type.
		/// </summary>
		NoticeType type;
		
		
		
		/// <summary>
		/// Gets or sets the link.
		/// </summary>
		/// <value>
		/// The link.
		/// </value>
		[Templatize(true)]
		public Link Link {
			get {
				return this.link;
			}
			set {
				link = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		[Templatize]
		public string Text {
			get {
				return this.text;
			}
			set {
				text = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		[Templatize]
		public string Title {
			get {
				return this.title;
			}
			set {
				title = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
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