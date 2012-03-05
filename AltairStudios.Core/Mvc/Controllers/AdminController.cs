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
			string path = MvcApplication.Path;
			
			html.Append("<!DOCTYPE html>\n");
			html.Append("<html lang='en'>");
			html.Append("<head>");
			html.Append("<meta charset='utf-8' />");
			html.Append("<title>AltairStudios.Core - Admin</title>");
			html.Append("<link rel='stylesheet' type='text/css' href='" + path + "/Bin/resources/css/Ext.css' />");
			html.Append("<link rel='stylesheet' type='text/css' href='" + path + "/Bin/resources/css/Desktop.css' />");
			html.Append("<script src='" + path + "/Bin/resources/javascript/Ext.js'></script>");
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
	        html.Append("'Ext.ux.desktop': '" + path + "/Bin/resources/javascript/Ux',");
	        html.Append("'AdminDesktop': '" + path + "/Bin/resources/javascript/Desktop'");
	        html.Append("});");
	
	        html.Append("Ext.require('AdminDesktop.App');");
	
	        html.Append("var adminDesktop = null;");
	        html.Append("Ext.onReady(function () {");
	        //html.Append("alert('adsf');");
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
			 string css = textStreamReader.ReadToEnd();
			 textStreamReader.Close();
			 
			 Response.ContentType = "text/css";
			 
			 return css;
		}
		
		public string Image(string path) {
			 Assembly assembly = Assembly.GetExecutingAssembly();
			 TextReader textStreamReader = new StreamReader(assembly.GetManifestResourceStream("AltairStudios.Core.Resources.Images." + path));
			 string image = textStreamReader.ReadToEnd();
			 textStreamReader.Close();
			 
			 //Response.ContentType = "text/css";
			 
			 return image;
		}
		
		public string ImageDesktop(string path) {
			 Assembly assembly = Assembly.GetExecutingAssembly();
			 TextReader textStreamReader = new StreamReader(assembly.GetManifestResourceStream("AltairStudios.Core.Resources.Images.Desktop." + path));
			 string image = textStreamReader.ReadToEnd();
			 textStreamReader.Close();
			 
			 //Response.ContentType = "text/css";
			 
			 return image;
		}
		
		
		public string ThemeImage(string theme, string pack, string image) {
			 Assembly assembly = Assembly.GetExecutingAssembly();
			 TextReader textStreamReader = new StreamReader(assembly.GetManifestResourceStream("AltairStudios.Core.Resources.Images.Theme." + theme + "." + pack + "." + image));
			 string imageSrc = textStreamReader.ReadToEnd();
			 textStreamReader.Close();
			 
			 this.setImageContentType(image);
			 
			 //Response.ContentType = "text/css";
			 
			 return imageSrc;
		}
		
		
		public string ThemeImageSublevel(string theme, string pack, string image) {
			 Assembly assembly = Assembly.GetExecutingAssembly();
			 TextReader textStreamReader = new StreamReader(assembly.GetManifestResourceStream("AltairStudios.Core.Resources.Images.Theme." + theme + "." + pack + "." + image));
			 string imageSrc = textStreamReader.ReadToEnd();
			 textStreamReader.Close();
			 
			 this.setImageContentType(image);
			 //Response.ContentType = "text/css";
			 
			 return imageSrc;
		}
		
		
		protected void setImageContentType(string file) {
			string extension = file.Substring(file.Length - 3, 3);
			return;
			if(extension == "gif") {
				Response.ContentType = "image/gif";
			} else if(extension == "jpg") {
				Response.ContentType = "image/jpeg";
			} else if(extension == "png") {
				Response.ContentType = "image/png";
			}
		}
	}
}