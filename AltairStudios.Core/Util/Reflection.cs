using System;
using System.Reflection;
using AltairStudios.Core.Orm;
using AltairStudios.Core.Plugin;


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
					//types[j].ba
					if(types[j].IsGenericType == false && this.isChildOf(types[j], typeof(Orm.Model))) {
					//if(types[j].GetNestedType("AltairStudios.Core.Orm.Model") != null) {
					//if(types[j].Namespace != null && (types[j].Namespace == "AltairStudios.Core.Orm.Models" || types[j].Namespace.EndsWith(".Model"))) {
						models.Add(auxModel.cast<Model>(Activator.CreateInstance(types[j])));
					}
				}
			}
			
			return models;
		}
		
		
		
		public bool isChildOf(Type objectType, Type baseType) {
			if(objectType == baseType) {
				return true;
			} else if(objectType.BaseType == null) {
				return false;
			} else {
				return this.isChildOf(objectType.BaseType, baseType);
			}
		}
		
		
		
		/// <summary>
		/// Gets the model from string.
		/// </summary>
		/// <returns>
		/// The model from string.
		/// </returns>
		/// <param name='modelName'>
		/// Model name.
		/// </param>
		public Model getModelFromString(string modelName) {
			ModelList<Model> models = this.getTemplatizeModels();
			
			for(int i = 0; i < models.Count; i++) {
				if(models[i].ToString() == modelName) {
					return models[i];
				}
			}
			
			return null;
		}
		
		
		
		/// <summary>
		/// Gets the core plugins.
		/// </summary>
		/// <returns>
		/// The core plugins.
		/// </returns>
		public ModelList<PluginBase> getCorePlugins() {
			Assembly[] assemblies = this.getAssemblies();
			ModelList<PluginBase> plugins = new ModelList<PluginBase>();
			Model auxModel = new Model();
			
			for(int i = 0; i < assemblies.Length; i++) {
				Type[] types = assemblies[i].GetTypes();
				for(int j = 0; j < types.Length; j++) {
					if(types[j].Namespace != null && types[j].Namespace.Contains("Plugin.") && types[j].GetInterface("iPlugin") != null) {
						plugins.Add(auxModel.cast<PluginBase>(Activator.CreateInstance(types[j])));
					}
				}
			}
			
			return plugins;
		}
	}
}