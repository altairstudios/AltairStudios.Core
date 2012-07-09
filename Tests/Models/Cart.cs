using System;
using System.Collections.Generic;
using System.Data;

using System.Text;
using AltairStudios.Core.Orm;


namespace yShop.Model {
	public class yshop_cart : AltairStudios.Core.Orm.Model {
		protected int id;
		//protected User user;
		//protected List<Product> products;
		protected string commentary;
		protected StringBuilder trace;
		
		
		public string Commentary {
			get {
				return this.commentary;
			}
			set {
				commentary = value;
			}
		}
		
		protected string name;
		protected string address;
		protected string zipcode;
		protected string city;
		protected string region;
		
		[Templatize]
		public string Address {
			get {
				return this.address;
			}
			set {
				address = value;
			}
		}
		
		[Templatize]
		public string City {
			get {
				return this.city;
			}
			set {
				city = value;
			}
		}

		[Templatize]
		public string Name {
			get {
				return this.name;
			}
			set {
				name = value;
			}
		}
		
		[Templatize]
		public string Region {
			get {
				return this.region;
			}
			set {
				region = value;
			}
		}
		
		[Templatize]
		public string Zipcode {
			get {
				return this.zipcode;
			}
			set {
				zipcode = value;
			}
		}		
		


			
		public StringBuilder Trace {
			get {
				return this.trace;
			}
		}



	}
}

