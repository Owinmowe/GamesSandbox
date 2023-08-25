using System;
using UnityEngine;
using UnityEngine.UI;

namespace General.MVP.Shared.SettingsScreen
{
    /// <summary>
    /// Shared Settings Control class of all games. This class implements IScreenControl interface.
    /// </summary>
    public class SettingsScreenControl : MonoBehaviour, IScreenControl
    {
        /// <summary>Event called from SettingsScreenControl when exit button is pressed.</summary>
        public event Action OnBackButtonPressed;
        
        [Header("Buttons")]
        [SerializeField] private Button backButtonButton;
        
        private void Start()
        {
            backButtonButton.onClick.AddListener(BackButtonPressed);   
        }

        private void OnDestroy()
        { 
            backButtonButton.onClick.RemoveListener(BackButtonPressed);   
        }
        private void BackButtonPressed() => OnBackButtonPressed?.Invoke();
        
        /// <summary>Method for enabling the SettingsScreen gameObject.</summary>
        public void OpenScreen() => gameObject.SetActive(true);
        
        /// <summary>Method for disabling the SettingsScreen gameObject.</summary>
        public void CloseScreen() => gameObject.SetActive(false);
    }
}
