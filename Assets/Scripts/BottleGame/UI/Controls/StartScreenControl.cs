using System;
using UnityEngine;
using UnityEngine.UI;
using General.MVP;

namespace BottleGame.UI.Controls
{
    /// <summary>
    /// Control class of the BottleGame. This class implements IScreenControl interface.
    /// </summary>
    public class StartScreenControl : MonoBehaviour, IScreenControl
    {
        /// <summary>Event called from StartScreenControl when exit button is pressed.</summary>
        public event Action OnExitButtonPressed;
        
        /// <summary>Event called from StartScreenControl when start button is pressed.</summary>
        public event Action OnStartButtonPressed;
        
        /// <summary>Event called from StartScreenControl when settings button is pressed.</summary>
        public event Action OnSettingsButtonPressed;
        
        [Header("Buttons")]
        [SerializeField] private Button startButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButtonButton;
        
        private void Start()
        {
            startButton.onClick.AddListener(StartButtonPressed);   
            settingsButton.onClick.AddListener(SettingsButtonPressed);   
            exitButtonButton.onClick.AddListener(ExitButtonPressed);   
        }

        private void OnDestroy()
        {
            startButton.onClick.RemoveListener(StartButtonPressed);   
            settingsButton.onClick.RemoveListener(SettingsButtonPressed);   
            exitButtonButton.onClick.RemoveListener(ExitButtonPressed);   
        }

        private void StartButtonPressed() => OnStartButtonPressed?.Invoke();
        private void SettingsButtonPressed() => OnSettingsButtonPressed?.Invoke();
        private void ExitButtonPressed() => OnExitButtonPressed?.Invoke();
        
        /// <summary>Method for enabling the StartScreen gameObject.</summary>
        public void OpenScreen() => gameObject.SetActive(true);
        
        /// <summary>Method for disabling the StartScreen gameObject.</summary>
        public void CloseScreen() => gameObject.SetActive(false);
    }
}
