using System;
using System.Reflection;
using AltairStudios.Core.Orm;


namespace AltairStudios.Core.Util {
	/// <summary>
	/// Reflection.
	/// </summary>
	public class Reflection {
		/// <summary>
		/// The instance.
		/// </summary>
		protected static Reflection instance;
		
		
		
		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>
		/// The instance.
		/// </value>
		public static Reflection Instance {
			get {
				if(Reflection.instance == null) {
					Reflection.instance = new Reflection();
				}
				return Reflection.instance;
			}
		}		
		
		
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AltairStudios.Core.Util.Reflection"/> class.
		/// </summary>
		private Reflection() {
		}
		
		
		
		/// <summary>
		/// Gets the assemblies.
		/// </summary>
		/// <returns>
		/// The assemblies.
		/// </returns>
		public Assembly[] getAssemblies() {
			AppDomain domain = AppDomain.CurrentDomain;
			Assembly[] assemblies = domain.GetAssemblies();
			
			return assemblies;
		}
		
		
		
		/// <summary>
		/// Gets the templatize models.
		/// </summary>
		/// <returns>
		/// The templatize models.
		/// </returns>
		public ModelList<Model> getTemplatizeModels() {
			Assembly[] assemblies = this.getAssemblies();
			ModelList<Model> models = new ModelList<Model>();
			Model auxModel = new Model();
			Type[] modelType = new Type[1];
			modelType[0] = typeof(Model);
			
			for(int i = 0; i < assemblies.Length; i++) {
				Type[] types = assemblies[i].GetTypes();
				for(int j = 0; j < types.Length; j++) {
					if(types[j].Namespace == "AltairStudios.Core.Orm.Models") {
						models.Add(auxModel.cast<Model>(Activator.CreateInstance(types[j])));
					}
				}
			}
			
			return models;
		}
	}
}