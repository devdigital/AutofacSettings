using AutofacSettings.Sources;
using Microsoft.Extensions.Configuration;

namespace AutofacSettings
{
    public class AspNetCoreSettingsService : DefaultSettingsService
    {
        public AspNetCoreSettingsService(
            IConfiguration configuration,
            string appKeyPrefix = "",
            string settingsPostfix = "Settings",
            ISettingConverter converter = null, 
            IInvalidSettingHandler handler = null) : base(new AspNetCoreSettingsSource(configuration), appKeyPrefix, settingsPostfix, converter, handler)
        {
        }
    }
}