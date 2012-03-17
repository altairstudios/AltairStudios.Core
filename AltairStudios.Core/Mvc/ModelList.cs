using System;
using System.Text;


namespace AltairStudios.Core.Mvc {
	/// <summary>
	/// Model list.
	/// </summary>
	public class ModelList<T> : System.Collections.Generic.List<T> {
		/// <summary>
		/// Tos the json.
		/// </summary>
		/// <returns>
		/// The json.
		/// </returns>
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
		
		
		
		/// <summary>
		/// Cast the specified o.
		/// </summary>
		/// <param name='o'>
		/// O.
		/// </param>
		/// <typeparam name='T'>
		/// The 1st type parameter.
		/// </typeparam>
		public T cast<T>(object o) {
			return (T)o;
		}
	}
}