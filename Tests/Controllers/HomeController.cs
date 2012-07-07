using System;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Web.Handlers;
using System.Web.UI;
using AltairStudios.Core.Orm.Providers;
using AltairStudios.Core.Orm.Models;

namespace AltairStudios.Core.Tests.Web.Controllers {
	[HandleError]
	public class HomeController : Controller {
		public string Index() {
			/*Tests.Web.Models.TestModel tet = new AltairStudios.Core.Tests.Web.Models.TestModel();
			tet.Name = "prueba";
			tet.Price = 5;
			tet.Mol = new AltairStudios.Core.Tests.Web.Models.JsonTestModel();
			tet.Mol.Ignore = true;
			tet.Mol.Has = false;
			tet.Mol.Name = "prueba interna";
			tet.Mol.Price = 5.5;*/
			
			/*Tests.Web.Models.TestModel tet = new AltairStudios.Core.Tests.Web.Models.TestModel();
			
			//return SqlProvider.getProvider().sqlDelete(tet.GetType());
			
			tet.Id = 5;
			AltairStudios.Core.Orm.ModelList<Tests.Web.Models.TestModel> list = tet.getBy<Tests.Web.Models.TestModel>();
			
			list[0].Price = 3;
			list[0].delete();
			
			return list.ToJson();*/
			
			

			AltairStudios.Core.Orm.Models.User user = new AltairStudios.Core.Orm.Models.User();
			user.Name = "Juan";
			
			//user.Name = "juan";
			
			return user.getBy<AltairStudios.Core.Orm.Models.User>().ToJson();
		}
		
		public T cast<T>(object o) {
			return (T)o;
		}
		
		public static string GetResourceUrl<TAssemblyObject>(string resourcePath) {
 			AssemblyResourceLoader loader = new AssemblyResourceLoader();
 			
            MethodInfo getResourceUrlMethod = typeof(AssemblyResourceLoader)
                .GetMethod("GetWebResourceUrlInternal", BindingFlags.NonPublic | BindingFlags.Static);
            string resourceUrl = string.Format("/{0}", getResourceUrlMethod.Invoke
                (
                    null,
                    new object[] { Assembly.GetAssembly(typeof(TAssemblyObject)), resourcePath, false })
                );
            return resourceUrl;
        }
	}
}