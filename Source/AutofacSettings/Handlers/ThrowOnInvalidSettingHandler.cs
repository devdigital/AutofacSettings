using System;

namespace AutofacSettings.Handlers
{
    public class ThrowOnInvalidSettingHandler : IInvalidSettingHandler
    {
        public void HandleMissingSetting(Type settingsType, string settingName)
        {
            throw new InvalidOperationException(
                $"Could not find setting '{settingName}' corresponding to the equivalent property on settings type '{settingsType.Name}'.");
        }
    }
}