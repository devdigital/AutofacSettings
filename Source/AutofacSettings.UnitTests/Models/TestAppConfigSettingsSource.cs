using System.Collections.Specialized;
using AutofacSettings.Sources;

namespace AutofacSettings.UnitTests.Models
{
    public class TestAppConfigSettingsSource : AppConfigSettingsSource
    {
        public TestAppConfigSettingsSource() : base(Settings())
        {
        }

        private static NameValueCollection Settings()
        {
            return new NameValueCollection
            {
                { "Logging:Enabled", "true" },
                { "Logging:IncludeDetail", "true" }
            };
        }
    }
}