using System;
//using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AltairStudios.Core.Orm.Models;
using AltairStudios.Core.Orm.Models.Admin;


namespace AltairStudios.Core.Mvc.Controllers {
	/// <summary>
	/// Admin controller to load automatic administration page.
	/// </summary>
	public class AdminController : Controller {
		#region Html pages
		/// <summary>
		/// Administration page
		/// </summary>
		[Authorize()]
		public ActionResult Index() {
			return RedirectToAction("Desktop");
		}
		
		
		
		/// <summary>
		/// Administration page
		/// </summary>
		[Authorize()]
		public ActionResult Desktop() {
			StringBuilder html = new StringBuilder();
			string path;
			string min = "";
			User user;

			user = ((User)Session["admin_user"]);
			path = MvcApplication.Path;
			
			html.Append("<!DOCTYPE html>\n");
			html.Append("<html lang='es'>");
			html.Append("<head>");
			html.Append("<meta charset='utf-8' />");
			html.Append("<title>Administrator</title>");
			html.Append("<meta name='viewport' content='width=device-width, initial-scale=1.0' />");
			html.Append("<meta name='description' content='AltairStudios.Core - Administration' />");
			html.Append("<meta name='author' content='Altair Studios' />");
			
			html.Append("<link rel='stylesheet' type='text/css' href='" + path + "/Bin/resources/css/bootstrap" + min + ".css' />");
			html.Append("<link rel='stylesheet' type='text/css' href='" + path + "/Bin/resources/css/bootstrap-responsive" + min + ".css' />");
			
			html.Append("<style type='text/css'>");
			html.Append("body {");
			html.Append("padding-top: 60px;");
			html.Append("padding-bottom: 40px;");
			html.Append("}");
			html.Append(".sidebar-nav {");
			html.Append(" padding: 9px 0;");
			html.Append("}");
			html.Append("</style>");
			
			html.Append("<!--[if lt IE 9]>");
			html.Append("<script src='http://html5shim.googlecode.com/svn/trunk/html5.js'></script>");
			html.Append("<![endif]-->");
			
			html.Append("</head>");
			html.Append("<body>");
			
			html.Append("<div id='navbar' class='navbar navbar-fixed-top'>");
			html.Append("<div class='navbar-inner'>");
			html.Append("<div class='container-fluid'>");
			html.Append("<a class='btn btn-navbar' data-toggle='collapse' data-target='.nav-collapse'>");
			html.Append("<span class='icon-bar'></span>");
			html.Append("<span class='icon-bar'></span>");
			html.Append("<span class='icon-bar'></span>");
			html.Append("</a>");
			html.Append("<a class='brand' href='#'>Administrador</a>");
			html.Append("<div class='nav-collapse'>");
			html.Append("<ul class='nav'>");
			html.Append("<li class='active'><a href='" + this.getUrl("home") + "'>Home</a></li>");
			html.Append("<li><a href='" + this.getUrl("get-users") + "'>Usuarios</a></li>");
			html.Append("</ul>");
			
			html.Append("<p class='navbar-text pull-right'>Loggeado como <a href='" + this.getUrl("logout") + "'>" + user.Name + " " + user.Surname + "</a></p>");
			
			html.Append("</div><!--/.nav-collapse -->");
	        html.Append("</div>");
			html.Append("</div>");
			html.Append("</div>");
		
			html.Append("<div class='container-fluid'>");
			html.Append("<div class='row-fluid' id='content'>");
			
			html.Append("</div><!--/row-->");
			
			html.Append("<hr>");
			
			html.Append("<footer>");
			html.Append("<p>© Altair Studios 2012</p>");
			html.Append("</footer>");

			html.Append("</div>");
			
			html.Append("<script type='text/javascript'>var coreProcess = null;var AltairStudios = { Core: { Admin: { Plugins: {} } } }; var path = '" + path + "';</script>");
			
			html.Append("<script type='text/javascript' src='https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery" + min + ".js'></script>");
			html.Append("<script type='text/javascript' src='" + path + "/Bin/resources/js/bootstrap" + min + ".js'></script>");
			
			html.Append("<script type='text/javascript' src='" + path + "/Bin/resources/js/admin/renderer.js'></script>");
			html.Append("<script type='text/javascript' src='" + path + "/Bin/resources/js/admin/core.js'></script>");
			
			
			html.Append("<script type='text/javascript'>");
			html.Append("var uvOptions = {};");
			html.Append("(function() {");
			html.Append("var uv = document.createElement('script'); uv.type = 'text/javascript'; uv.async = true;");
			html.Append("uv.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + 'widget.uservoice.com/G4Ce3YuUFULbfnRobWXGQ.js';");
			html.Append("var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(uv, s);");
			html.Append("})();");
			html.Append("</script>");
			
			
			html.Append("</body>");
			html.Append("</html>");
			
			return Content(html.ToString());
		}
		
		
		
