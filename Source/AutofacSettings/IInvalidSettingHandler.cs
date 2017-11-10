using System;

namespace AutofacSettings
{
    public interface IInvalidSettingHandler
    {
        void HandleMissingSetting(Type settingsType, string settingName);
    }
}