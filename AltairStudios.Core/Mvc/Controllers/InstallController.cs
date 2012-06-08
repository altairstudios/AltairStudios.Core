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
		
		
		public ActionResult Database(string dbtype, string server, string database, string user, string password) {
			string connection = "";
			string provider = "";
			
			if(!string.IsNullOrEmpty(dbtype)) {
				if(dbtype == "sqlserver") {
					provider = "System.Data.SqlClient";
					connection = "data source=" + server + ";initial catalog=" + database + ";user id=" + user + ";pwd=" + password + ";Pooling=true;Min Pool Size=0;Max Pool Size=100;";
				} else if(dbtype == "mysql") {
					provider = "MySql.Data.MySqlClient";
					connection = "Datasource=" + server + ";Database=" + database + ";uid=" + user + ";pwd=" + password + ";Pooling=true;Min Pool Size=0;Max Pool Size=100;";
				}
				
				Session["install-config-provider"] = provider;
				Session["install-config-connection"] = connection;
				
				return RedirectToAction("Admin");
			}
			
			return View("~/resources/AltairStudios.Core.Views.Install.Database.aspx");
		}
		
		
		public ActionResult Admin(string user, string password) {
			if(!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(password)) {
				Session["install-config-admin-user"] = user;
				Session["install-config-admin-password"] = password;
				
				return RedirectToAction("Finish");
			}
			
			return View("~/resources/AltairStudios.Core.Views.Install.Admin.aspx");
		}
		
		
		
		public ActionResult Finish() {   
			string user = Session["install-config-admin-user"].ToString();
			string password = Session["install-config-admin-password"].ToString();
			string provider = Session["install-config-provider"].ToString();
			string connection = Session["install-config-connection"].ToString();
			
			ViewData["admin-user"] = "&lt;add key=\"altairstudios.core.access.user\" value=\"" + user + "\" /&gt;";
			ViewData["admin-password"] = "&lt;add key=\"altairstudios.core.access.pass\" value=\"" + password + "\" /&gt;";
			ViewData["admin-name"] = "&lt;add key=\"altairstudios.core.access.name\" value=\"John Snow\" /&gt;";
			ViewData["admin-connectionstring"] = "&lt;add name=\"SqlServerConnection\" connectionString=\"" + connection + "\" providerName=\"" + provider + "\" /&gt;";
			
			return View("~/resources/AltairStudios.Core.Views.Install.Finish.aspx");
		}
		#endregion		
	}
}