		/// <summary>
		/// Login administration page
		/// </summary>
		public ActionResult Login() {
			if(MvcApplication.Configurated == false) {
				return RedirectToAction("Install");
			}
		
			StringBuilder html = new StringBuilder();
			string path = MvcApplication.Path;
			string min = "";
			
			html.Append("<!DOCTYPE html>\n");
			html.Append("<html lang='es'>");
			html.Append("<head>");
			html.Append("<meta charset='utf-8' />");
			html.Append("<title>Administrator</title>");
			html.Append("<meta name='viewport' content='width=device-width, initial-scale=1.0' />");
			html.Append("<meta name='description' content='AltairStudios.Core - Administration module' />");
			html.Append("<meta name='author' content='Altair Studios' />");
			
			html.Append("<link rel='stylesheet' type='text/css' href='" + path + "/Bin/resources/css/bootstrap" + min + ".css' />");
			html.Append("<link rel='stylesheet' type='text/css' href='" + path + "/Bin/resources/css/bootstrap-responsive" + min + ".css' />");
			
			html.Append("<style type='text/css'>");
			html.Append("body {");
			html.Append("padding-top: 60px;");
			html.Append("padding-bottom: 40px;");
			html.Append("}");
			html.Append(".sidebar-nav {");
			html.Append(" padding: 9px 0;");
			html.Append("}");
			html.Append("</style>");
			
			html.Append("<!--[if lt IE 9]>");
			html.Append("<script src='http://html5shim.googlecode.com/svn/trunk/html5.js'></script>");
			html.Append("<![endif]-->");
			
			html.Append("</head>");
			html.Append("<body>");
			
			html.Append("<div class='container-fluid'>");
			html.Append("<section>");
			
			html.Append("<div class='row-fluid'>");
			html.Append("<div class='span4'>&nbsp;</div><div class='span4'>");
			html.Append("<form><fieldset><legend>Acceder</legend>");
			
			html.Append("<div class='control-group'><label class='control-label' for='loginUser'>Usuario:</label><div class='controls'><input name='Email' class='input-xlarge focused' id='loginUser' type='text' placeholder='Email' autocomplete='off'></div></div>");
			html.Append("<div class='control-group'><label class='control-label' for='loginPassword'>Contraseña:</label><div class='controls'><input name='Password' class='input-xlarge focused' id='loginPassword' type='password' placeholder='Contraseña' autocomplete='off'></div></div>");
			html.Append("<div class='control-group form-inline'><label class='control-label' for='loginRemember'>Recordar: <input name='Remember' class='input-small' id='loginRemember' type='checkbox' /></label></div>");
			html.Append("<div class='control-group'><a id='loginButton' class='btn btn-primary' href='#'><i class='icon-off icon-white'></i> Acceder</a></div>");
						
			html.Append("</fieldset></form>");
			
			html.Append(" </div><div class='span4'>&nbsp;</div>");
			html.Append("</div>");  
  
			html.Append("</section>");
			html.Append("</div>");

			html.Append("<script type='text/javascript' src='https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery" + min + ".js'></script>");
			html.Append("<script type='text/javascript' src='" + path + "/Bin/resources/js/bootstrap" + min + ".js'></script>");
			html.Append("<script type='text/javascript' src='" + path + "/Bin/resources/js/login.js'></script>");
			
			html.Append("</body>");
			html.Append("</html>");
			
			return Content(html.ToString());
		}
		
		
		
