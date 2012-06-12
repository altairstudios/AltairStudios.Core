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
	/// Admin controller to load automatic administration page.
	/// </summary>
	public class AdminController : Controller {
		#region Html pages
		/// <summary>
		/// Administration page
		/// </summary>
		
		public ActionResult Index() {
			return RedirectToAction("Desktop");
		}
		
		
		
		/// <summary>
		/// Administration page
		/// </summary>
		[Authorize()]
		public ActionResult Desktop() {
			User user;
			ModelList<Orm.Model> models = Reflection.Instance.getTemplatizeModels();
			ModelList<Plugin.PluginBase> plugins = Reflection.Instance.getCorePlugins();
			user = ((User)Session["admin_user"]);
			
			if(user == null) {
				return RedirectToAction("Logout", "Authorize");
			}
			
			ViewData["models"] = models;
			ViewData["plugins"] = plugins;
			ViewData["user"] = user;
			
			return View("~/resources/AltairStudios.Core.Views.Admin.Desktop.aspx");
		}
		
		
		[Authorize()]
		public ActionResult DatabaseSynchronize() {
			ModelList<Orm.Model> models = Reflection.Instance.getTemplatizeModels();
			
			ViewData["models"] = models;
			
			return View("~/resources/AltairStudios.Core.Views.Admin.Synchronize.aspx");
		}
		#endregion
		
		
		
		#region Utils methods
		/// <summary>
		/// Gets the URL.
		/// </summary>
		/// <returns>
		/// The URL.
		/// </returns>
		/// <param name='url'>
		/// Base URL to format link.
		/// </param>
		protected string getUrl(string url) {
			return MvcApplication.Path + "/Admin/Desktop#!/" + url;
		}
		#endregion
		
		
		
		#region Json methods
		[Authorize()]
		public ActionResult SynchronizeModel(string id) {
			Orm.Model model = Reflection.Instance.getModelFromString(id);
			model.query(model.createTable());
			return Content("{'result':true}");
		}
		
		
		/// <summary>
		/// Gets the user information.
		/// </summary>
		/// <returns>
		/// The user.
		/// </returns>
		[Authorize()]
		public ActionResult GetUser() {
			return Content(((User)Session["admin_user"]).ToJson());
		}
		
		
		
		/// <summary>
		/// Gets list of the users.
		/// </summary>
		/// <returns>
		/// The users.
		/// </returns>
		[Authorize()]
		public ActionResult GetUsers() {
			User user = new User();
			ModelList<User> users = user.getBy<User>();
			return Content(users.ToJson());
		}
		
		
		
		/// <summary>
		/// Get home information.
		/// </summary>
		[Authorize()]
		public ActionResult Home() {
			AdminJsonResult<ModelList<User>> result = new AdminJsonResult<ModelList<User>>();
			
			Link noticeLink = new Link();
			noticeLink.Name = "¡Visitanos! &raquo;";
			noticeLink.Title = "Visitanos";
			noticeLink.Anchor = "http://www.altairstudios.es";
			
			result.createNotice("¡Bienvenido!", "Te damos la bienvenida a nuestro administrador. Puedes realizar cualquier operación de una forma sencilla desde cualquier parte del menú. Si quieres saber mas, puedes contactar con nosotros mediante soporte o visitar nuestra web.", noticeLink);
			
			return Content(result.ToJson());
		}
		
		
		
		/// <summary>
		/// Databases the viewer.
		/// </summary>
		/// <returns>
		/// The viewer.
		/// </returns>
		/// <param name='id'>
		/// Identifier.
		/// </param>
		[Authorize()]
		public ActionResult DatabaseViewer(string id) {
			Orm.Model model = Reflection.Instance.getModelFromString(id);
			Orm.ModelList<Orm.Model> models = model.getBy<Orm.Model>();

			AdminJsonResult<Orm.ModelList<Orm.Model>> result = new AdminJsonResult<Orm.ModelList<Orm.Model>>();
			
			result.Content = models;
			return Content(result.ToJson());
		}
				


		[Authorize()]
		public ActionResult DatabaseUpdateData(string id) {
			Model model = Reflection.Instance.getModelFromString(id);
			
			return Content(model.ToJson());
		}
		#endregion
	}
}