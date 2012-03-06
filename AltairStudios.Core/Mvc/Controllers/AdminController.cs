using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AltairStudios.Core.Orm.Models;


namespace AltairStudios.Core.Mvc.Controllers {
	public class AdminController : Controller {
		[Authorize()]
		public string Index() {
			StringBuilder html = new StringBuilder();
			string path = MvcApplication.Path;
			
			html.Append("<!DOCTYPE html>\n");
			html.Append("<html lang='es'>");
			html.Append("<head>");
			html.Append("<meta charset='utf-8' />");
			html.Append("<title>AltairStudios.Core - Admin</title>");
			html.Append("<link rel='stylesheet' type='text/css' href='" + path + "/Bin/resources/css/Ext.css' />");
			html.Append("<link rel='stylesheet' type='text/css' href='" + path + "/Bin/resources/css/Desktop.css' />");
			html.Append("<script src='" + path + "/Bin/resources/javascript/Ext.js'></script>");
			html.Append("<script type='text/javascript'>");			
			html.Append("Ext.Loader.setConfig({enabled:true});");
	        html.Append("Ext.Loader.setPath({");
	        html.Append("'Ext.ux.desktop': '" + path + "/Bin/resources/javascript/Ux',");
	        html.Append("'AdminDesktop': '" + path + "/Bin/resources/javascript/Desktop'");
	        html.Append("});");
	
	        html.Append("Ext.require('AdminDesktop.App');");
	        html.Append("var adminDesktop = null;");
	        html.Append("Ext.onReady(function () {");
			html.Append("adminDesktop = new AdminDesktop.App();");
			html.Append("});");
			
			html.Append("</script>");
			html.Append("</head>");
			html.Append("<body>");
			html.Append("</body>");
			html.Append("</html>");

			return html.ToString();
		}
		
		
		public string Login() {
			StringBuilder html = new StringBuilder();
			string path = MvcApplication.Path;
			
			html.Append("<!DOCTYPE html>\n");
			html.Append("<html lang='es'>");
			html.Append("<head>");
			html.Append("<meta charset='utf-8' />");
			html.Append("<title>AltairStudios.Core - Admin</title>");
			html.Append("<link rel='stylesheet' type='text/css' href='" + path + "/Bin/resources/css/Ext.css' />");
			html.Append("<link rel='stylesheet' type='text/css' href='" + path + "/Bin/resources/css/Desktop.css' />");
			html.Append("<script src='" + path + "/Bin/resources/javascript/Ext.js'></script>");
			
			html.Append("<script type='text/javascript'>");			
			html.Append("Ext.Loader.setConfig({enabled:true});");
			
			html.Append("Ext.Loader.setPath({");
	        html.Append("'AdminDesktop.Login': '" + path + "/Bin/resources/javascript/Login'");
	        html.Append("});");
			
	        html.Append("Ext.require('AdminDesktop.Login.App');");
	        html.Append("var adminDesktopLogin = null;");
	        html.Append("Ext.onReady(function () {");
			html.Append("adminDesktopLogin = new AdminDesktop.Login.App();");
			html.Append("adminDesktopLogin.show();");
			html.Append("});");
			
			html.Append("</script>");
			html.Append("</head>");
			html.Append("<body>");
			html.Append("</body>");
			html.Append("</html>");
			
			return html.ToString();
		}
		
			
		public ActionResult Authorize(User user){
			List<User> users = user.getBy<User>();
			
			if (users.Count > 0) {
				FormsAuthentication.Initialize();
				FormsAuthenticationTicket fat = new FormsAuthenticationTicket(user.Email, user.Remember, 30);
				Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(fat)));
				Session["admin_user"] = users[0];
			}
			
			return RedirectToAction("Index");
		}
			
		
		[Authorize()]
		public ActionResult Logout() {
			FormsAuthentication.SignOut();
			return RedirectToAction("Login");
		}
	}
}