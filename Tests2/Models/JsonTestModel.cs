using System;


namespace AltairStudios.Core.Tests.Web.Models {
	public class JsonTestModel : AltairStudios.Core.Mvc.Model {
		protected int id;
		protected string name;
		protected double price;
		
		public JsonTestModel() {
			this.id = 1;
			this.name = "Test name";
			this.price = 4.3;
		}
	}
}