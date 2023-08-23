using System;
using UnityEngine;
using UnityEngine.UI;
using Utilities.MVP;

namespace BottleGame.UI.Controls
{
    /// <summary>
    /// Control class of the BottleGame. This class implements IScreenControl interface
    /// </summary>
    public class GameplayScreenControl : MonoBehaviour, IScreenControl
    {
        /// <summary>Event called from GameplayScreenControl when back button is pressed.</summary>
        public event Action OnBackButtonPressed;
        
        /// <summary>Event called from GameplayScreenControl when game has started.</summary>
        public event Action OnStartGameEvent;
        
        [Header("Buttons")]
        [SerializeField] private Button backToMenuButton;

        private void Awake()
        {
            backToMenuButton.onClick.AddListener(BackToMenuButtonPressed);
            HideCurrentBottles();
        }

        private void OnDestroy()
        {
            backToMenuButton.onClick.RemoveListener(BackToMenuButtonPressed);
        }

        private void BackToMenuButtonPressed() => OnBackButtonPressed?.Invoke();
        
        private void HideCurrentBottles()
        {
            // TODO Add hide current bottles functionality
        }
        
        /// <summary>Method for enabling the GameplayScreen gameObject and calling OnStartGameEvent.</summary>
        public void OpenScreen()
        {
            gameObject.SetActive(true);
            OnStartGameEvent?.Invoke();
        }

        /// <summary>Method for disabling the GameplayScreen gameObject and hiding all current bottles.</summary>
        public void CloseScreen()
        {
            HideCurrentBottles();
            gameObject.SetActive(false);
        }

        // TODO Add show current bottles functionality
        /*
        public void ShowCurrentBottles(List<Bottle> bottles)
        {
            
        }
        */
    }
}