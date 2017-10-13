using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;

namespace AutofacSettings.UnitTests.Models
{
    public class TestAppConfigSettingsService : AppConfigSettingsService
    {
        public TestAppConfigSettingsService()
            : base(TestSettings(), string.Empty, "Settings")
        {
        }

        private static NameValueCollection TestSettings()
        {
            return new NameValueCollection
            {
                { "Logging:Enabled", "true" },
                { "Logging:IncludeDetail", "true" }
            };
        }
    }
}