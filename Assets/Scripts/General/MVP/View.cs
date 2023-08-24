using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace General.MVP
{
    /// <summary>
    /// Base class for View in MVP design. This class is responsible of receiving events
    /// from controls and send that information to the corresponding Presenter. Then receiving
    /// actions from the presenter to affect the game or call controls to do it.<br/>
    /// This class is <b>partial</b> so every shared View functionality doesn't clog the main cs file.
    /// </summary>
    public abstract partial class View : MonoBehaviour
    {
        
        [Header("Data")] 
        [SerializeField, Tooltip("Addressables Asset Label referencing all necessary Data for a given View.")] 
        private AssetLabelReference scriptableDataLabel;
        
        private Dictionary<Type, ScriptableData> _modelDataDictionary;
        
        
        //TODO Add method call with CacheScriptableDataTypes and CreateAllPresenters as Initialization in an Awake 

        /// <summary>
        /// Asynchronous Method used by View classes to cache all used ScriptableData. This method uses Addressables
        /// and Assets Label References.
        /// <returns>Awaitable <b>System.Threading.Task</b> that completes when all corresponding ScriptableData
        /// finishes loading.</returns>
        /// </summary>
        protected async Task CacheScriptableDataTypes()
        {
            _modelDataDictionary = new Dictionary<Type, ScriptableData>();
            
            var handle = Addressables.LoadAssetsAsync<ScriptableData>(scriptableDataLabel, null);
            await handle.Task;

            foreach (var scriptableData in handle.Result)
                _modelDataDictionary.Add(scriptableData.GetType(), scriptableData);
        }

        /// <summary>
        /// Method used by View classes to create all Presenters. This method uses reflection on the calling
        /// and executing Assembly.
        /// <param name ="view">The View component that will be injected into the Presenters.</param>
        /// <typeparam name ="T">The type of View that will create the Presenters.</typeparam>
        /// </summary>
        protected void CreateAllPresenters<T>(View view)
        {
            var generalPresenterTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => typeof(Presenter<View>).IsAssignableFrom(type)).ToArray();
            
            foreach (Type type in generalPresenterTypes)
            {
                Activator.CreateInstance(type, new object[] { view });
            }
            
            var specificPresenterTypes = Assembly.GetCallingAssembly().GetTypes()
                .Where(type => typeof(Presenter<T>).IsAssignableFrom(type));

            foreach (Type type in specificPresenterTypes)
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