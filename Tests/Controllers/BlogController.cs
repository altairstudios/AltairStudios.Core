using System.Web.Mvc;
using AltairStudios.Core.Orm;
using AltairStudios.Core.Tests.Web.Models.Blog;


namespace AltairStudios.Core.Tests.Web.Controllers {
	public class BlogController : Controller {
		protected ModelList<Category> categories;
		ModelList<Tag> tags;
		
		protected BlogController() {
			Category category = new Category();
			Tag tag = new Tag();

			this.categories = category.getBy<Category>();
			this.tags = tag.getBy<Tag>();
			
			ViewData["categories"] = this.categories;
			ViewData["tags"] = this.tags;
		}
		
		public ActionResult Index() {
			Post post = new Post();
			ModelList<Post> posts = post.getBy<Post>(true);
			
			ViewData["posts"] = posts;
		
			return View();
		}
		
		public ActionResult Post() {
			return View();
		}
		
		public ActionResult Category() {
			return View();
		}
		
		public ActionResult Tag() {
			return View();
		}
	}
}