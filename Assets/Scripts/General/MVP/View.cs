using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace General.MVP
{
    /// <summary>
    /// Base class for View in MVP design. This class is responsible of receiving events
    /// from controls and send that information to the corresponding Presenter. Then receiving
    /// actions from the presenter to affect the game or call controls to do it.
    /// </summary>
    public abstract class View : MonoBehaviour
    {
        private Dictionary<Type, ScriptableData> _modelDataDictionary;

        private void Awake()
        {
            CacheScriptableDataTypes();
        }

        /// This method uses reflection to get all existing ScriptableData types, creates an instance and caches
        /// the data in a dictionary for faster access from Presenters. This is a conscious trade-off with dynamic
        /// data types not being able to be added in runtime.
        private void CacheScriptableDataTypes()
        {
            _modelDataDictionary = new Dictionary<Type, ScriptableData>();

            // We get all possible data type that are assignable to ScriptableData except the base ScriptableData class.
            var allScriptableDataTypes = Assembly.GetExecutingAssembly().GetTypes().Where(
                type => typeof(ScriptableData).IsAssignableFrom(type) && type != typeof(ScriptableData)).ToArray();

            // Then we create a new instance of each type of ScriptableData type and add it to the dictionary.
            foreach (Type type in allScriptableDataTypes)
            {
                ScriptableData scriptableData = (ScriptableData)Activator.CreateInstance(type);
                _modelDataDictionary.Add(type, scriptableData);
            }
        }

        /// <summary>
        /// Method used by View classes to create all Presenters.
        /// <param name ="view">The View component that will be injected into the Presenters.</param>
        /// <typeparam name ="T">The type of View that will create the Presenters.</typeparam>
        /// </summary>
        protected void CreateAllPresenters<T>(View view)
        {
            var allValidPresenterTypes = Assembly.GetCallingAssembly().GetTypes()
                .Where(type => typeof(Presenter<T>).IsAssignableFrom(type));
            
            foreach (Type type in allValidPresenterTypes)
            {
                Activator.CreateInstance(type, new object[] { view });
            }
        }

        /// <summary>
        /// Method used by Presenters to get ScriptableData based on data type. 
        /// <typeparam name ="T">The type of data to get. This data must be a subclass of ScriptableData
        /// that already exist when the Awake method of this component is called.</typeparam>
        /// <returns>ScriptableData instance of requested type or null if not found.</returns>
        /// </summary>
        public T GetData<T>() where T : ScriptableData
        {
            if (_modelDataDictionary.TryGetValue(typeof(T), out ScriptableData data))
            {
                return (T)data;
            }

            Debug.LogError("Error in View: " + name + " - Data Type Requested: " + typeof(T));
            Debug.LogError("Call to View GetData method returns no valid data.");
            Debug.LogError("Check if requested data derives from ScriptableData.");
            return null;
        }
    }
}