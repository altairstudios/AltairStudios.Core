using System;
using System.Globalization;
using AltairStudios.Core.Mvc;


namespace AltairStudios.Core.Util {
	/// <summary>
	/// String converter.
	/// </summary>
	public class StringConverter {
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Util.StringConverter"/> class.
		/// </summary>
		public StringConverter() {
		}
		
		
		
		/// <summary>
		/// Convert the specified val and type.
		/// </summary>
		/// <param name='val'>
		/// Value.
		/// </param>
		/// <param name='type'>
		/// Type.
		/// </param>
		public string convert(object val, Type type) {
			string converted = "";
			
			switch(type.Name) {
				case "Int32": converted = this.convert((int)val); break;
				case "String": converted = "\"" + this.convert((string)val) + "\""; break;
				case "Double": converted = this.convert((double)val); break;
				case "Decimal": converted = this.convert((decimal)val); break;
				case "Boolean": converted = this.convert((bool)val); break;
				case "Model": converted = this.convert((Model)val); break;
				case "Nullable`1": converted = this.convert(Convert.ChangeType(val, Nullable.GetUnderlyingType(type)), Nullable.GetUnderlyingType(type)); break;
				default: converted = "\"\""; break;
			}
			
			return converted;
		}
		
		
		
		/// <summary>
		/// Convert the specified val.
		/// </summary>
		/// <param name='val'>
		/// Value.
		/// </param>
		public string convert(Model val) {
			return val.ToJson();
		}
		
		
		
		/// <summary>
		/// Convert the specified val.
		/// </summary>
		/// <param name='val'>
		/// Value.
		/// </param>
		/// <typeparam name='T'>
		/// The 1st type parameter.
		/// </typeparam>
		public string convert<T>(ModelList<T> val) {
			return val.ToJson();
		}
		
		
		
		/// <summary>
		/// Convert the specified val.
		/// </summary>
		/// <param name='val'>
		/// Value.
		/// </param>
		public string convert(object val) {
			return val.ToString();
		}
		
		
		
		/// <summary>
		/// Convert the specified val.
		/// </summary>
		/// <param name='val'>
		/// Value.
		/// </param>
		public string convert(bool val) {
			if(val) {
				return "true";
			} else {
				return "false";
			}
		}
		
		
		
		/// <summary>
		/// Convert the specified val.
		/// </summary>
		/// <param name='val'>
		/// Value.
		/// </param>
		public string convert(int val) {
			return val.ToString(CultureInfo.InvariantCulture.NumberFormat);
		}
		
		
		
		/// <summary>
		/// Convert the specified val.
		/// </summary>
		/// <param name='val'>
		/// Value.
		/// </param>
		public string convert(double val) {
			return val.ToString(CultureInfo.InvariantCulture.NumberFormat);
		}
		
		
		
		/// <summary>
		/// Convert the specified val.
		/// </summary>
		/// <param name='val'>
		/// Value.
		/// </param>
		public string convert(decimal val) {
			return val.ToString(CultureInfo.InvariantCulture.NumberFormat);
		}
		
		
		
		/// <summary>
		/// Convert the specified val.
		/// </summary>
		/// <param name='val'>
		/// Value.
		/// </param>
		public string convert(string val) {
			val = val.Replace("\"", "\\\"");
			//return this.convert(val, false);
			return val;
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