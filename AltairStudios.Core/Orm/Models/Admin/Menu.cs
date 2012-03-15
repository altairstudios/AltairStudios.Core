using System;
using AltairStudios.Core.Orm;
using AltairStudios.Core.Orm.Models;


namespace AltairStudios.Core.Orm.Models.Admin {
	public class Menu : Model {
		Link link;
		ModelList<Menu> submenus;
		
		[Templatize(true)]
		public Link Link {
			get {
				return this.link;
			}
			set {
				link = value;
			}
		}
		
		[Templatize(true, true)]
		public ModelList<Menu> Submenus {
			get {
				return this.submenus;
			}
			set {
				submenus = value;
			}
		}
		
		public Menu() {
			this.submenus = new ModelList<Menu>();
		}
		
		public void addMenu(Menu menu) {
			this.submenus.Add(menu);
		}
	}
}