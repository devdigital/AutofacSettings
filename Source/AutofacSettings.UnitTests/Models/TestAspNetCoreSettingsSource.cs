using System.IO;
using AutofacSettings.Sources;
using Microsoft.Extensions.Configuration;

namespace AutofacSettings.UnitTests.Models
{
    public class TestAspNetCoreSettingsSource : AspNetCoreSettingsSource
    {
        public TestAspNetCoreSettingsSource() : base(Settings())
        {
        }

        private static IConfiguration Settings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }
}
