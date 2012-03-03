using System;
using System.Web;
using System.Web.Mvc;
using AltairStudios.Core.Tests.Web.Models;


namespace AltairStudios.Core.Tests.Web.Controllers {
	public class SerializerController : Controller {
		public string Index() {
			JsonTestModel model = new JsonTestModel();
			
			
			return model.ToJson();
		}
	}
}