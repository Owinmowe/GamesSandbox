using System.Collections.Generic;
using UnityEngine;

namespace BottleGame.Data
{
    /// <summary>
    /// Data class for possible liquids type in BottleGame. This class is only modifiable before build time
    /// (Scriptable objects don't work the same way on builds that on editor).
    /// </summary>
    [CreateAssetMenu(fileName = "Liquids Type", menuName = "BottleGame/Core/Liquids Type", order = 1)]
    public class LiquidsType : ScriptableObject
    {
        /// <summary>Possible liquid types data. This includes color.</summary>
        public List<LiquidTypeData> liquidsTypeData;
    }
}
