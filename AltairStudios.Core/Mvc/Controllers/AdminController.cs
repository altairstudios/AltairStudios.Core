using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace AltairStudios.Core.Mvc.Controllers {
	public class AdminController : Controller {
		public string Index() {
			StringBuilder html = new StringBuilder();
			
			html.Append("<!DOCTYPE html>\n");
			html.Append("<html lang='en'>");
			html.Append("<head>");
			html.Append("<meta charset='utf-8' />");
			html.Append("<title>AltairStudios.Core - Admin</title>");
			html.Append("<link rel='stylesheet' type='text/css' href='Admin/Resources/Css/Ext.css' />");
			html.Append("<link rel='stylesheet' type='text/css' href='Admin/Resources/Css/Desktop.css' />");
			html.Append("<script src='Admin/Resources/Javascript/Ext.js'></script>");
			//html.Append("<script src='Admin/Resources/Javascript/Desktop/App.js'></script>");
			html.Append("<script type='text/javascript'>");
			/*html.Append("Ext.application({");
			html.Append("name: 'AltairStudios.Core.Admin',");
			//html.Append("autoCreateViewport: true,");
			html.Append("launch: function() {");
			html.Append("alert('juas')");
			html.Append("}");
			html.Append("});");*/
			
			
			html.Append("Ext.Loader.setConfig({enabled:true});");
	        html.Append("Ext.Loader.setPath({");
	        html.Append("'Ext.ux.desktop': 'Admin/Resources/Javascript/Ux',");
	        html.Append("'AdminDesktop': 'Admin/Resources/Javascript/Desktop'");
	        html.Append("});");
	
	        html.Append("Ext.require('AdminDesktop.App');");
	
	        html.Append("var adminDesktop = null;");
	        html.Append("Ext.onReady(function () {");
	        html.Append("alert('adsf');");
			html.Append("adminDesktop = new AdminDesktop.App();");
			html.Append("});");
			
			
			html.Append("</script>");
			html.Append("</head>");
			html.Append("<body>");
			//html.Append("<h1>rules!</h1>");
			html.Append("</body>");
			html.Append("</html>");
			
			return html.ToString();
		}
		
		
		public string JavascriptDesktop(string path) {
			 Assembly assembly = Assembly.GetExecutingAssembly();
			 TextReader textStreamReader = new StreamReader(assembly.GetManifestResourceStream("AltairStudios.Core.Resources.Javascript.Desktop." + path));
			 string javascript = textStreamReader.ReadToEnd();
			 textStreamReader.Close();
			 
			 Response.ContentType = "application/x-javascript";
			 
			 return javascript;
		}
		
		
		public string JavascriptUx(string path) {
			 Assembly assembly = Assembly.GetExecutingAssembly();
			 TextReader textStreamReader = new StreamReader(assembly.GetManifestResourceStream("AltairStudios.Core.Resources.Javascript.Ux." + path));
			 string javascript = textStreamReader.ReadToEnd();
			 textStreamReader.Close();
			 
			 Response.ContentType = "application/x-javascript";
			 
			 return javascript;
		}
		
		
		public string Javascript(string path) {
			 Assembly assembly = Assembly.GetExecutingAssembly();
			 TextReader textStreamReader = new StreamReader(assembly.GetManifestResourceStream("AltairStudios.Core.Resources.Javascript." + path));
			 string javascript = textStreamReader.ReadToEnd();
			 textStreamReader.Close();
			 
			 Response.ContentType = "application/x-javascript";
			 
			 return javascript;
		}
		
		
		public string Css(string path) {
			 Assembly assembly = Assembly.GetExecutingAssembly();
			 TextReader textStreamReader = new StreamReader(assembly.GetManifestResourceStream("AltairStudios.Core.Resources.Css." + path));
			 string javascript = textStreamReader.ReadToEnd();
			 textStreamReader.Close();
			 
			 Response.ContentType = "text/css";
			 
			 return javascript;
		}
	}
}