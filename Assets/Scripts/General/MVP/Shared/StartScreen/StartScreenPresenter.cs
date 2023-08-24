using UnityEngine.Scripting;

#if !UNITY_EDITOR
using UnityEngine;
#endif

namespace General.MVP.Shared.StartScreen
{
    ///<summary>
    /// Shared Presenter class of all games. This class is a subclass of the Presenter class and receives
    /// a generic View in its constructor.
    /// </summary>
    [Preserve]
    public class StartScreenPresenter : Presenter<View> 
    {
        
        public StartScreenPresenter(View view) : base(view)
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
