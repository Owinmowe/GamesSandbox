using GamesSandbox.Core;

namespace BottleGame.Data
{
    /// <summary>
    /// PostGame Data class of the BottleGame. This class is a subclass of the PostGameData class and its cast to
    /// object through the MVP pipeline and then recast to its starting type in the corresponding Control.
    /// </summary>
    public class BottleGamePostGameData : PostGameData
    {
        /// <summary>Time taken to complete the game. </summary>
        public float TimeToComplete;
    }
}
