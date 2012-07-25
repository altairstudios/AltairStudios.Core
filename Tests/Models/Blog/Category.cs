using AltairStudios.Core.Orm;
using AltairStudios.Core.Orm.Models;


namespace AltairStudios.Core.Tests.Web.Models.Blog {
	public class Category : Archivable {
		protected string description;
		
		[Templatize]
		public string Description {
			get {
				return description;
			}
			set {
				description = value;
			}
		}
	}
}