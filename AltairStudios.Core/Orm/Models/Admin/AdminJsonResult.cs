using System;
using AltairStudios.Core.Orm;
using AltairStudios.Core.Orm.Models;
using System.Reflection;


namespace AltairStudios.Core.Orm.Models.Admin {
	/// <summary>
	/// Admin json result.
	/// </summary>
	public class AdminJsonResult<T> : Model {
		/// <summary>
		/// The content.
		/// </summary>
		T content;
		/// <summary>
		/// The sidebar.
		/// </summary>
		Menu sidebar;
		/// <summary>
		/// The notice.
		/// </summary>
		Notice notice;
		
		
		
		/// <summary>
		/// Gets or sets the notice.
		/// </summary>
		/// <value>
		/// The notice.
		/// </value>
		[Templatize]
		public Notice Notice {
			get {
				return this.notice;
			}
			set {
				notice = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		/// <value>
		/// The content.
		/// </value>
		[Templatize]
		public T Content {
			get {
				return this.content;
			}
			set {
				content = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets the sidebar.
		/// </summary>
		/// <value>
		/// The sidebar.
		/// </value>
		[Templatize]
		public Menu Sidebar {
			get {
				return this.sidebar;
			}
			set {
				sidebar = value;
			}
		}
		
		
		
		/// <summary>
		/// Creates the notice.
		/// </summary>
		/// <param name='title'>
		/// Title.
		/// </param>
		/// <param name='text'>
		/// Text.
		/// </param>
		/// <param name='link'>
		/// Link.
		/// </param>
		public void createNotice(string title, string text, Link link) {
			this.createNotice(title, text, link, NoticeType.Information);
		}
		
		
		
		/// <summary>
		/// Creates the notice.
		/// </summary>
		/// <param name='title'>
		/// Title.
		/// </param>
		/// <param name='text'>
		/// Text.
		/// </param>
		/// <param name='link'>
		/// Link.
		/// </param>
		/// <param name='type'>
		/// Type.
		/// </param>
		public void createNotice(string title, string text, Link link, NoticeType type) {
			this.notice = new Notice();
			
			this.notice.Title = title;
			this.notice.Text = text;
			this.notice.Link = link;
			this.notice.Type = type;
		}
	}
}