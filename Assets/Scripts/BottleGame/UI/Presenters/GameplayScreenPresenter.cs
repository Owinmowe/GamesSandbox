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