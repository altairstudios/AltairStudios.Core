using AltairStudios.Core.Orm;
using AltairStudios.Core.Orm.Models;


namespace AltairStudios.Core.Tests.Web.Models.Blog {
	public class Archivable : Model {
		protected int? id;
		protected string name;
		protected string url;
		
		[PrimaryKey(true)]
		public int? Id {
			get {
				return id;
			}
			set {
				id = value;
			}
		}
		
		[Index]
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}
		
		[Index]
		public string Url {
			get {
				return url;
			}
			set {
				url = value;
			}
		}
	}
}