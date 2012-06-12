using System;
using System.IO;
using System.Collections.Generic;
using AltairStudios.Core.Orm;
using AltairStudios.Core.Orm.Models.I18n;
using Newtonsoft.Json;


namespace AltairStudios.Core.I18n {
	/// <summary>
	/// Translate.
	/// </summary>
	public class Translate {
		#region Attributes
		/// <summary>
		/// The instance.
		/// </summary>
		protected static Translate instance;
		
		
		
		/// <summary>
		/// The translates.
		/// </summary>
		protected Dictionary<string, ModelList<TranslateItem>> translates;
		
		
		
		/// <summary>
		/// The debug.
		/// </summary>
		protected string debug;
		#endregion
		
		
		
		#region Properties
		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>
		/// The instance.
		/// </value>
		public static Translate Instance {
			get {
				if(instance == null) {
					instance = new Translate();
				}
				return instance;
			}
		}
		#endregion
		
		
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.I18n.Translate"/> class.
		/// </summary>
		protected Translate() {
			this.translates = new Dictionary<string, ModelList<TranslateItem>>();
		}
		#endregion
		
		
		
		#region Translation methods
		/// <summary>
		/// Gets the language.
		/// </summary>
		/// <returns>
		/// The language.
		/// </returns>
		public string getLanguage() {
			string language = "";
			
			if(System.Web.HttpContext.Current.Session["language"] != null) {
				language = System.Web.HttpContext.Current.Session["language"].ToString();
			}
			
			if(string.IsNullOrEmpty(language)) {
				language = this.simplyfyLanguage(System.Web.HttpContext.Current.Request.UserLanguages[0]);
				if(!this.translates.ContainsKey(language)) {
					language = "en";
				}
			}
			
			return language;
		}
		
		
		
		/// <summary>
		/// Translate the specified code.
		/// </summary>
		/// <param name='code'>
		/// Code.
		/// </param>
		public static string t(string code) {
			return t(code, instance.getLanguage());
		}
		
		
		
		/// <summary>
		/// Translate the specified code and language.
		/// </summary>
		/// <param name='code'>
		/// Code.
		/// </param>
		/// <param name='language'>
		/// Language.
		/// </param>
		public static string t(string code, string language) {			
			if(instance.translates.ContainsKey(language)) {
				for(int i = 0; i < instance.translates[language].Count; i++) {
					if(instance.translates[language][i].Code == code) {
						return instance.translates[language][i].Text;
					}
				}
				return code;
			} else {
				return code;
			}
		}
		
		
		
		/// <summary>
		/// Tanslate to plural the specified code.
		/// </summary>
		/// <param name='code'>
		/// Code.
		/// </param>
		public static string ts(string code) {
			return ts(code, instance.getLanguage());
		}
		
		
		
		/// <summary>
		/// Tanslate to plural the specified code and language.
		/// </summary>
		/// <param name='code'>
		/// Code.
		/// </param>
		/// <param name='language'>
		/// Language.
		/// </param>
		public static string ts(string code, string language) {
			if(instance.translates.ContainsKey(language)) {
				for(int i = 0; i < instance.translates[language].Count; i++) {
					if(instance.translates[language][i].Code == code) {
						return instance.translates[language][i].PluralText;
					}
				}
				return code;
			} else {
				return code;
			}
		}
		
		
		
		/// <summary>
		/// Loads the file.
		/// </summary>
		/// <param name='file'>
		/// File.
		/// </param>
		/// <param name='language'>
		/// Language.
		/// </param>
		public void loadFile(string file, string language) {
			StreamReader reader = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(file));
			
			JsonSerializer serializer = new JsonSerializer();
			JsonTextReader jsonReader = new JsonTextReader(reader);
			
			ModelList<TranslateItem> tar = serializer.Deserialize<ModelList<TranslateItem>>(jsonReader);
			
			if(this.translates.ContainsKey(language)) {
				this.translates[language] = tar;
			} else {
				this.translates.Add(language, tar);
			}
 		}
 		#endregion
		
		
		
		#region Utilities methods
		/// <summary>
		/// Clear this instance.
		/// </summary>
 		public void clear() {
			this.translates.Clear();
 		}
 		
		
		
		/// <summary>
		/// Simplyfies the language.
		/// </summary>
		/// <returns>
		/// The language.
		/// </returns>
		/// <param name='language'>
		/// Language.
		/// </param>
 		public string simplyfyLanguage(string language) {
			language = language.Substring(0, 2);
			
			return language;
 		}
		#endregion
	}
}