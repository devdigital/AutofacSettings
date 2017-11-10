using System;

namespace AutofacSettings.Handlers
{
    public class NullInvalidSettingHandler : IInvalidSettingHandler
    {
        public void HandleMissingSetting(Type settingsType, string settingName)
        {            
        }
    }
}