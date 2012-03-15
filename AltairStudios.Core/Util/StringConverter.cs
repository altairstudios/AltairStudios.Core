using System;
using System.Globalization;


namespace AltairStudios.Core.Util {
	public class StringConverter {		
		public StringConverter() {
		}
		
		public string convert(object val, Type type) {
			string converted = "";
			switch(type.Name) {
				case "Int32": converted = this.convert((int)val); break;
				case "String": converted = "\"" + this.convert((string)val) + "\""; break;
				case "Double": converted = this.convert((double)val); break;
				case "Boolean": converted = this.convert((bool)val); break;
				case "Model": converted = "null"; break;
				default: converted = "\"\""; break;
			}
			
			return converted;
		}
		
		public string convert(object val) {
			return val.ToString();
		}
		
		public string convert(bool val) {
			if(val) {
				return "true";
			} else {
				return "false";
			}
		}
		
		public string convert(int val) {
			return val.ToString(CultureInfo.InvariantCulture.NumberFormat);
		}
		
		public string convert(double val) {
			return val.ToString(CultureInfo.InvariantCulture.NumberFormat);
		}
		
		public string convert(string val) {
			val = val.Replace("\"", "\\\"");
			//return this.convert(val, false);
			return val;
		}
		
		/*public string convert(string val, bool doubleEscape) {
			if(doubleEscape) {
				val = val.Replace("\"", "'");
			} else {
				val = val.Replace("'", "\"");
			}
			
			return val;
		}*/
		
		public T cast<T>(object o) {
			return (T)o;
		}
	}
}