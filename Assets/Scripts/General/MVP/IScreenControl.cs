namespace General.MVP
{
    /// <summary>
    /// Base interface for controls in MVP design. This class is an extension of the View
    /// made to simplify the logic inside of it and separate each screen logic inside its own
    /// script.
    /// </summary>
    public interface IScreenControl
    {
        void OpenScreen();
        void CloseScreen();
    }
}
