using System;
using System.Reflection;
using System.Text;
using AltairStudios.Core.Orm;
using AltairStudios.Core.Orm.Models;
using AltairStudios.Core.Util;


namespace AltairStudios.Core.Mvc {
	public class Model {
		protected ModelList<string> getFields(PropertyInfo[] properties) {
			ModelList<string> fields = new ModelList<string>();
			
			for(int i = 0; i < properties.Length; i++) {	
				TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
				if(attributes.Length > 0 && attributes[0].Templatize) {
					fields.Add(properties[i].Name);
				}
			}
			
			return fields;
		}
		
				
		public T cast<T>(object o) {
			return (T)o;
		}
		
		
		public ModelList<T> castList<T>(object o) {
			System.Collections.IList io = this.cast<System.Collections.IList>(o);
			ModelList<T> modelList = new ModelList<T>();
			
			foreach(object item in io) {
				modelList.Add(this.cast<T>(item));
			}
			return modelList;
		}
		
		
		public string ToJson() {
			PropertyInfo[] properties = this.GetType().GetProperties();
			StringBuilder json = new StringBuilder();
			ModelList<string> jsonProperties = new ModelList<string>();
			StringConverter converter = new StringConverter();
			
			json.Append("{");
			
			for(int i = 0; i < properties.Length; i++) {
				if(properties[i].GetValue(this, null) != null) {
					TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
					if(attributes.Length > 0 && attributes[0].Templatize) {
						jsonProperties.Add("\"" + properties[i].Name + "\"" + ":" + converter.convert(properties[i].GetValue(this, null), properties[i].PropertyType));
					}
				}
			}
			
			json.Append(string.Join(",", jsonProperties.ToArray()));
			json.Append("}");
			
			return json.ToString();
		}
	}
}