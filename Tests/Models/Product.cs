using System;
using System.Collections.Generic;
using AltairStudios.Core.Orm;


namespace yShop.Model {
	public class yshop_products : AltairStudios.Core.Orm.Model {
		protected int id;
		protected string name;
		protected string shortDescription;
		protected string longDescription;
		protected decimal price;
		protected string homePhoto;
		protected int amount;
		
		
		public yshop_products() {
		}
		
		
		
		[PrimaryKey(true)]
		public int Id {
			get {
				return this.id;
			}
			set {
				id = value;
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
		public string ShortDescription {
			get {
				return this.shortDescription;
			}
			set {
				shortDescription = value;
			}
		}
		
		[Templatize]
		public string LongDescription {
			get {
				return this.longDescription;
			}
			set {
				longDescription = value;
			}
		}
		
		[Templatize]
		public decimal Price {
			get {
				return this.price;
			}
			set {
				price = value;
			}
		}
		
		[Templatize]
		public string HomePhoto {
			get {
				return this.homePhoto;
			}
			set {
				homePhoto = value;
			}
		}
		
		public int Amount {
			get {
				return this.amount;
			}
			set {
				amount = value;
			}
		}		
		
		
	}
}