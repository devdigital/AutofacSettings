using System.IO;
using Autofac;
using AutofacSettings.UnitTests.Models;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace AutofacSettings.UnitTests.Tests
{
    public class AutofacSettingsSourceTests
    {
        [Theory]
        [AutoData]
        public void AutofacSettingsSourceReturnsExpectedSettings()
        {
            var settings = GetSettings();

            var builder = new ContainerBuilder();
            builder.RegisterSource(
                new AutofacSettingsSource(new AspNetCoreSettingsService(settings)));
            
            var container = builder.Build();
            var loggingSettings = container.Resolve<LoggingSettings>();
            var expectedLoggingSettings = new LoggingSettings
            {
                Enabled = true,
                IncludeDetail = true
            };

            loggingSettings.ShouldBeEquivalentTo(expectedLoggingSettings);
        }

        private static IConfiguration GetSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }
}
