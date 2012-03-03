using System;
using AltairStudios.Core.Mvc;


namespace AltairStudios.Core.Tests.Web.Models {
	public class TestModel {
		protected int id;
		protected string name;
		protected double price;
		
		public TestModel() {
			this.id = 1;
			this.name = "Test name";
			this.price = 4.3;
		}
	}
}