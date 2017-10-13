using System.Collections.Generic;

namespace AutofacSettings
{
    using System.Collections.Specialized;
    using System.Linq;

    public abstract class AppConfigSettingsService : KeyValueSettingsService
    {
        protected AppConfigSettingsService(
            NameValueCollection settings, 
            string appKeyPrefix = "", 
            string settingsPostfix = "Settings") : base(ToDictionary(settings), appKeyPrefix, settingsPostfix)
        {
        }

        private static IDictionary<string, string> ToDictionary(NameValueCollection settings)
        {
            return settings.AllKeys.ToDictionary(
                k => k,
                k => settings[k]);
        }
    }
}
