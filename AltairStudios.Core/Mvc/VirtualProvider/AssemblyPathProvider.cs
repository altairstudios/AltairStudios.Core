using System;
using System.Web;
using System.Web.Hosting;



namespace AltairStudios.Core.Mvc.VirtualProvider {
	/// <summary>
	/// Assembly path provider.
	/// </summary>
	public class AssemblyPathProvider : VirtualPathProvider {
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Mvc.VirtualProvider.AssemblyPathProvider"/> class.
		/// </summary>
		public AssemblyPathProvider() : base() {}
		
		
		
		/// <summary>
		/// Determines whether this instance is path virtual the specified virtualPath.
		/// </summary>
		/// <returns>
		/// <c>true</c> if this instance is path virtual the specified virtualPath; otherwise, <c>false</c>.
		/// </returns>
		/// <param name='virtualPath'>
		/// If set to <c>true</c> virtual path.
		/// </param>
		private bool IsPathVirtual(string virtualPath) {
			String checkPath = VirtualPathUtility.ToAppRelative(virtualPath);
			return (checkPath.StartsWith("~/resources/") || checkPath.StartsWith("/resources/"));
		}
		
		
		
		/// <summary>
		/// Files the exists.
		/// </summary>
		/// <returns>
		/// The exists.
		/// </returns>
		/// <param name='virtualPath'>
		/// If set to <c>true</c> virtual path.
		/// </param>
		public override bool FileExists(string virtualPath) {
			if (IsPathVirtual(virtualPath)) {
				AssemblyVirtualFile file = (AssemblyVirtualFile)this.GetFile(virtualPath);
				return file.Exists;
			} else { 
				return Previous.FileExists(virtualPath);
			}
		}



		/// <summary>
		/// Opens the file.
		/// </summary>
		/// <returns>
		/// The file.
		/// </returns>
		/// <param name='virtualPath'>
		/// Virtual path.
		/// </param>
		public System.IO.Stream OpenFile(string virtualPath) {
			VirtualFile vf = this.GetFile(virtualPath);
			return vf.Open();
		}

		
		
		/// <summary>
		/// Gets the file.
		/// </summary>
		/// <returns>
		/// The file.
		/// </returns>
		/// <param name='virtualPath'>
		/// Virtual path.
		/// </param>
		public override VirtualFile GetFile(string virtualPath) {
			if (IsPathVirtual(virtualPath)) {
				return new AssemblyVirtualFile(virtualPath, this);
			} else {
				return Previous.GetFile(virtualPath);
			}
		}
	}
}