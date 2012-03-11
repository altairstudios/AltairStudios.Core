using System;
using System.Text;


namespace AltairStudios.Core.Orm.Models {
	public class List<T> : System.Collections.Generic.List<T> {
		public string ToJson() {
			List<string> jsonProperties = new List<string>();
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