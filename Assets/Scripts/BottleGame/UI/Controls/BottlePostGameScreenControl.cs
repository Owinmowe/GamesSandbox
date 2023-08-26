using BottleGame.Data;
using UnityEngine;
using UnityEngine.UI;
using GamesSandbox.MVP.Shared.PostGameScreen;
using TMPro;

namespace BottleGame.UI.Controls
{
    /// <summary>
    /// PostGame Control class of the BottleGame. This class implements IScreenControl interface.
    /// </summary>
    public class BottlePostGameScreenControl : PostGameScreenControl
    {

        [Header("Buttons")]
        [SerializeField, Tooltip("Button used to return to the main menu.")]
        private Button backToMenuButton;
        
        [SerializeField, Tooltip("Button used to play again.")]
        private Button resetGameButton;

        [Header("Texts")] 
        [SerializeField, Tooltip("Text responsible of showing the total game time.")]
        private TextMeshProUGUI postGameTimeTextComponent;

        private void Awake()
        {
            backToMenuButton.onClick.AddListener(BackToMenuButtonPressed);
            resetGameButton.onClick.AddListener(ResetGameButtonPressed);
        }

        private void OnDestroy()
        {
            backToMenuButton.onClick.RemoveListener(BackToMenuButtonPressed);
            resetGameButton.onClick.RemoveListener(ResetGameButtonPressed);
        }

        private void BackToMenuButtonPressed() => OnPostGameBackButtonPressed?.Invoke();
        private void ResetGameButtonPressed() => OnPostGameResetGameButtonPressed?.Invoke();

        public override void SetPostScreenData(object postGameObjectData)
        {
            BottleGamePostGameData postGameData = (BottleGamePostGameData)postGameObjectData;
            postGameTimeTextComponent.text = $"Time: {postGameData.TimeToComplete:0.##}";
        }
    }
}