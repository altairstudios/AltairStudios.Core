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
			
			routes.MapRoute("Admin", "Admin/{action}", new { controller = "Admin", action = "Index", id = "" });
			
			/*routes.MapRoute("AdminDesktopJavascript", "Admin/Resources/Javascript/Desktop/{path}", new { controller = "Admin", action = "JavascriptDesktop", path = "" });
			routes.MapRoute("AdminUxJavascript", "Admin/Resources/Javascript/Ux/{path}", new { controller = "Admin", action = "JavascriptUx", path = "" });
			routes.MapRoute("AdminImageDesktop", "Admin/Resources/Images/Desktop/{path}", new { controller = "Admin", action = "ImageDesktop", path = "" });
			routes.MapRoute("AdminThemes", "Admin/resources/themes/images/{theme}/{pack}/{image}", new { controller = "Admin", action = "ThemeImage", theme = "", pack = "", image = "" });
			routes.MapRoute("AdminThemesSublevel", "Admin/resources/themes/images/{theme}/{pack}/{sublevel}/{image}", new { controller = "Admin", action = "ThemeImageSublevel", theme = "", pack = "", sublevel = "", image = "" });*/
			//routes.MapRoute("AdminResources", "Admin/Resources/{action}/{path}", new { controller = "Admin", action = "Index", path = "" });
			
			routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = "" });
		}


		protected void Application_Start() {
			RegisterRoutes(RouteTable.Routes);
			
			
			MvcApplication.diskPath = HttpContext.Current.Server.MapPath("~");
			if(System.Web.HttpContext.Current.Request.ApplicationPath != "/") {
				MvcApplication.path = System.Web.HttpContext.Current.Request.ApplicationPath;
			}
			
			if(ConfigurationManager.ConnectionStrings["SqlServerConnection"] != null) {
				MvcApplication.connectionString = ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString;
			}
		}
	}
}