using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AltairStudios.Core.Orm;
using AltairStudios.Core.Orm.Models;
using AltairStudios.Core.Orm.Models.Admin;
using AltairStudios.Core.Util;


namespace AltairStudios.Core.Mvc.Controllers {
	
	public class PluginConfigController : Controller {
		
		public ActionResult Index(string id) {
			return Content("");
		}
		
		
	}
}