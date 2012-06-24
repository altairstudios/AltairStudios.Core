using System;


namespace AltairStudios.Core.Orm {
	/// <summary>
	/// Templatize attribute.
	/// </summary>
	public class TemplatizeAttribute : AltairStudios.Core.Mvc.TemplatizeAttribute {
		/*
		/// <summary>
		/// The is subtable.
		/// </summary>
		protected bool isSubtable = false;
		/// <summary>
		/// The is list.
		/// </summary>
		protected bool isList = false;
		*/
		
		/*
		/// <summary>
		/// Gets or sets a value indicating whether this instance is list.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is list; otherwise, <c>false</c>.
		/// </value>
		public bool IsList {
			get {
				return this.isList;
			}
			set {
				isList = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets a value indicating whether this instance is subtable.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is subtable; otherwise, <c>false</c>.
		/// </value>
		public bool IsSubtable {
			get {
				return this.isSubtable;
			}
			set {
				isSubtable = value;
			}
		}
		*/
		
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Orm.TemplatizeAttribute"/> class.
		/// </summary>
		public TemplatizeAttribute() {
			this.templatize = true;
		}
		
		
		/*
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Orm.TemplatizeAttribute"/> class.
		/// </summary>
		/// <param name='isSubtable'>
		/// Is subtable.
		/// </param>
		public TemplatizeAttribute(bool isSubtable) {
			this.templatize = true;
			this.isSubtable = isSubtable;
		}
		
		
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Orm.TemplatizeAttribute"/> class.
		/// </summary>
		/// <param name='isSubtable'>
		/// Is subtable.
		/// </param>
		/// <param name='isList'>
		/// Is list.
		/// </param>
		public TemplatizeAttribute(bool isSubtable, bool isList) {
			this.templatize = true;
			this.isSubtable = isSubtable;
			this.isList = isList;
		}
		*/
	}
}