using System;
using AltairStudios.Core.Orm;
using AltairStudios.Core.Orm.Models;


namespace AltairStudios.Core.Orm.Models.Admin {
	/// <summary>
	/// Menu.
	/// </summary>
	public class Menu : Model {
		/// <summary>
		/// The link.
		/// </summary>
		Link link;
		/// <summary>
		/// The submenus.
		/// </summary>
		ModelList<Menu> submenus;
		
		
		
		/// <summary>
		/// Gets or sets the link.
		/// </summary>
		/// <value>
		/// The link.
		/// </value>
		[Templatize]
		public Link Link {
			get {
				return this.link;
			}
			set {
				link = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets the submenus.
		/// </summary>
		/// <value>
		/// The submenus.
		/// </value>
		[Templatize]
		public ModelList<Menu> Submenus {
			get {
				return this.submenus;
			}
			set {
				submenus = value;
			}
		}
		
		
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Orm.Models.Admin.Menu"/> class.
		/// </summary>
		public Menu() {
			this.submenus = new ModelList<Menu>();
		}
		
		
		
		/// <summary>
		/// Adds the menu.
		/// </summary>
		/// <param name='menu'>
		/// Menu.
		/// </param>
		public void addMenu(Menu menu) {
			this.submenus.Add(menu);
		}
	}
}