namespace GamesSandbox.MVP.Shared.SettingsScreen.Options
{
    /// <summary>
    /// Data struct for Settings Change in the GamesSandbox.
    /// This struct value must be parsed to its corresponding type before being used.
    /// </summary>
    public struct SettingsChangeData
    {
        /// <summary>Key for identifying the option to change. </summary>
        public string SettingKey;
        
        /// <summary>Key enum for identifying the option new value. </summary>
        public object Value;
    }
}