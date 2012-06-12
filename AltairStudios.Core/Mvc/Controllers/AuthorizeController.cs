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
		#region Attributes
		/// <summary>
		/// The access path.
		/// </summary>
		protected string accessPath;
		
		
		
		/// <summary>
		/// The admin path.
		/// </summary>
		protected string adminPath;
		#endregion
		
		
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Mvc.Controllers.AuthorizeController"/> class.
		/// </summary>
		public AuthorizeController() : base() {
			this.accessPath = Url.Action("Desktop", "Admin");
			this.adminPath = Url.Action("Desktop", "Admin");
		}
		#endregion
		
		
		
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
		public virtual ActionResult Login() {
			if(MvcApplication.Configurated == false) {
				return RedirectToAction("Index", "Install");
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
			User authUser;
			
			authUser = this.userAuthentify(user);
			if(authUser == null) {
				authUser = this.genericAuthentify(user);	
			}
			
			if(authUser != null) {
				FormsAuthentication.Initialize();
				FormsAuthenticationTicket fat = new FormsAuthenticationTicket(user.Email, user.Remember, 30);
				Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(fat)));
				Session["admin_user"] = authUser;
				return Content(authUser.ToJson());
			} else {
				return Content("{error:true}");
			}
		}

		
		
		/// <summary>
		/// Users the authentify.
		/// </summary>
		/// <returns>
		/// The authentify.
		/// </returns>
		/// <param name='user'>
		/// User.
		/// </param>
		protected virtual User userAuthentify(User user) {
			ModelList<User> users = new ModelList<User>();
			users = user.getBy<User>();
			
			if(users.Count > 0) {
				return users[0];
			} else {
				return null;
			}
		}
		
		
		
		/// <summary>
		/// Generics the authentify.
		/// </summary>
		/// <returns>
		/// The authentify.
		/// </returns>
		/// <param name='user'>
		/// User.
		/// </param>
		protected virtual User genericAuthentify(User user) {
			if(ConfigurationManager.AppSettings["altairstudios.core.access.user"] != null && ConfigurationManager.AppSettings["altairstudios.core.access.pass"] != null && ConfigurationManager.AppSettings["altairstudios.core.access.user"] == user.Email && ConfigurationManager.AppSettings["altairstudios.core.access.pass"] == user.Password) {
				if(ConfigurationManager.AppSettings["altairstudios.core.access.name"] != null) {
					user.Name = ConfigurationManager.AppSettings["altairstudios.core.access.user"];
				} else {
					user.Name = "Jon Snow";
				}
				
				return user;
			} else {
				return null;
			}
		}
			
		
		
		/// <summary>
		/// Logout current user for admin session.
		/// </summary>
		[Authorize()]
		public ActionResult Logout() {
			Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
			FormsAuthentication.SignOut();
			Session.Clear();
			return RedirectToAction("Login");
		}
		#endregion
	}
}
