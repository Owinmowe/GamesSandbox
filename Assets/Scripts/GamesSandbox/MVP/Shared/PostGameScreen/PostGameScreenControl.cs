using System;
using UnityEngine;

namespace GamesSandbox.MVP.Shared.PostGameScreen
{
    public abstract class PostGameScreenControl : MonoBehaviour, IScreenControl
    {
        /// <summary>Event called from PostGameScreenControl when back button is pressed. </summary>
        public Action OnPostGameBackButtonPressed;
        
        /// <summary>Event called from PostGameScreenControl when reset game button is pressed. </summary>
        public Action OnPostGameResetGameButtonPressed;
        
        /// <summary>Method for enabling the PostGameScreen gameObject.</summary>
        public void OpenScreen()
        {
            gameObject.SetActive(true);
        }

        /// <summary>Method for disabling the PostGameScreen gameObject.</summary>
        public void CloseScreen()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Method for setting the PostGameScreen data.
        /// <param name ="postGameObjectData">The data used for the PostGame.
        /// This data must be cast to its corresponding subclass of PostScreenData to be usable inside of a
        /// PostGameScreenControl subclass.</param>
        /// </summary>
        public abstract void SetPostScreenData(object postGameObjectData);
    }
}