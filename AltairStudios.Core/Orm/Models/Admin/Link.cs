using System;


namespace AltairStudios.Core.Orm.Models.Admin {
	/// <summary>
	/// Link.
	/// </summary>
	public class Link : Model {
		/// <summary>
		/// The name.
		/// </summary>
		string name;
		/// <summary>
		/// The title.
		/// </summary>
		string title;
		/// <summary>
		/// The anchor.
		/// </summary>
		string anchor;
		
		
		
		/// <summary>
		/// Gets or sets the anchor.
		/// </summary>
		/// <value>
		/// The anchor.
		/// </value>
		[Templatize]
		public string Anchor {
			get {
				return this.anchor;
			}
			set {
				anchor = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[Templatize]
		public string Name {
			get {
				return this.name;
			}
			set {
				name = value;
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
	}
}