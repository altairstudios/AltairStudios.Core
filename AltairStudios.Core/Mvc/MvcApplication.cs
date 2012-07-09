using System;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;


namespace AltairStudios.Core.Mvc {
	/// <summary>
	/// Mvc application.
	/// </summary>
	public class MvcApplication : System.Web.HttpApplication {
		/// <summary>
		/// The path.
		/// </summary>
		protected static string path = "";
		/// <summary>
		/// The disk path.
		/// </summary>
		protected static string diskPath = "";
		/// <summary>
		/// The connection string.
		/// </summary>
		protected static string connectionString = "";
		/// <summary>
		/// The configurated.
		/// </summary>
		protected static bool configurated = false;

		
		
		/// <summary>
		/// Gets the connection string.
		/// </summary>
		/// <value>
		/// The connection string.
		/// </value>
		public static string ConnectionString {
			get {
				return MvcApplication.connectionString;
			}
		}
		
		
		
		/// <summary>
		/// Gets the disk path.
		/// </summary>
		/// <value>
		/// The disk path.
		/// </value>
		public static string DiskPath {
			get {
				return MvcApplication.diskPath;
			}
		}
		
		
		
		/// <summary>
		/// Gets the path.
		/// </summary>
		/// <value>
		/// The path.
		/// </value>
		public static string Path {
			get {
				if(MvcApplication.path == "") {
					if(HttpContext.Current.Request.ApplicationPath != "/") {
						MvcApplication.path = HttpContext.Current.Request.ApplicationPath;
					}
				}
				return MvcApplication.path;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="AltairStudios.Core.Mvc.MvcApplication"/> is configurated.
		/// </summary>
		/// <value>
		/// <c>true</c> if configurated; otherwise, <c>false</c>.
		/// </value>
		public static bool Configurated {
			get {
				return MvcApplication.configurated;
			}
			set {
				configurated = value;
			}
		}		
		
		
		
		/// <summary>
		/// Registers the routes.
		/// </summary>
		/// <param name='routes'>
		/// Routes.
		/// </param>
		public virtual void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute ("{resource}.axd/{*pathInfo}");

			routes.MapRoute("Install", "Admin/Install/{action}", new { controller = "Install", action = "Index", id = "" });
			routes.MapRoute("Plugin", "Admin/PluginConfig/{action}/{id}", new { controller = "PluginConfig", action = "Index", id = "plugin" });
			routes.MapRoute("Admin", "Admin/{action}", new { controller = "Admin", action = "Index", id = "" });
			routes.MapRoute("Resource", "Resource/{action}/{resource}", new { controller = "Resource", action = "Index", resource = "" });
			routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = "" });
		}

		
		
		/// <summary>
		/// Application_s the start.
		/// </summary>
		protected void Application_Start() {
			this.RegisterRoutes(RouteTable.Routes);
			
			Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
			AuthenticationSection authSection = (AuthenticationSection)config.GetSection("system.web/authentication");
			authSection.Mode = AuthenticationMode.Forms;
			authSection.Forms.LoginUrl = "~/Authorize/Login";
			
			System.Web.Hosting.HostingEnvironment.RegisterVirtualPathProvider(new VirtualProvider.AssemblyPathProvider());
			
			/*MvcApplication.diskPath = HttpContext.Current.Server.MapPath("~");
			if(HttpContext.Current.Request.ApplicationPath != "/") {
				MvcApplication.path = HttpContext.Current.Request.ApplicationPath;
			}*/
			
			if(ConfigurationManager.ConnectionStrings["SqlServerConnection"] != null) {
				MvcApplication.configurated = true;
				MvcApplication.connectionString = ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString;
			}
			
			AltairStudios.Core.I18n.Translate.Instance.clear();
			AltairStudios.Core.I18n.Translate.Instance.loadFile("AltairStudios.Core.resources.locales.es.json", "es");
			AltairStudios.Core.I18n.Translate.Instance.loadFile("AltairStudios.Core.resources.locales.en.json", "en");
		}
		
		
		
		/// <summary>
		/// Loads the configuration.
		/// </summary>
		public static void loadConfiguration() {
			if(ConfigurationManager.ConnectionStrings["SqlServerConnection"] != null) {
				MvcApplication.configurated = true;
				MvcApplication.connectionString = ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString;
			}
		}
	}
}