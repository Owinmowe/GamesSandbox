using Utilities.MVP;
using BottleGame.UI.Views;

namespace BottleGame.UI.Presenters
{
    public class GameplayScreenPresenter: Presenter<BottleGameView>
    {
        public GameplayScreenPresenter(BottleGameView view) : base(view)
        {
            
        }

        protected override void AddViewListeners()
        {
            View.OnBackToMenuButtonEvent += ReturnToMenu;
            View.OnStartGameEvent += StartGame;
        }

        protected override void RemoveViewListeners()
        {
            View.OnBackToMenuButtonEvent -= ReturnToMenu;
            View.OnStartGameEvent -= StartGame;
        }

        private void StartGame()
        {
            
        }

        private void ReturnToMenu()
        {
            View.CloseGameplayScreen();
            View.OpenStartScreen();
        }
    }
}