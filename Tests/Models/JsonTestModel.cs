using System;
using AltairStudios.Core.Mvc;


namespace AltairStudios.Core.Tests.Web.Models {
	public class JsonTestModel : Model {
		protected int id;
		protected string name;
		protected double price;
		protected bool ignore;
		protected bool has;
		
		[Templatize()]
		public int Id {
			get {
				return this.id;
			}
			set {
				id = value;
			}
		}
		
		[Templatize()]
		public string Name {
			get {
				return this.name;
			}
			set {
				name = value;
			}
		}
		
		[Templatize()]
		public double Price {
			get {
				return this.price;
			}
			set {
				price = value;
			}
		}

		public bool Ignore {
			get {
				return this.ignore;
			}
			set {
				ignore = value;
			}
		}
		
		[Templatize()]
		public bool Has {
			get {
				return this.has;
			}
			set {
				has = value;
			}
		}
		
		public JsonTestModel() {
			this.id = 1;
			this.name = "Test name";
			this.price = 4.3;
		}
	}
}