using Utilities.MVP;
using BottleGame.UI.Views;

namespace BottleGame.UI.Presenters
{
    public class StartScreenPresenter : Presenter<BottleGameView>
    {
        public StartScreenPresenter(BottleGameView view) : base(view)
        {
            
        }

        protected override void AddViewListeners()
        {
            View.OnStartButtonEvent += OnStart;
            View.OnSettingsButtonEvent += OnSettings;
            View.OnExitButtonEvent += OnExit;
        }

        protected override void RemoveViewListeners()
        {
            View.OnStartButtonEvent -= OnStart;
            View.OnSettingsButtonEvent -= OnSettings;
            View.OnExitButtonEvent -= OnExit;
        }

        private void OnStart()
        {
            View.CloseStartScreen();
            View.OpenGameplayScreen();
        }

        private void OnSettings()
        {
            
        }
        
        private void OnExit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); 
#endif
        }

    }
}
