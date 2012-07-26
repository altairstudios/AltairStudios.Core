using System.Web.Mvc;
using AltairStudios.Core.Orm;
using AltairStudios.Core.Tests.Web.Models.Blog;


namespace AltairStudios.Core.Tests.Web.Controllers {
	public class BlogController : Controller {		
		public ActionResult Index() {
			this.loadCommonData();
			
			Post post = new Post();
			ModelList<Post> posts = post.getBy<Post>(true);
			
			ViewData["posts"] = posts;
		
			return View();
		}
		
		public ActionResult Post(string id) {
			this.loadCommonData();
			
			Post post = new Post();
			post.Url = id;
			ModelList<Post> posts = post.getBy<Post>(true);
			
			ViewData["post"] = posts[0];
			
			return View();
		}
		
		public ActionResult Category(string id) {
			this.loadCommonData();
			
			Category category = new Category();
			Post post = new Post();
			
			if(id != "") {
				category.Url = id;
				post.Categories = new ModelList<Category>();
				post.Categories.Add(category);
			}
			
			ModelList<Post> posts = post.getBy<Post>(true);
			
			ViewData["posts"] = posts;
			
			return View();
		}
		
		public ActionResult Tag(string id) {
			this.loadCommonData();
			
			Tag tag = new Tag();
			Post post = new Post();
			
			if(id != "") {
				tag.Url = id;
				post.Tags = new ModelList<Tag>();
				post.Tags.Add(tag);
			}
			
			ModelList<Post> posts = post.getBy<Post>(true);
			
			ViewData["posts"] = posts;
			
			return View();
		}
		
		protected void loadCommonData() {
			Category category = new Category();
			Tag tag = new Tag();

			ModelList<Category> categories = category.getBy<Category>();
			ModelList<Tag> tags = tag.getBy<Tag>();
			
			ViewData["categories"] = categories;
			ViewData["tags"] = tags;
		}
	}
}