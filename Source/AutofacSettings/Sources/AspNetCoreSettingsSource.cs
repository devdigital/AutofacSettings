using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AutofacSettings.Sources
{
    public class AspNetCoreSettingsSource : ISettingsSource
    {
        private readonly IConfiguration settings;

        public AspNetCoreSettingsSource(IConfiguration settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public Task<string> GetSetting(string settingName)
        {
            if (string.IsNullOrWhiteSpace(settingName))
            {
                throw new ArgumentNullException(nameof(settingName));
            }

            return Task.FromResult(this.settings[settingName]);
        }

        public Task<IDictionary<string, string>> GetSettings()
        {
            var dictionary = this.settings.AsEnumerable().ToDictionary(
                c => c.Key,
                c => c.Value) as IDictionary<string, string>;

            return Task.FromResult(dictionary);
        }
    }
}