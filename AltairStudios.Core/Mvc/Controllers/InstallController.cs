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
	/// <summary>
	/// Install controller.
	/// </summary>
	public class InstallController : Controller {
		#region Html pages
		/// <summary>
		/// Administration page
		/// </summary>
		
		public ActionResult Index() {
			return RedirectToAction("Language");
		}
		
		
		
		public ActionResult Language(string language) {
			if(!string.IsNullOrEmpty(language)) {
				Session["language"] = language;
			}
			return View("~/resources/AltairStudios.Core.Views.Install.Language.aspx");
		}
		
		
		
		/// <summary>
		/// Install this instance.
		/// </summary>
		public ActionResult Install() {
			MvcApplication.loadConfiguration();
			ModelList<Orm.Model> models = Reflection.Instance.getTemplatizeModels();
			
			ViewData["models"] = models;
			
			return View("~/resources/AltairStudios.Core.Views.Admin.Install.aspx");
		}
		#endregion		
	}
}