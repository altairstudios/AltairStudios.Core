using System;
using AltairStudios.Core.Orm;


namespace AltairStudios.Core.Orm.Models.I18n {
	/// <summary>
	/// Translate item.
	/// </summary>
	public class TranslateItem : Model {
		#region Attributes
		/// <summary>
		/// The code.
		/// </summary>
		protected string code;
		
		
		
		/// <summary>
		/// The domain.
		/// </summary>
		protected string domain;
		
		
		
		/// <summary>
		/// The text.
		/// </summary>
		protected string text;
		
		
		
		/// <summary>
		/// The plural text.
		/// </summary>
		protected string pluralText;
		#endregion
		
		
		
		#region Properties
		/// <summary>
		/// Gets or sets the code.
		/// </summary>
		/// <value>
		/// The code.
		/// </value>
		[Templatize]
		public string Code {
			get {
				return code;
			}
			set {
				code = value;
			}
		}	
		
		
		
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		[Templatize]
		public string Text {
			get {
				return text;
			}
			set {
				text = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets the plural text.
		/// </summary>
		/// <value>
		/// The plural text.
		/// </value>
		[Templatize]
		public string PluralText {
			get {
				return pluralText;
			}
			set {
				pluralText = value;
			}
		}
		
		
		
		/// <summary>
		/// Gets or sets the domain.
		/// </summary>
		/// <value>
		/// The domain.
		/// </value>
		public string Domain {
			get {
				return domain;
			}
			set {
				domain = value;
			}
		}
		#endregion
		
		
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Orm.Models.I18n.TranslateItem"/> class.
		/// </summary>
		public TranslateItem() {
		}
		#endregion
	}
}