		/// <summary>
		/// Install this instance.
		/// </summary>
		public ActionResult Install() {
			MvcApplication.loadConfiguration();
			StringBuilder html = new StringBuilder();
			string path = MvcApplication.Path;
			string min = "";
			
			html.Append("<!DOCTYPE html>\n");
			html.Append("<html lang='es'>");
			html.Append("<head>");
			html.Append("<meta charset='utf-8' />");
			html.Append("<title>Instalador</title>");
			html.Append("<meta name='viewport' content='width=device-width, initial-scale=1.0' />");
			html.Append("<meta name='description' content='AltairStudios.Core - Administration module' />");
			html.Append("<meta name='author' content='Altair Studios' />");
			
			html.Append("<link rel='stylesheet' type='text/css' href='" + path + "/Bin/resources/css/bootstrap" + min + ".css' />");
			html.Append("<link rel='stylesheet' type='text/css' href='" + path + "/Bin/resources/css/bootstrap-responsive" + min + ".css' />");
			
			html.Append("<style type='text/css'>");
			html.Append("body {");
			html.Append("padding-top: 60px;");
			html.Append("padding-bottom: 40px;");
			html.Append("}");
			html.Append(".sidebar-nav {");
			html.Append(" padding: 9px 0;");
			html.Append("}");
			html.Append("</style>");
			
			html.Append("<!--[if lt IE 9]>");
			html.Append("<script src='http://html5shim.googlecode.com/svn/trunk/html5.js'></script>");
			html.Append("<![endif]-->");
			
			html.Append("</head>");
			html.Append("<body>");
			
			html.Append("<div class='container-fluid'>");
			html.Append("<section>");
			
			html.Append("<div class='row-fluid'>");
			html.Append("<div class='span4'>&nbsp;</div><div class='span4'>");
			html.Append("<form><fieldset><legend>Instalar</legend>");
			
			html.Append("<div class='control-group'><label class='control-label' for='loginUser'>Añade la siguiente linea al webconfig:</label><label class='control-label' for='loginUser'>&lt;connectionStrings&gt;<br/>&nbsp;&nbsp;&nbsp;&nbsp;&lt;add name=\"SqlServerConnection\" connectionString=\"Datasource=[server];Database=[database];uid=[user];pwd=[password];Pooling=true;Min Pool Size=0;Max Pool Size=100;\" providerName=\"MySql.Data.MySqlClient\"/&gt;<br/>&lt;/connectionStrings&gt;</label></div>");
			
			html.Append("</fieldset></form>");
			
			html.Append(" </div><div class='span4'>&nbsp;</div>");
			html.Append("</div>");  
  
			html.Append("</section>");
			html.Append("</div>");

			html.Append("<script type='text/javascript' src='https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery" + min + ".js'></script>");
			html.Append("<script type='text/javascript' src='" + path + "/Bin/resources/js/bootstrap" + min + ".js'></script>");
			
			html.Append("</body>");
			html.Append("</html>");
			
			return Content(html.ToString());
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
			/*User user = new User();
			ModelList<User> users = user.getBy<User>();*/
			
			AdminJsonResult<ModelList<User>> result = new AdminJsonResult<ModelList<User>>();
			//result.Content = users;
			
			Link noticeLink = new Link();
			noticeLink.Name = "¡Visitanos! &raquo;";
			noticeLink.Title = "Visitanos";
			noticeLink.Anchor = "http://www.altairstudios.es";
			
			result.createNotice("¡Bienvenido!", "Te damos la bienvenida a nuestro administrador. Puedes realizar cualquier operación de una forma sencilla desde cualquier parte del menú. Si quieres saber mas, puedes contactar con nosotros mediante soporte o visitar nuestra web.", noticeLink);
			
			return Content(result.ToJson());
		}
		#endregion
	}
}