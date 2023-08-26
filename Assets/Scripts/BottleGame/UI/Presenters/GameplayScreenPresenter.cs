using BottleGame.Core;
using BottleGame.Data;
using BottleGame.Data.Configuration;
using GamesSandbox.MVP;
using BottleGame.UI.View;
using GamesSandbox.Core;
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
            View.OnGameplayBackToMenuButtonEvent += GameplayBackToMenu;
            View.OnStartGameEvent += StartGame;
            View.OnBottleMixEvent += MixBottles;
        }

        protected override void RemoveViewListeners()
        {
            View.OnGameplayBackToMenuButtonEvent -= GameplayBackToMenu;
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

        private void GameplayBackToMenu()
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
            
            View.HideCurrentBottles();
            
            _bottleGameLogic.OnGameStarted -= OnGameStarted;
            _bottleGameLogic.OnGameEnded -= OnGameEnded;
            _bottleGameLogic = null;
        }
        
        private void OnGameStarted()
        {
            View.InitializeBottles(_bottleGameLogic.CurrentBottles);
        }

        private void OnGameEnded(PostGameData postGameData)
        {
            DeInitializeBottleGameLogic();
            View.CloseGameplayScreen();
            
            View.SetPostScreenData(postGameData);
            View.OpenPostGameScreen();
        }

        private void MixBottles(BottlesMixData bottlesMixData)
        {
            _bottleGameLogic.MixTwoBottlesLiquid(bottlesMixData);
            
            if(_bottleGameLogic is { GameActive: true })
                View.UpdateCurrentBottles(_bottleGameLogic.CurrentBottles);
        }
    }
}