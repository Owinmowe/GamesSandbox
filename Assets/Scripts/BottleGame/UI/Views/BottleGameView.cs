using System;
using UnityEngine;
using BottleGame.UI.Controls;
using General.MVP;


namespace BottleGame.UI.Views
{
    /// <summary>
    /// View class of the BottleGame. This class is a subclass of the View class.
    /// </summary>
    public class BottleGameView : View
    {
        [Header("BottleGame Specific Controls")]
        [SerializeField, Tooltip("GameplayScreenControl reference in scene/prefab.")] 
        private GameplayScreenControl gameplayScreenControl;

        #region GAMEPLAY_SCREEN_EVENTS
        
        /// <summary>Event called from BottleGameView when GameplayScreenControl calls the same event.</summary>
        public event Action OnStartGameEvent;
        
        /// <summary>Event called from BottleGameView when GameplayScreenControl calls the same event.</summary>
        public event Action OnBackToMenuButtonEvent;

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
            
            gameplayScreenControl.OnBackButtonPressed += OnBackToMenuButtonEvent;
            
            startScreenControl.OnStartButtonPressed += OnStartGameEvent;
        }

        private void RemoveEvents()
        {
            startScreenControl.OnExitButtonPressed -= StartScreenOnExitButtonEvent;
            startScreenControl.OnStartButtonPressed -= StartScreenOnPlayButtonEvent;
            startScreenControl.OnSettingsButtonPressed -= StartScreenOnSettingsButtonEvent;
            
            settingsScreenControl.OnBackButtonPressed -= SettingsScreenOnBackButtonEvent;
            
            gameplayScreenControl.OnBackButtonPressed -= OnBackToMenuButtonEvent;
            
            startScreenControl.OnStartButtonPressed -= OnStartGameEvent;
        }

        #region GAMEPLAY_SCREEN_CONTROL

        /// <summary>Method for opening the Gameplay Screen from a Presenter.</summary>
        public void OpenGameplayScreen() => gameplayScreenControl.OpenScreen();
        
        /// <summary>Method for closing the Gameplay Screen from a Presenter.</summary>
        public void CloseGameplayScreen() => gameplayScreenControl.CloseScreen();

        #endregion

    }
}
