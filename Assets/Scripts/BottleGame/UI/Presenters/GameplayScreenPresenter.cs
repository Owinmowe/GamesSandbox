using General.MVP;
using BottleGame.UI.Views;
using UnityEngine.Scripting;

namespace BottleGame.UI.Presenters
{
    /// <summary>
    /// Presenter class of the BottleGame. This class is a subclass of the Presenter class and received
    /// the BottleGameView in its constructor.
    /// </summary>
    [Preserve]
    public class GameplayScreenPresenter: Presenter<BottleGameView>
    {
        public GameplayScreenPresenter(BottleGameView view) : base(view)
        {
            
        }

        protected override void AddViewListeners()
        {
            View.OnBackToMenuButtonEvent += BackToMenu;
            View.OnStartGameEvent += StartGame;
        }

        protected override void RemoveViewListeners()
        {
            View.OnBackToMenuButtonEvent -= BackToMenu;
            View.OnStartGameEvent -= StartGame;
        }

        private void StartGame()
        {
            View.CloseStartScreen();
            View.OpenGameplayScreen();
        }

        private void BackToMenu()
        {
            View.CloseGameplayScreen();
            View.OpenStartScreen();
        }
    }
}