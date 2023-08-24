using System;
using UnityEngine;
using General.MVP.Shared.SettingsScreen;

//Forced namespace as a limitation of partial classes in C# since all parts MUST be in same namespace
namespace General.MVP
{
    public abstract partial class View
    {
        /// <summary>Variable shared from SettingsScreenView for the corresponding control.</summary>
        [SerializeField, Tooltip("SettingsScreenControl reference in scene/prefab.")] 
        protected SettingsScreenControl settingsScreenControl;
        
        /// <summary>Event called from any View when SettingsControl calls the same event.</summary>
        public Action SettingsScreenOnBackButtonEvent;
        
        /// <summary>Method for opening the Settings Screen from a Presenter.</summary>
        public void OpenSettingsScreen() => settingsScreenControl.OpenScreen();
        
        /// <summary>Method for closing the Settings Screen from a Presenter.</summary>
        public void CloseSettingScreen() => settingsScreenControl.CloseScreen();
    }
}