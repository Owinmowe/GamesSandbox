using UnityEngine.Scripting;

namespace GamesSandbox.MVP.Shared.PostGameScreen
{
    /// <summary>
    /// PostGame Presenter class. This class is a shared subclass of the Presenter class that receives all post game events.
    /// </summary>
    [Preserve]
    public class PostGameScreenPresenter: Presenter<View>
    {
        public PostGameScreenPresenter(View view) : base(view)
        {
            
        }

        protected override void AddViewListeners()
        {
            View.PostGameScreenOnResetButtonEvent += ResetGame;
            View.PostGameScreenOnBackToMenuButtonEvent += BackToMenu;
        }

        protected override void RemoveViewListeners()
        {
            View.PostGameScreenOnResetButtonEvent -= ResetGame;
            View.PostGameScreenOnBackToMenuButtonEvent -= BackToMenu;
        }

        private void ResetGame()
        {
            View.ClosePostGameScreen();
        }

        private void BackToMenu()
        {
            View.ClosePostGameScreen();
            View.OpenStartScreen();
        }
    }
}