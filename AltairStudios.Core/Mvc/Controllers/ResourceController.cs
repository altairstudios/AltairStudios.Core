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
	/// Resource controller.
	/// </summary>
	public class ResourceController : Controller {
		/// <summary>
		/// The creation time.
		/// </summary>
		protected static DateTime creationTime;
	
	
	
		/// <summary>
		/// Index this instance.
		/// </summary>
		public string Index() {
			throw new HttpException(404, "Not found");
		}
		
		
		
		/// <summary>
		/// Load the specified resource.
		/// </summary>
		/// <param name='resource'>
		/// Resource.
		/// </param>
		/// <exception cref='HttpException'>
		/// Is thrown when the http exception.
		/// </exception>
		public ActionResult load(string resource) {
			if (!String.IsNullOrEmpty(Request.Headers["If-Modified-Since"])) {
				Response.StatusCode = 304;
	            Response.StatusDescription = "Not Modified";
	            return Content(String.Empty);
		    }
		    
		    string extension;
			string contentType;
			
			try {
				extension = Path.GetExtension(resource);
				contentType = this.resolveContextType(extension);
				
				FileContentResult fileContent = new FileContentResult(this.readResource(resource, extension), contentType);
				
				Response.ContentType = contentType;
					
				Response.CacheControl = "public";
				Response.Cache.SetCacheability(HttpCacheability.Public);
				
				Response.Cache.SetLastModified(this.getResourcesCreationTime());
				
				 
				
				return fileContent;
			} catch(FileNotFoundException exception) {
				throw new HttpException(404, "Not found", exception);
			}
		}
		
		
		
		/// <summary>
		/// Resolves the type of the context.
		/// </summary>
		/// <returns>
		/// The context type.
		/// </returns>
		/// <param name='extension'>
		/// Extension.
		/// </param>
		protected string resolveContextType(string extension) {
			string contentType = "plain/text";
			
			switch(extension) {
				case ".js": contentType = "application/javascript"; break;
				case ".png": contentType = "image/png"; break;
				case ".css": contentType = "text/css"; break;
			}
			
			return contentType;
		}
		
		
		
		/// <summary>
		/// Gets the resources creation time.
		/// </summary>
		/// <returns>
		/// The resources creation time.
		/// </returns>
		protected DateTime getResourcesCreationTime() {
			if(ResourceController.creationTime == null) {
				System.IO.FileInfo coreFileInfo = new System.IO.FileInfo(Assembly.GetExecutingAssembly().Location);
				ResourceController.creationTime = coreFileInfo.CreationTime;
			}
			
			return ResourceController.creationTime;
		}
		
		
		
		#region Stream load
		/// <summary>
		/// Reads the stream.
		/// </summary>
		/// <returns>
		/// The stream.
		/// </returns>
		/// <param name='stream'>
		/// Stream.
		/// </param>
		protected byte[] readStream(Stream stream) {
			byte[] buffer = new byte[32768];
		    using (MemoryStream ms = new MemoryStream()) {
		        while (true) {
		            int read = stream.Read (buffer, 0, buffer.Length);
		            if (read <= 0)
		                return ms.ToArray();
		            ms.Write (buffer, 0, read);
		        }
		    }
		}
		
		
		
		/// <summary>
		/// Reads the resource.
		/// </summary>
		/// <returns>
		/// The resource.
		/// </returns>
		/// <param name='resource'>
		/// Resource.
		/// </param>
		protected byte[] readResource(string resource, string extension) {
			Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
			
			if(stream == null) {
				throw new FileNotFoundException("Not found", resource);
			}
			
			byte[] content;
			
			if(extension == ".css") {
				StreamReader reader = new StreamReader(stream);
				string file = reader.ReadToEnd().Replace("../img/","../load/AltairStudios.Core.resources.img.");
				System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
				content = encoding.GetBytes(file);
			} else {
				content = this.readStream(stream);
			}
			
			return content;
		}
		#endregion
	}
}