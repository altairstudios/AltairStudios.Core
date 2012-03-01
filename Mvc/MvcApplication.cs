using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace AltairStudios.Core.Mvc {
	public class MvcApplication : System.Web.HttpApplication {
		protected static string path = "";
		protected static string diskPath = "";
		protected static string connectionString = "";

		public static string ConnectionString {
			get {
				return MvcApplication.connectionString;
			}
		}		
		
		public static string DiskPath {
			get {
				return MvcApplication.diskPath;
			}
		}

		public static string Path {
			get {
				return MvcApplication.path;
			}
		}		
		
		
		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute ("{resource}.axd/{*pathInfo}");
			//routes.MapRoute("Accounts", "Accounts/{action}/{id}", new { controller = "Accounts", action = "Index", id = "" });
			routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = "" });
		}
		

		protected void Application_Start() {
			RegisterRoutes(RouteTable.Routes);
			
			MvcApplication.diskPath = HttpContext.Current.Server.MapPath("~");
			if(System.Web.HttpContext.Current.Request.ApplicationPath != "/") {
				MvcApplication.path = System.Web.HttpContext.Current.Request.ApplicationPath;
			}

			MvcApplication.connectionString = ConfigurationManager.ConnectionStrings["MySqlServerConnection"].ConnectionString;
		}
	}
}