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
			
			html.Append("<!DOCTYPE html>");
			html.Append("<html lang='en'>");
			html.Append("<head>");
			html.Append("<meta charset='utf-8' />");
			html.Append("<title>AltairStudios.Core - Admin</title>");
			html.Append("<script src='Admin/Javascript?path=Ext.js'></script>");
			html.Append("</head>");
			html.Append("<body>");
			html.Append("<h1>rules!</h1>");
			html.Append("</body>");
			html.Append("</html>");
			
			return html.ToString();
		}
		
		
		public string Javascript(string path) {
			 Assembly assembly = Assembly.GetExecutingAssembly();
			 TextReader textStreamReader = new StreamReader(assembly.GetManifestResourceStream("AltairStudios.Core.Resources.Javascript." + path));
			 string javascript = textStreamReader.ReadToEnd();
			 textStreamReader.Close();
			 
			 Response.ContentType = "application/x-javascript";
			 
			 return javascript;
		}
	}
}