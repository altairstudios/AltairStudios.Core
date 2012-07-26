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
		
		public ActionResult Post() {
			this.loadCommonData();
			
			return View();
		}
		
		public ActionResult Category() {
			this.loadCommonData();
			
			return View();
		}
		
		public ActionResult Tag() {
			this.loadCommonData();
			
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