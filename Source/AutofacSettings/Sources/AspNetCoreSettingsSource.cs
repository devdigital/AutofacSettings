using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AutofacSettings.Sources
{
    public class AspNetCoreSettingsSource : ISettingsSource
    {
        private readonly IConfiguration configuration;

        public AspNetCoreSettingsSource(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Task<string> GetSetting(string settingName)
        {
            if (string.IsNullOrWhiteSpace(settingName))
            {
                throw new ArgumentNullException(nameof(settingName));
            }

            return Task.FromResult(this.configuration[settingName]);
        }

        public Task<IDictionary<string, string>> GetSettings()
        {
            var dictionary = this.configuration.AsEnumerable().ToDictionary(
                c => c.Key,
                c => c.Value) as IDictionary<string, string>;

            return Task.FromResult(dictionary);
        }
    }
}