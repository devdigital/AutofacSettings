using System;

namespace AutofacSettings.Handlers
{
    public class ThrowOnMissingSettingHandler : IMissingSettingHandler
    {
        public void HandleMissingProperty(Type settingsType, string propertyName)
        {
            throw new InvalidOperationException(
                $"Settings class '{settingsType.Name}' has no '{propertyName}' property.");
        }
    }
}