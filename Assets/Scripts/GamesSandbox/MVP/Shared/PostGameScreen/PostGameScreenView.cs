using System;
using GamesSandbox.MVP.Shared.PostGameScreen;
using UnityEngine;

//Forced namespace as a limitation of partial classes in C# since all parts MUST be in same namespace
namespace GamesSandbox.MVP
{
    public partial class View
    {
        /// <summary>Variable shared from PostGameScreenView for the corresponding control.</summary>
        [Header("PostGame Screen View")] 
        [SerializeField, Tooltip("PostGameScreenControl reference in scene.")]
        protected PostGameScreenControl postGameScreenControl;
        
        /// <summary>Event called from any View when PostGameScreenControl calls the same event.</summary>
        public Action PostGameScreenOnResetButtonEvent;
        
        /// <summary>Event called from any View when PostGameScreenControl calls the same event.</summary>
        public Action PostGameScreenOnBackToMenuButtonEvent;
        
        /// <summary>Method for opening the PostGame Screen from a Presenter.</summary>
        public void OpenPostGameScreen() => postGameScreenControl.OpenScreen();
        
        /// <summary>Method for closing the PostGame Screen from a Presenter.</summary>
        public void ClosePostGameScreen() => postGameScreenControl.CloseScreen();

        /// <summary>Method for setting the PostGameData in the PostGameControl from a Presenter.</summary>
        public void SetPostScreenData(object postGameObjectData) => postGameScreenControl.SetPostScreenData(postGameObjectData);
    }
}