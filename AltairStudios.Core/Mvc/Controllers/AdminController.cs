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
			user = ((User)Session["admin_user"]);
			
			ViewData["models"] = models;
			ViewData["user"] = user;
			
			return View("~/resources/AltairStudios.Core.Views.Admin.Desktop.aspx");
		}
		
		
		
		/// <summary>
		/// Login administration page
		/// </summary>
		public ActionResult Login() {
			if(MvcApplication.Configurated == false) {
				return RedirectToAction("Install");
			}
			
			return View("~/resources/AltairStudios.Core.Views.Admin.Login.aspx");
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
		
		
		
		#region Authentification process
		/// <summary>
		/// Authorize the specified user.
		/// </summary>
		/// <param name='user'>
		/// User to access admin page.
		/// </param>
		public ActionResult Authorize(User user){
			ModelList<User> users = user.getBy<User>();
			
			if (users.Count > 0) {
				FormsAuthentication.Initialize();
				FormsAuthenticationTicket fat = new FormsAuthenticationTicket(user.Email, user.Remember, 30);
				Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(fat)));
				Session["admin_user"] = users[0];
				return Content(users[0].ToJson());
			} else {
				return Content("{error:true}");
			}
		}
			
		
		
		/// <summary>
		/// Logout current user for admin session.
		/// </summary>
		[Authorize()]
		public ActionResult Logout() {
			FormsAuthentication.SignOut();
			return RedirectToAction("Login");
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
		#endregion
	}
}