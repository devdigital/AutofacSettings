using System.IO;
using Microsoft.Extensions.Configuration;

namespace AutofacSettings.UnitTests.Models
{
    public class TestAspNetCoreSettingsService : AspNetCoreSettingsService
    {
        public TestAspNetCoreSettingsService()
            : base(TestSettings(), string.Empty, "Settings")
        {
        }

        private static IConfiguration TestSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }
}
