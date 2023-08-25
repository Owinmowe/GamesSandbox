using System;
using UnityEngine;
using GamesSandbox.MVP.Shared.StartScreen;

//Forced namespace as a limitation of partial classes in C# since all parts MUST be in same namespace
namespace GamesSandbox.MVP
{
    public abstract partial class View
    {
        
        /// <summary>Variable shared from StartScreenView for the corresponding control.</summary>
        [Header("Start Screen View")]
        [SerializeField, Tooltip("StartScreenControl reference in scene/prefab.")] 
        protected StartScreenControl startScreenControl;
        
        /// <summary>Event called from any View when StartScreenControl calls the same event.</summary>
        public Action StartScreenOnExitButtonEvent;
        
        /// <summary>Event called from any View when StartScreenControl calls the same event.</summary>
        public Action StartScreenOnPlayButtonEvent;
        
        /// <summary>Event called from any View when StartScreenControl calls the same event.</summary>
        public Action StartScreenOnSettingsButtonEvent;
        
        /// <summary>Method for opening the Start Screen from a Presenter.</summary>
        public void OpenStartScreen() => startScreenControl.OpenScreen();
        
        /// <summary>Method for closing the Start Screen from a Presenter.</summary>
        public void CloseStartScreen() => startScreenControl.CloseScreen();
    }
}