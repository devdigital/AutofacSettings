using System;

namespace AutofacSettings.Handlers
{
    public class NullMissingSettingHandler : IMissingSettingHandler
    {
        public void HandleMissingProperty(Type settingsType, string propertyName)
        {            
        }
    }
}