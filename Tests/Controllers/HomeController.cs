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
			/*Models.TestModel test = new AltairStudios.Core.Tests.Web.Models.TestModel();
			
			test.Id = 1;
			
			AltairStudios.Core.Orm.ModelList<Models.TestModel> result = test.getBy<Models.TestModel>(true);
			
			result[0].Address = result[0].Address.getBy<Address>(true)[0];
			
			return result.ToJson();*/
			/*
			
			Invoices.Models.Contact contact = new Invoices.Models.Contact();
			contact.Id = 1;
			
			return contact.getBy<Invoices.Models.Contact>(true).ToJson();*/
			return View();
		}
	}
}