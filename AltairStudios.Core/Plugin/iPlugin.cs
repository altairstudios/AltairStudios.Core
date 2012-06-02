using System;
using AltairStudios.Core.Orm.Models.Admin;


namespace AltairStudios.Core.Plugin {
	/// <summary>
	/// Interface for plugins.
	/// </summary>
	public interface iPlugin {
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		string Name {
			get;
			set;
		}
		
		
		
		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>
		/// The version.
		/// </value>
		string Version {
			get;
			set;
		}
		
		
		
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		string Description {
			get;
			set;
		}
		
		
		
		/// <summary>
		/// Gets or sets the menu.
		/// </summary>
		/// <value>
		/// The menu.
		/// </value>
		Menu Menu {
			get;
			set;
		}
	}
}