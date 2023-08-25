using UnityEngine;

namespace BottleGame.Data.Configuration
{
    /// <summary>
    /// Data class for Gameplay Configurations in BottleGame. This class is directly used by the BottleGame
    /// GameplayScreenPresenter to create a logic version of the BottleGame with the data.
    /// This class is only modifiable before build time (Scriptable objects don't work the same way on builds that on editor).
    /// </summary>
    [CreateAssetMenu(fileName = "Gameplay Configuration Data", menuName = "BottleGame/Core/Gameplay Configuration Data", order = 1)]
    public class GameplayConfigurationData : ScriptableObject
    {
        /// <summary>Amount of empty bottles that will be created. </summary>
        public int emptyBottlesAmount;
        
        /// <summary>Amount of bottles that will be created and filled. </summary>
        public int fullBottlesAmount;
        
        /// <summary>The maximum amount of liquid that each bottle will be able to carry. </summary>
        public int bottlesMaxCapacity;
        
        /// <summary>
        /// The amount of slots for liquid for each bottle. Each slot is a fixed amount of liquid
        /// that will be filled on each bottle on creation.
        /// </summary>
        public int bottlesSlotsAmount;
        
        /// <summary>
        /// All possible liquid types. When creating bottles this will define the amount of variety of colors.
        /// </summary>
        public LiquidsType possibleLiquids;
    }
}
