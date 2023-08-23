using System;
using UnityEngine;
using BottleGame.UI.Controls;
using BottleGame.UI.Presenters;
using General.MVP;


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

        private GameplayScreenPresenter _gameplayScreenPresenter;
        private StartScreenPresenter _startScreenPresenter;

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
            _startScreenPresenter = new StartScreenPresenter(this);
            _gameplayScreenPresenter = new GameplayScreenPresenter(this);
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
        
        /// <summary>Method for opening the Start Screen from a Presenter.</summary>
        public void OpenStartScreen() => startScreenControl.OpenScreen();
        
        /// <summary>Method for closing the Start Screen from a Presenter.</summary>
        public void CloseStartScreen() => startScreenControl.CloseScreen();

        #endregion

        #region GAMEPLAY_SCREEN_CONTROL

        /// <summary>Method for opening the Gameplay Screen from a Presenter.</summary>
        public void OpenGameplayScreen() => gameplayScreenControl.OpenScreen();
        
        /// <summary>Method for closing the Gameplay Screen from a Presenter.</summary>
        public void CloseGameplayScreen() => gameplayScreenControl.CloseScreen();

        #endregion

    }
}
