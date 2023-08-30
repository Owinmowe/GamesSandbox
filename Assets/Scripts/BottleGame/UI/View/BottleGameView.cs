using System;
using System.Collections.Generic;
using BottleGame.Core;
using BottleGame.Data;
using UnityEngine;
using BottleGame.UI.Controls;
using BottleGame.Data.Configuration;


namespace BottleGame.UI.View
{
    /// <summary>
    /// View class of the BottleGame. This class is a subclass of the View class.
    /// </summary>
    public class BottleGameView : GamesSandbox.MVP.View
    {
        [Header("BottleGame Specific")]
        [SerializeField, Tooltip("GameplayScreenControl reference in scene.")] 
        private GameplayScreenControl gameplayScreenControl;

        [SerializeField, Tooltip("Gameplay configuration data used to set the gameplay in the Gameplay Presenter.")] 
        private GameplayConfigurationData gameplayConfigurationData;

        #region GAMEPLAY_SCREEN_EVENTS
        
        /// <summary>
        /// Event called from BottleGameView when GameplayScreenControl calls the same event. <br/>
        /// The GameplayConfigurationData is added by the BottleGameView before calling the event.
        /// </summary>
        public event Action<GameplayConfigurationData> OnStartGameEvent;
        
        /// <summary>
        /// Event called from BottleGameView when GameplayScreenControl calls the same event. <br/>
        /// The BottleMixData comes from BottleController events.
        /// </summary>
        public event Action<BottlesMixData> OnBottleMixEvent;
        
        /// <summary>Event called from BottleGameView when GameplayScreenControl calls the same event.</summary>
        public event Action OnGameplayBackToMenuButtonEvent;

        #endregion

        protected override void OnStarted()
        {
            AddEvents();
            OpenStartScreen();
        }

        protected override void OnDestroyed()
        {
            RemoveEvents();
        }

        private void AddEvents()
        {
            startScreenControl.OnExitButtonPressed += StartScreenOnExitButtonEvent;
            startScreenControl.OnStartButtonPressed += StartScreenOnPlayButtonEvent;
            startScreenControl.OnSettingsButtonPressed += StartScreenOnSettingsButtonEvent;

            settingsScreenControl.OnBackButtonPressed += SettingsScreenOnBackButtonEvent;
            
            gameplayScreenControl.OnBackButtonPressed += OnGameplayBackToMenuButtonEvent;
            gameplayScreenControl.OnBottleMixEvent += OnBottleMixEvent;
            
            // TODO Improve Gameplay Presenter StartGame calls
            // Since GameplayPresenter is a BottleGameView specific Presenter, events that must call methods 
            // on that presenter must be added separately.
            startScreenControl.OnStartButtonPressed += OnStartGame;
            postGameScreenControl.OnPostGameResetGameButtonPressed += OnStartGame;

            postGameScreenControl.OnPostGameBackButtonPressed += PostGameScreenOnBackToMenuButtonEvent;
            postGameScreenControl.OnPostGameResetGameButtonPressed += PostGameScreenOnResetButtonEvent;
        }

        private void RemoveEvents()
        {
            startScreenControl.OnExitButtonPressed -= StartScreenOnExitButtonEvent;
            startScreenControl.OnStartButtonPressed -= StartScreenOnPlayButtonEvent;
            startScreenControl.OnSettingsButtonPressed -= StartScreenOnSettingsButtonEvent;
            
            settingsScreenControl.OnBackButtonPressed -= SettingsScreenOnBackButtonEvent;
            
            gameplayScreenControl.OnBackButtonPressed -= OnGameplayBackToMenuButtonEvent;
            gameplayScreenControl.OnBottleMixEvent -= OnBottleMixEvent;
            
            // TODO Improve Gameplay Presenter StartGame calls
            // Since GameplayPresenter is a BottleGameView specific Presenter, events that must call methods 
            // on that presenter must be added separately.
            startScreenControl.OnStartButtonPressed -= OnStartGame;
            postGameScreenControl.OnPostGameResetGameButtonPressed -= OnStartGame;
            
            postGameScreenControl.OnPostGameBackButtonPressed -= PostGameScreenOnBackToMenuButtonEvent;
            postGameScreenControl.OnPostGameResetGameButtonPressed -= PostGameScreenOnResetButtonEvent;
        }

        private void OnStartGame() => OnStartGameEvent?.Invoke(gameplayConfigurationData);
        
        #region GAMEPLAY_SCREEN_CONTROL

        /// <summary>Method for opening the Gameplay Screen from a Presenter.</summary>
        public void OpenGameplayScreen() => gameplayScreenControl.OpenScreen();
        
        /// <summary>Method for closing the Gameplay Screen from a Presenter.</summary>
        public void CloseGameplayScreen() => gameplayScreenControl.CloseScreen();
        
        /// <summary>Method for calling the InitializeBottles method in the GameplayScreenControl.</summary>
        public void InitializeBottles(List<Bottle> bottles) => gameplayScreenControl.InitializeBottles(bottles);
        
        /// <summary>Method for calling the UpdateCurrentBottles method in the GameplayScreenControl.</summary>
        public void UpdateCurrentBottles(List<Bottle> bottles) => gameplayScreenControl.UpdateCurrentBottles(bottles);
        
        /// <summary>Method for calling the HideCurrentBottles method in the GameplayScreenControl.</summary>
        public void HideCurrentBottles() => gameplayScreenControl.DeInitializeBottles();

        #endregion
        
        #region POST_GAME_SCREEN_CONTROL
        
        #endregion

    }
}
