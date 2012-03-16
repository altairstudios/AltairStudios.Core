using System;
using AltairStudios.Core.Orm;
using AltairStudios.Core.Orm.Models;
using System.Reflection;


namespace AltairStudios.Core.Orm.Models.Admin {
	public class AdminJsonResult<T> : Model {
		T content;
		Menu sidebar;
		Notice notice;
		
		[Templatize(true)]
		public Notice Notice {
			get {
				return this.notice;
			}
			set {
				notice = value;
			}
		}
		
		[Templatize(true, true)]
		public T Content {
			get {
				return this.content;
			}
			set {
				content = value;
			}
		}

		[Templatize(true)]
		public Menu Sidebar {
			get {
				return this.sidebar;
			}
			set {
				sidebar = value;
			}
		}
		
		
		public void createNotice(string title, string text, Link link) {
			this.createNotice(title, text, link, NoticeType.Information);
		}
		
		
		public void createNotice(string title, string text, Link link, NoticeType type) {
			this.notice = new Notice();
			
			this.notice.Title = title;
			this.notice.Text = text;
			this.notice.Link = link;
			this.notice.Type = type;
		}
	}
}