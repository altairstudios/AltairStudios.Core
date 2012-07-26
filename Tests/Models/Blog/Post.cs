using AltairStudios.Core.Orm;
using AltairStudios.Core.Orm.Models;


namespace AltairStudios.Core.Tests.Web.Models.Blog {
	public class Post : Model {
		protected int? id;
		protected string title;
		protected User author;
		protected string content;
		protected ModelList<Category> categories;
		protected ModelList<Tag> tags;
		protected string url;
		
		[PrimaryKey(true)]
		public int? Id {
			get {
				return id;
			}
			set {
				id = value;
			}
		}
		
		[Index]
		public string Title {
			get {
				return title;
			}
			set {
				title = value;
			}
		}
		
		[Templatize]
		public User Author {
			get {
				return author;
			}
			set {
				author = value;
			}
		}
		
		[Templatize]
		public string Content {
			get {
				return content;
			}
			set {
				content = value;
			}
		}
		
		[Templatize]
		public ModelList<Category> Categories {
			get {
				return categories;
			}
			set {
				categories = value;
			}
		}
		
		[Templatize]
		public ModelList<Tag> Tags {
			get {
				return tags;
			}
			set {
				tags = value;
			}
		}
		
		[Index(true)]
		public string Url {
			get {
				return url;
			}
			set {
				url = value;
			}
		}

	}
}