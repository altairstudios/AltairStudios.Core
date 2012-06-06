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
			} else if(string.IsNullOrEmpty(language) && Session["language"] == null) {
				Session["language"] = "en";
				language = "";
			}
			
			ViewData["language"] = language;
			System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.GetCultureInfo(Session["language"].ToString());
			ViewData["language_name"] = cul.TextInfo.ToTitleCase(cul.NativeName);
			
			return View("~/resources/AltairStudios.Core.Views.Install.Language.aspx");
		}
		
		
		public ActionResult Database() {
			return View("~/resources/AltairStudios.Core.Views.Install.Database.aspx");
		}
		#endregion		
	}
}