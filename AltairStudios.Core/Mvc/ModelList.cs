using System;
using System.Text;
using AltairStudios.Core.Util;


namespace AltairStudios.Core.Mvc {
	/// <summary>
	/// Model list.
	/// </summary>
	public class ModelList<T> : System.Collections.Generic.List<T>, IModelizable {
		/// <summary>
		/// Tos the json.
		/// </summary>
		/// <returns>
		/// The json.
		/// </returns>
		public string ToJson() {
			ModelList<string> jsonProperties = new ModelList<string>();
			StringBuilder json = new StringBuilder();
			StringConverter converter = new StringConverter();
			
			for(int i = 0; i < this.Count; i++) {
				if(this[i].GetType().GetInterface("IModelizable") != null) {
					jsonProperties.Add(((IModelizable)this[i]).ToJson());
				} else {
					jsonProperties.Add(converter.convert(this[i]));
				}
				
				
				//if(this[i] is Model || this[i] is ModelList<T>) {
					
					
					/*if(Reflection.Instance.isChildOf(this[i].GetType(), typeof(Model))) {
						jsonProperties.Add(this.cast<Model>(this[i]).ToJson());
					} else if(Reflection.Instance.isChildOf(this[i].GetType(), typeof(ModelList<>))) {
						jsonProperties.Add(this.cast<ModelList<T>>(this[i]).ToJson());
						//jsonProperties.Add(((ModelList<object>)this.getPropertyValue(properties[i])).ToJson());
					} else {
						jsonProperties.Add(converter.convert(this[i]));
					}
					//jsonProperties.Add(this.cast<Model>(this[i]).ToJson());*/
				//}
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