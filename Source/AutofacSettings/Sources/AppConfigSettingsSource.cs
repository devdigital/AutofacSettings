using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace AutofacSettings.Sources
{
    public class AppConfigSettingsSource : ISettingsSource
    {
        private readonly NameValueCollection settings;

        public AppConfigSettingsSource(NameValueCollection settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<string> GetSetting(string settingName)
        {
            var dictionary = await this.GetSettings();
            return dictionary.ContainsKey(settingName) 
                ? dictionary[settingName]
                : null;
        }

        public Task<IDictionary<string, string>> GetSettings()
        {
            var dictionary = settings.AllKeys.ToDictionary(
                k => k,
                k => settings[k]) as IDictionary<string, string>;

            return Task.FromResult(dictionary);
        }
    }
}