using System;
using System.IO;
using System.Web.Hosting;



namespace AltairStudios.Core.Mvc.VirtualProvider {
	/// <summary>
	/// Assembly virtual file.
	/// </summary>
	public class AssemblyVirtualFile : VirtualFile {
		/// <summary>
		/// The file exists.
		/// </summary>
		private bool fileExists = false;
		/// <summary>
		/// The name of the virtual.
		/// </summary>
		private string virtualName = "";
		
		
		/// <summary>
		/// Gets a value indicating whether this <see cref="AltairStudios.Core.Mvc.VirtualProvider.AssemblyVirtualFile"/> is exists.
		/// </summary>
		/// <value>
		/// <c>true</c> if exists; otherwise, <c>false</c>.
		/// </value>
		public bool Exists {
			get { return this.fileExists; }
		}
		
		
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Mvc.VirtualProvider.AssemblyVirtualFile"/> class.
		/// </summary>
		/// <param name='virtualPath'>
		/// Virtual path.
		/// </param>
		/// <param name='provider'>
		/// Provider.
		/// </param>
		public AssemblyVirtualFile(string virtualPath, AssemblyPathProvider provider) : base(virtualPath) {
			//System.IO.File.WriteAllText("/tmp/traza", virtualPath);
			string[] vPath = virtualPath.Split("/".ToCharArray());
			//this.virtualName = virtualPath.Replace("/resources/", "");
			this.virtualName = vPath[vPath.Length - 1];
			if(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceInfo(this.virtualName) != null) {
				this.fileExists = true;
			}
		}


		
		/// <summary>
		/// Open this instance.
		/// </summary>
		public override Stream Open() {
			return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(this.virtualName);
		}
	}
}