using UnityEngine;

namespace Utilities.MVP
{
    /// <summary>
    /// Base class for data created for each MVP screens. This data should only
    /// be accessed from the corresponding View and used in a Presenter class.
    /// </summary>
    public abstract class ScriptableData : ScriptableObject
    {
        public abstract ScriptableDataType DataType { get; }
    }
}
