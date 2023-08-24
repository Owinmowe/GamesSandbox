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
        [Header("Controls")]

        [SerializeField, Tooltip("GameplayScreenControl reference in scene/prefab.")] 
        private GameplayScreenControl gameplayScreenControl;

        #region GAMEPLAY_SCREEN_EVENTS
        
        /// <summary>Event called from BottleGameView when GameplayScreenControl calls the same event.</summary>
        public event Action OnStartGameEvent;
        
        /// <summary>Event called from BottleGameView when GameplayScreenControl calls the same event.</summary>
        public event Action OnBackToMenuButtonEvent;

        #endregion

        private async void Start()
        {
            await InitializeView();
            AddEvents();
            OpenStartScreen();
        }
        
        private void OnDestroy()
        {
            RemoveEvents();
        }
        
        private void AddEvents()
        {
            startScreenControl.OnExitButtonPressed += OnExitButtonEvent;
            startScreenControl.OnStartButtonPressed += OnStartButtonEvent;
            startScreenControl.OnSettingsButtonPressed += OnSettingsButtonEvent;
            startScreenControl.OnStartButtonPressed += OnStartGameEvent;

            gameplayScreenControl.OnBackButtonPressed += OnBackToMenuButtonEvent;
        }

        private void RemoveEvents()
        {
            startScreenControl.OnExitButtonPressed -= OnExitButtonEvent;
            startScreenControl.OnStartButtonPressed -= OnStartButtonEvent;
            startScreenControl.OnSettingsButtonPressed -= OnSettingsButtonEvent;
            startScreenControl.OnStartButtonPressed -= OnStartGameEvent;
            
            gameplayScreenControl.OnBackButtonPressed -= OnBackToMenuButtonEvent;
        }

        #region GAMEPLAY_SCREEN_CONTROL

        /// <summary>Method for opening the Gameplay Screen from a Presenter.</summary>
        public void OpenGameplayScreen() => gameplayScreenControl.OpenScreen();
        
        /// <summary>Method for closing the Gameplay Screen from a Presenter.</summary>
        public void CloseGameplayScreen() => gameplayScreenControl.CloseScreen();

        #endregion

    }
}
