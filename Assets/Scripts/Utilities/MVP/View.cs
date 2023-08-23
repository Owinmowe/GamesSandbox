using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Utilities.MVP
{
    /// <summary>
    /// Base class for View in MVP design. This class is responsible of receiving events
    /// from controls and send that information to the corresponding Presenter. Then receiving
    /// actions from the presenter to affect the game or call controls to do it.
    /// </summary>
    public abstract class View : MonoBehaviour
    {
        private Dictionary<ScriptableDataType, ScriptableData> _modelDataDictionary;

        /// <summary>
        /// This method uses reflection to get all existing ScriptableData and caches the data in a dictionary
        /// for faster access from Presenters
        /// </summary>
        public void Awake()
        {
            _modelDataDictionary = new Dictionary<ScriptableDataType, ScriptableData>();

            var allScriptableDataTypes = Assembly.GetExecutingAssembly().GetTypes().Where(
                type => typeof(ScriptableData).IsAssignableFrom(type) && type != typeof(ScriptableData)).ToArray();

            foreach (Type type in allScriptableDataTypes)
            {
                ScriptableData scriptableData = (ScriptableData)Activator.CreateInstance(type);
                _modelDataDictionary.Add(scriptableData.DataType, scriptableData);
            }
        }

        /// <summary>Method used by Presenters to get ScriptableData based on enum type</summary>
        public ScriptableData GetData<T>(ScriptableDataType dataType) where T : ScriptableData
        {
            if (_modelDataDictionary.TryGetValue(dataType, out ScriptableData data))
            {
                return data;
            }
            
            Debug.LogError("Error in View: " + name + " - Data Type Requested: " + dataType);
            Debug.LogError("Call to View GetData method returns no valid data.");
            Debug.LogError("Check if corresponding data is added in View in Unity Editor.");
            return null;
        }
    }
}