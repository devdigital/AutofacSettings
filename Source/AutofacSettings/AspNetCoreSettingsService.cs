using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace AutofacSettings
{
    public class AspNetCoreSettingsService : KeyValueSettingsService
    {
        public AspNetCoreSettingsService(
            IConfiguration settings, 
            string appKeyPrefix = "",
            string settingsPostfix = "Settings") : base(ToDictionary(settings), appKeyPrefix, settingsPostfix)
        {
        }

        private static IDictionary<string, string> ToDictionary(IConfiguration configurationRoot)
        {
            return configurationRoot.AsEnumerable().ToDictionary(
                c => c.Key,
                c => c.Value);
        }
    }
}