using System.Threading.Tasks;
using AutofacSettings.UnitTests.Models;
using AutofacSettings.UnitTests.Services;
using FluentAssertions;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace AutofacSettings.UnitTests.Tests
{
    public class AspNetCoreSettingsServiceTests
    {
        [Theory]
        [AutoData]
        public async Task GetSettingValueExistingShouldReturnValue(
            TestAspNetCoreSettingsSource source,
            SettingsServiceBuilder serviceBuilder)
        {
            var service = serviceBuilder.WithSource(source).Build();
            var settingValue = await service.GetSettingValue("Logging:Enabled", false);
            Assert.True(settingValue);
        }

        [Theory]
        [AutoData]
        public async Task GetSettingValueNotExistingShouldReturnDefault(
            TestAspNetCoreSettingsSource source,
            SettingsServiceBuilder serviceBuilder,
            string defaultValue)
        {
            var service = serviceBuilder.WithSource(source).Build();
            var settingValue = await service.GetSettingValue("UnexistingSetting", defaultValue);
            Assert.Equal(defaultValue, settingValue);
        }

        [Theory]
        [AutoData]
        public async Task GetSettingsExistingShouldReturnPopulatedType(
            TestAspNetCoreSettingsSource source,
            SettingsServiceBuilder serviceBuilder)
        {
            var service = serviceBuilder.WithSource(source).Build();
            var expectedLoggingSettings = new LoggingSettings { Enabled = true, IncludeDetail = true };
            var loggingSettings = await service.GetSettings<LoggingSettings>();
            loggingSettings.ShouldBeEquivalentTo(expectedLoggingSettings);
        }

        [Theory]
        [AutoData]
        public async Task GetSettingsExistingAsObjectShouldReturnObject(
            TestAspNetCoreSettingsSource source,
            SettingsServiceBuilder serviceBuilder)
        {
            var service = serviceBuilder.WithSource(source).Build();
            var expectedLoggingSettings = new { Enabled = true, IncludeDetail = true };
            var loggingSettings = await service.GetSettings(typeof(LoggingSettings));
            loggingSettings.ShouldBeEquivalentTo(expectedLoggingSettings);
        }
    }
}
