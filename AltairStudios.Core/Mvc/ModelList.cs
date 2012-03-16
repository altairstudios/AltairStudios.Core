using System;
using System.Text;


namespace AltairStudios.Core.Mvc {
	public class ModelList<T> : System.Collections.Generic.List<T> {
		public string ToJson() {
			ModelList<string> jsonProperties = new ModelList<string>();
			StringBuilder json = new StringBuilder();
			
			for(int i = 0; i < this.Count; i++) {
				if(this[i] is Model) {
					jsonProperties.Add(this.cast<Model>(this[i]).ToJson());
				}
			}
		
			json.Append("[");
			json.Append(string.Join(",", jsonProperties.ToArray()));
			json.Append("]");
			
			return json.ToString();
		}
		
		
		public T cast<T>(object o) {
			return (T)o;
		}
	}
}