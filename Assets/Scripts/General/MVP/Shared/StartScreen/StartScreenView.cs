using UnityEngine;
using System;
using General.MVP.Shared.StartScreen;

//Forced namespace as a limitation of partial classes in C# since all parts MUST be in same namespace
namespace General.MVP
{
    public abstract partial class View
    {
        /// <summary>Variable shared from StartScreenView for the corresponding control.</summary>
        [SerializeField, Tooltip("StartScreenControl reference in scene/prefab.")] 
        protected StartScreenControl startScreenControl;
        
        /// <summary>Event called from any View when StartScreenControl calls the same event.</summary>
        public Action OnExitButtonEvent;
        
        /// <summary>Event called from any View when StartScreenControl calls the same event.</summary>
        public Action OnStartButtonEvent;
        
        /// <summary>Event called from any View when StartScreenControl calls the same event.</summary>
        public Action OnSettingsButtonEvent;
        
        /// <summary>Method for opening the Start Screen from a Presenter.</summary>
        public void OpenStartScreen() => startScreenControl.OpenScreen();
        
        /// <summary>Method for closing the Start Screen from a Presenter.</summary>
        public void CloseStartScreen() => startScreenControl.CloseScreen();
    }
}