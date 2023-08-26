using System;
using System.Collections.Generic;
using BottleGame.Core;
using BottleGame.Data;
using BottleGame.GameObjects;
using UnityEngine;
using UnityEngine.UI;
using GamesSandbox.MVP;

namespace BottleGame.UI.Controls
{
    /// <summary>
    /// Gameplay Control class of the BottleGame. This class implements IScreenControl interface.
    /// </summary>
    public class GameplayScreenControl : MonoBehaviour, IScreenControl
    {
        /// <summary>Event called from GameplayScreenControl when back button is pressed. </summary>
        public event Action OnBackButtonPressed;
        
        /// <summary>Event called from GameplayScreenControl when BottleController receives a BottleMixEvent. </summary>
        public event Action<BottlesMixData> OnBottleMixEvent;
        
        [Header("Buttons")]
        [SerializeField] private Button backToMenuButton;

        private PlayerController _currentPlayerController;
        private BottlesController _currentBottlesController;

        private void Awake()
        {
            backToMenuButton.onClick.AddListener(BackToMenuButtonPressed);
            
            _currentPlayerController = FindObjectOfType<PlayerController>(includeInactive: true);
            _currentBottlesController = FindObjectOfType<BottlesController>(includeInactive: true);

            _currentPlayerController.OnBottlesMixEvent += OnBottleMixEvent;
        }

        private void OnDestroy()
        {
            backToMenuButton.onClick.RemoveListener(BackToMenuButtonPressed);
            
            _currentPlayerController.OnBottlesMixEvent -= OnBottleMixEvent;
        }

        private void BackToMenuButtonPressed() => OnBackButtonPressed?.Invoke();

        /// <summary>Method for calling the InitializeBottles method in the BottlesController.</summary>
        public void InitializeBottles(List<Bottle> bottles) => _currentBottlesController.InitializeBottles(bottles);
        
        /// <summary>Method for calling the InitializeBottles method in the BottlesController.</summary>
        public void UpdateCurrentBottles(List<Bottle> bottles) => _currentBottlesController.UpdateCurrentBottles(bottles);
        
        /// <summary>Method for calling the InitializeBottles method in the BottlesController.</summary>
        public void DeInitializeBottles() => _currentBottlesController.DeInitializeBottles();
        

        /// <summary>Method for enabling the GameplayScreen gameObject and calling OnStartGameEvent.</summary>
        public void OpenScreen()
        {
            gameObject.SetActive(true);
        }

        /// <summary>Method for disabling the GameplayScreen gameObject and hiding all current bottles.</summary>
        public void CloseScreen()
        {
            gameObject.SetActive(false);
        }
    }
}