using System;

namespace AltairStudios.Core.Mvc {
	/// <summary>
	/// IModelizable interface to implement in Model classes.
	/// </summary>
	public interface IModelizable {
		/// <summary>
		/// Serialize model to json.
		/// </summary>
		/// <returns>
		/// The json.
		/// </returns>
		string ToJson();
	}
}