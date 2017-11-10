using System.Collections.Specialized;
using AutofacSettings.Sources;

namespace AutofacSettings
{
    public class AppConfigSettingsService : DefaultSettingsService
    {
        public AppConfigSettingsService(
            NameValueCollection settings,
            string appKeyPrefix = "", 
            string settingsPostfix = "Settings", 
            ISettingConverter converter = null, 
            IInvalidSettingHandler handler = null) : base(new AppConfigSettingsSource(settings), appKeyPrefix, settingsPostfix, converter, handler)
        {
        }
    }
}