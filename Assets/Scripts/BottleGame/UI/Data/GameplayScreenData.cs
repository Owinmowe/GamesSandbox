using UnityEngine;
using Utilities.MVP;

namespace BottleGame.UI.Data
{
    [CreateAssetMenu(fileName = "Gameplay Screen Data", menuName = "BottleGame/UI/Gameplay Screen Data", order = 1)]
    public class GameplayScreenData : ScriptableData
    {
        public override ScriptableDataType DataType => ScriptableDataType.GameplayScreenData;
    }
}