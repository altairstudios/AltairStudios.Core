using System;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Web.Handlers;
using System.Web.UI;
using AltairStudios.Core.Orm.Providers;
using AltairStudios.Core.Orm.Models;

namespace AltairStudios.Core.Tests.Web.Controllers {
	[HandleError]
	public class HomeController : Controller {
		public ActionResult Index() {
			return View();
		}
	}
}