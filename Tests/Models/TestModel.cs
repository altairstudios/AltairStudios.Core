using System;
using AltairStudios.Core.Orm;


namespace AltairStudios.Core.Tests.Web.Models {
	public class TestModel : Model {
		protected int id;
		protected string name;
		protected double price;
		protected JsonTestModel mol;
		protected JsonTestModel mol2;
		protected JsonTestModel mol3;
		protected JsonTestModel mol4;

		[PrimaryKey(true)]
		public int Id {
			get {
				return id;
			}
			set {
				id = value;
			}
		}
		
		[Templatize()]
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}

		[Templatize()]
		public double Price {
			get {
				return price;
			}
			set {
				price = value;
			}
		}

		[Templatize()]
		public JsonTestModel Mol {
			get {
				return mol;
			}
			set {
				mol = value;
			}
		}
		/*[ForeignKey()]
		public JsonTestModel Mol2 {
			get {
				return mol;
			}
			set {
				mol = value;
			}
		}
		[ForeignKey()]
		public JsonTestModel Mol3 {
			get {
				return mol;
			}
			set {
				mol = value;
			}
		}
		[ForeignKey()]
		public JsonTestModel Mol4 {
			get {
				return mol;
			}
			set {
				mol = value;
			}
		}
		
		public TestModel() {
			this.id = 1;
			this.name = "Test name";
			this.price = 4.3;
		}*/
	}
}