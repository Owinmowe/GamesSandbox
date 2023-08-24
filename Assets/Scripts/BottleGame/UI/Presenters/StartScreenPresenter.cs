using General.MVP;
using BottleGame.UI.Views;
using UnityEngine.Scripting;

#if !UNITY_EDITOR
using UnityEngine;
#endif

namespace BottleGame.UI.Presenters
{
    //TODO Improve functionality using a base View to recycle logic in different games in the sandbox (Presenter<View>)
    
    ///<summary>
    /// Presenter class of the BottleGame. This class is a subclass of the Presenter class and received
    /// the BottleGameView in its constructor.
    /// </summary>
    [Preserve]
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
