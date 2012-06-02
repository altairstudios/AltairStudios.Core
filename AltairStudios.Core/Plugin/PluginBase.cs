using System;
using AltairStudios.Core.Orm.Models.Admin;


namespace AltairStudios.Core.Plugin {
	/// <summary>
	/// Plugin base.
	/// </summary>
	public class PluginBase {
		/// <summary>
		/// The name.
		/// </summary>
		public string name;
		/// <summary>
		/// The version.
		/// </summary>
		public string version;
		/// <summary>
		/// The description.
		/// </summary>
		public string description;
		/// <summary>
		/// The menu.
		/// </summary>
		public Menu menu;
	
	
		
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name {
			get { 
				return this.name;
			} set {
				this.name = value;
			}
		}



		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>
		/// The version.
		/// </value>
		public string Version {
			get {
				return version;
			}
			set {
				version = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description {
			get {
				return description;
			}
			set {
				description = value;
			}
		}	



		/// <summary>
		/// Gets or sets the menu.
		/// </summary>
		/// <value>
		/// The menu.
		/// </value>
		public Menu Menu {
			get {
				return menu;
			}
			set {
				menu = value;
			}
		}



		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Plugin.PluginBase"/> class.
		/// </summary>
		public PluginBase() {
		}
	}
}