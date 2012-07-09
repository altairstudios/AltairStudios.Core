using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AltairStudios.Core.Orm;
using AltairStudios.Core.Orm.Models;
using AltairStudios.Core.Util;


namespace AltairStudios.Core.Mvc {
	/// <summary>
	/// Model.
	/// </summary>
	public class Model : IModelizable {
		/// <summary>
		/// Gets the fields.
		/// </summary>
		/// <returns>
		/// The fields.
		/// </returns>
		/// <param name='properties'>
		/// Properties.
		/// </param>
		protected ModelList<string> getFields(PropertyInfo[] properties) {
			return this.getFields(properties, true);
		}
		
		
		
		/// <summary>
		/// Gets the fields.
		/// </summary>
		/// <returns>
		/// The fields.
		/// </returns>
		/// <param name='properties'>
		/// Properties.
		/// </param>
		/// <param name='ownFields'>
		/// Own fields.
		/// </param>
		protected ModelList<string> getFields(PropertyInfo[] properties, bool ownFields) {
			ModelList<string> fields = new ModelList<string>();
			
			for(int i = 0; i < properties.Length; i++) {	
				TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
				if(attributes.Length > 0 && !Reflection.Instance.isChildOf(properties[i].PropertyType, typeof(Model))) {
					fields.Add(properties[i].Name);
				}
			}
			
			return fields;
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
		
		
		
		/// <summary>
		/// Casts the list.
		/// </summary>
		/// <returns>
		/// The list.
		/// </returns>
		/// <param name='o'>
		/// O.
		/// </param>
		/// <typeparam name='T'>
		/// The 1st type parameter.
		/// </typeparam>
		public ModelList<T> castList<T>(object o) {
			System.Collections.IList io = this.cast<System.Collections.IList>(o);
			ModelList<T> modelList = new ModelList<T>();
			
			foreach(object item in io) {
				modelList.Add(this.cast<T>(item));
			}
			return modelList;
		}
		
		
		
		/// <summary>
		/// Tos the json.
		/// </summary>
		/// <returns>
		/// The json.
		/// </returns>
		public virtual string ToJson() {
			PropertyInfo[] properties = this.GetType().GetProperties();
			StringBuilder json = new StringBuilder();
			ModelList<string> jsonProperties = new ModelList<string>();
			StringConverter converter = new StringConverter();
			
			json.Append("{");
			
			for(int i = 0; i < properties.Length; i++) {
				if(properties[i].GetValue(this, null) != null) {
					TemplatizeAttribute[] attributes = (TemplatizeAttribute[])properties[i].GetCustomAttributes(typeof(TemplatizeAttribute), true);
					if(attributes.Length > 0) {
						if(properties[i].PropertyType.GetInterface("IModelizable") != null) {
							jsonProperties.Add("\"" + properties[i].Name + "\"" + ":" + ((IModelizable)this.getPropertyValue(properties[i])).ToJson());
						} else {
							jsonProperties.Add("\"" + properties[i].Name + "\"" + ":" + converter.convert(this.getPropertyValue(properties[i]), properties[i].PropertyType));
						}
						
						
						/*//properties[i].PropertyType.GetInterface("")
						if(Reflection.Instance.isChildOf(properties[i].PropertyType, typeof(Model))) {
							jsonProperties.Add("\"" + properties[i].Name + "\"" + ":" + ((Model)this.getPropertyValue(properties[i])).ToJson());
						} else if(Reflection.Instance.isChildOf(properties[i].PropertyType, typeof(ModelList<>))) {
							jsonProperties.Add("\"" + properties[i].Name + "\"" + ":" + ((ModelList<object>)this.getPropertyValue(properties[i])).ToJson());
						} else {
							jsonProperties.Add("\"" + properties[i].Name + "\"" + ":" + converter.convert(this.getPropertyValue(properties[i]), properties[i].PropertyType));
						}*/
					}
				}
			}
			
			json.Append(string.Join(",", jsonProperties.ToArray()));
			json.Append("}");
			
			return json.ToString();
		}
		
		
		
		/// <summary>
		/// Gets the property value.
		/// </summary>
		/// <returns>
		/// The property value.
		/// </returns>
		/// <param name='property'>
		/// Property.
		/// </param>
		protected object getPropertyValue(PropertyInfo property) {
			if(Reflection.Instance.isChildOf(property.PropertyType, typeof(Nullable))) {
				object obj = property.GetValue(this, null);
				return obj;
			} else {
				return property.GetValue(this, null);
			}
		}
	}
}