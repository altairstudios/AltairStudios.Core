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
	/// Authorize controller.
	/// </summary>
	public class AuthorizeController : Controller {
		#region Html pages
		/// <summary>
		/// Administration page
		/// </summary>
		public ActionResult Index() {
			return RedirectToAction("Login");
		}
		
		
		
		/// <summary>
		/// Login administration page
		/// </summary>
		public ActionResult Login() {
			if(MvcApplication.Configurated == false) {
				return RedirectToAction("Install");
			}
			
			return View("~/resources/AltairStudios.Core.Views.Authorize.Login.aspx");
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
			ModelList<User> users = new ModelList<User>();
			
			if(ConfigurationManager.AppSettings["altairstudios.core.access.user"] != null && ConfigurationManager.AppSettings["altairstudios.core.access.pass"] != null && ConfigurationManager.AppSettings["altairstudios.core.access.user"] == user.Email && ConfigurationManager.AppSettings["altairstudios.core.access.pass"] == user.Password) {
				if(ConfigurationManager.AppSettings["altairstudios.core.access.name"] != null) {
					user.Name = ConfigurationManager.AppSettings["altairstudios.core.access.user"];
				} else {
					user.Name = "Jon Snow";
				}
				
				users.Add(user);
			} else {
				users = user.getBy<User>();
			}
			
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
			Session.Clear();
			return RedirectToAction("Login");
		}
		#endregion
	}
}