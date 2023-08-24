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
        
        private async void Start()
        {
            await InitializeView();
            OnStarted();
        }
        
        private void OnDestroy()
        {
            OnDestroyed();
        }

        /// <summary>
        /// Abstract method overriden by Views to have logic when the View component is initialized. This is not
        /// on the same time as Start method of MonoBehaviours since initializing a View takes time.
        /// </summary>
        protected abstract void OnStarted();
        
        /// <summary>
        /// Abstract Method overriden by Views to have logic when the object gets deleted. This is called on the same
        /// time as OnDestroy of MonoBehaviours.
        /// </summary>
        protected abstract void OnDestroyed();
        
        // Method used by Views to initialize all ScriptableData and Presenters.
        // Returns Awaitable System.Threading.Task that completes when initialization is completed.
        private async Task InitializeView()
        {
            await CacheScriptableDataTypes();
            CreateAllPresenters(this);
        }

        // Asynchronous Method used by View classes to cache all used ScriptableData. This method uses Addressables
        // and Assets Label References.
        // Returns a awaitable System.Threading.Task that completes when all corresponding ScriptableData
        // finishes loading
        private async Task CacheScriptableDataTypes()
        {
            _modelDataDictionary = new Dictionary<Type, ScriptableData>();
            
            var handle = Addressables.LoadAssetsAsync<ScriptableData>(scriptableDataLabel, null);
            await handle.Task;

            foreach (var scriptableData in handle.Result)
                _modelDataDictionary.Add(scriptableData.GetType(), scriptableData);
        }

        
        // Method used by View classes to create all Presenters. This method uses reflection on the calling Assembly
        // and the View type Assembly to find all Presenters in runtime.
        private void CreateAllPresenters(View view)
        {
            IEnumerable<Type> generalPresenterTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => typeof(Presenter<View>).IsAssignableFrom(type));
            
            foreach (Type type in generalPresenterTypes)
            {
                Activator.CreateInstance(type, new object[] { view });
            }
            
            Type viewType = view.GetType();
            Type specificType = typeof(Presenter<>).MakeGenericType(viewType);
            IEnumerable<Type> specificPresenterTypes = Assembly.GetAssembly(viewType).GetTypes()
                .Where(type => specificType.IsAssignableFrom(type));

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