using UnityEngine.Scripting;

#if !UNITY_EDITOR
using UnityEngine;
#endif

namespace General.MVP.Shared.SettingsScreen
{
    ///<summary>
    /// Shared Presenter class of all games. This class is a subclass of the Presenter class and receives
    /// a generic View in its constructor.
    /// </summary>
    [Preserve]
    public class SettingsScreenPresenter : Presenter<View> 
    {
        
        public SettingsScreenPresenter(View view) : base(view)
        {
            
        }

        protected override void AddViewListeners()
        {
            View.SettingsScreenOnBackButtonEvent += OnBack;
        }

        protected override void RemoveViewListeners()
        {
            View.SettingsScreenOnBackButtonEvent -= OnBack;
        }

        private void OnBack()
        {
            View.CloseSettingScreen();
            View.OpenStartScreen();
        }
    }
}
