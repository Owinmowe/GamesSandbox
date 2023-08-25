namespace GamesSandbox.MVP
{
    /// <summary>
    /// Base interface for controls in MVP design. This class is an extension of the View class
    /// made to simplify the logic inside of it and separate each screen logic inside its own
    /// script.
    /// </summary>
    public interface IScreenControl
    {
        /// <summary>Method for calling the opening of a screen of a IScreenControl implementation.</summary>
        void OpenScreen();
        
        /// <summary>Method for calling the opening of a screen of a IScreenControl implementation.</summary>
        void CloseScreen();
    }
}
