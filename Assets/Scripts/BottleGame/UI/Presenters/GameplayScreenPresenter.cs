using BottleGame.Core;
using BottleGame.Data;
using BottleGame.Data.Configuration;
using General.MVP;
using BottleGame.UI.Views;
using UnityEngine.Scripting;

namespace BottleGame.UI.Presenters
{
    /// <summary>
    /// Presenter class of the BottleGame. This class is a subclass of the Presenter class and received
    /// the BottleGameView in its constructor. This class contains a logic representation of the game
    /// based on GameplayConfigurationData
    /// </summary>
    [Preserve]
    public class GameplayScreenPresenter: Presenter<BottleGameView>
    {

        private BottleGameLogic _bottleGameLogic;
        
        public GameplayScreenPresenter(BottleGameView view) : base(view)
        {
            
        }

        protected override void AddViewListeners()
        {
            View.OnBackToMenuButtonEvent += BackToMenu;
            View.OnStartGameEvent += StartGame;
            View.OnBottleMixEvent += MixBottles;
        }

        protected override void RemoveViewListeners()
        {
            View.OnBackToMenuButtonEvent -= BackToMenu;
            View.OnStartGameEvent -= StartGame;
            View.OnBottleMixEvent -= MixBottles;
        }

        private void StartGame(GameplayConfigurationData gameplayConfigurationData)
        {
            View.CloseStartScreen();
            View.OpenGameplayScreen();

            InitializeBottleGameLogic(gameplayConfigurationData);
            _bottleGameLogic.StartGame();
        }

        private void BackToMenu()
        {
            DeInitializeBottleGameLogic();
            View.CloseGameplayScreen();
            View.OpenStartScreen();
        }

        private void InitializeBottleGameLogic(GameplayConfigurationData gameplayConfigurationData)
        {
            _bottleGameLogic = new BottleGameLogic(gameplayConfigurationData);
            
            _bottleGameLogic.OnGameStarted += OnGameStarted;
            _bottleGameLogic.OnGameEnded += OnGameEnded;
        }

        private void DeInitializeBottleGameLogic()
        {
            if (_bottleGameLogic == null) return;
            
            _bottleGameLogic.OnGameStarted -= OnGameStarted;
            _bottleGameLogic.OnGameEnded -= OnGameEnded;
            _bottleGameLogic = null;
        }
        
        private void OnGameStarted()
        {
            View.InitializeBottles(_bottleGameLogic.CurrentBottles);
        }

        private void OnGameEnded()
        {
            View.HideCurrentBottles();
        }

        private void MixBottles(BottlesMixData bottlesMixData)
        {
            _bottleGameLogic.MixTwoBottlesLiquid(bottlesMixData);
            View.UpdateCurrentBottles(_bottleGameLogic.CurrentBottles);
        }
    }
}