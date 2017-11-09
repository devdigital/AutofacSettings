using System;

namespace AutofacSettings
{
    public interface IMissingSettingHandler
    {
        void HandleMissingProperty(Type settingsType, string propertyName);
    }
}