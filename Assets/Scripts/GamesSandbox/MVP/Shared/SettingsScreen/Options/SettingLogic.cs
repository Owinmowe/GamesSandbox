namespace GamesSandbox.MVP.Shared.SettingsScreen.Options
{
    /// <summary>
    /// Abstract class used as a base for options logic in Settings Screen Presenter.
    /// All subclasses of this class will be loaded by reflection and used accordingly.
    /// </summary>
    public abstract class SettingLogic
    {
        /// <summary>Key for identifying the setting change. </summary>
        public abstract string SettingKey { get; }

        /// <summary>
        /// Method for changing the value of the corresponding setting.
        /// <param name = "value">New value for the setting.</param>
        /// <returns>True if the value was changed successfully. False otherwise.</returns>
        /// </summary>
        public abstract bool SetSettingValue(object value);
        
        /// <summary>
        /// Method for getting the value of the corresponding setting.
        /// <returns>Value of the setting as object. Note that this value needs to be parsed to be usable.</returns>
        /// </summary>
        public abstract object GetSettingValue();

        /// <summary>
        /// Method for getting the default value of the corresponding setting.
        /// <returns>Default value of the setting as object. Note that this value needs to be parsed to be usable.</returns>
        /// </summary>
        public abstract object GetSettingDefaultValue();
    }
}