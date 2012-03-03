using System;
using System.Web;
using System.Web.Mvc;


namespace AltairStudios.Core.Tests.Web.Controllers {
	[HandleError]
	public class HomeController : Controller {
		public ActionResult Index() {
			return View();
		}
	}
}