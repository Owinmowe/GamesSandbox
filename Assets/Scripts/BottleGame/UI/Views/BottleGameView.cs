using System;
using UnityEngine;
using BottleGame.UI.Controls;
using Utilities.MVP;

//using BottleGame.UI.Presenters;


namespace BottleGame.UI.Views
{
    /// <summary>
    /// View class of the BottleGame. This class is a subclass of the View class.
    /// </summary>
    public class BottleGameView : View
    {
        [Header("Controls")] 
        [SerializeField] private StartScreenControl startScreenControl;
        [SerializeField] private GameplayScreenControl gameplayScreenControl;

        //private GameplayScreenPresenter _gameplayScreenPresenter;
        //private StartScreenPresenter _startScreenPresenter;

        #region START_SCREEN_EVENTS

        public event Action OnExitButtonEvent;
        public event Action OnStartButtonEvent;
        public event Action OnSettingsButtonEvent;

        #endregion
        
        #region GAMEPLAY_SCREEN_EVENTS

        public event Action OnStartGameEvent;
        public event Action OnBackToMenuButtonEvent;

        #endregion
        
        private new void Awake()
        {
            base.Awake();
            //TODO Add Presenters construction
        }

        private void Start()
        {
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

            gameplayScreenControl.OnStartGameEvent += OnStartGameEvent;
            gameplayScreenControl.OnBackButtonPressed += OnBackToMenuButtonEvent;
        }

        private void RemoveEvents()
        {
            startScreenControl.OnExitButtonPressed -= OnExitButtonEvent;
            startScreenControl.OnStartButtonPressed -= OnStartButtonEvent;
            startScreenControl.OnSettingsButtonPressed -= OnSettingsButtonEvent;
            
            gameplayScreenControl.OnStartGameEvent -= OnStartGameEvent;
            gameplayScreenControl.OnBackButtonPressed -= OnBackToMenuButtonEvent;
        }

        #region START_SCREEN_CONTROL
        
        public void OpenStartScreen() => startScreenControl.OpenScreen();
        public void CloseStartScreen() => startScreenControl.CloseScreen();

        #endregion

        #region GAMEPLAY_SCREEN_CONTROL

        public void OpenGameplayScreen() => gameplayScreenControl.OpenScreen();
        public void CloseGameplayScreen() => gameplayScreenControl.CloseScreen();

        #endregion

    }
}
