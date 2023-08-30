using System;

namespace GamesSandbox.Core
{
    /// <summary>
    /// Base interface for all gameLogics in the GamesSandbox.  
    /// </summary>
    public interface IGameLogic
    {
        /// <summary>Event called after a game that implements IGameLogic interface starts.</summary>
        event Action OnGameStarted;
        
        /// <summary>
        /// Event called after a game that implements IGameLogic interface ends.
        /// <typeparam name = "PostGameData">The type of data to get. This data must be a subclass of ScriptableData
        /// that already exist when the Awake method of this component is called.</typeparam>
        /// </summary>
        event Action<PostGameData> OnGameEnded; 

        /// <summary>Method for starting the game implementing the IGameLogic interface.</summary>
        void StartGame();
        
        /// <summary>Method for stopping the game implementing the IGameLogic interface.</summary>
        void StopGame();
    }
}

