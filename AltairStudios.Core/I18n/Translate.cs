using System;
using System.IO;
using System.Collections.Generic;
using AltairStudios.Core.Orm;
using AltairStudios.Core.Orm.Models.I18n;
using Newtonsoft.Json;


namespace AltairStudios.Core.I18n {
	public class Translate {
		protected static Translate instance;
		protected Dictionary<string, ModelList<TranslateItem>> translates;
		protected string debug;

		public static Translate Instance {
			get {
				if(instance == null) {
					instance = new Translate();
				}
				return instance;
			}
		}
		
		protected Translate() {
			this.translates = new Dictionary<string, ModelList<TranslateItem>>();
		}
		
		
		public string getLanguage() {
			string language = "";
			
			if(System.Web.HttpContext.Current.Session["language"] != null) {
				language = System.Web.HttpContext.Current.Session["language"].ToString();
			}
			
			if(string.IsNullOrEmpty(language)) {
				language = "en-EN";
			}
			
			return language;
		}
		
		
		public static string t(string code) {
			return t(code, instance.getLanguage());
		}
		
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
		
		public static string ts(string code) {
			return ts(code, instance.getLanguage());
		}
		
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
 		
 		public void clear() {
			this.translates.Clear();
 		}
	}
}