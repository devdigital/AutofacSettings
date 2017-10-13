using System.Threading.Tasks;
using AutofacSettings.UnitTests.Models;
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
            TestAspNetCoreSettingsService appConfigSettingsService)
        {
            var settingValue = await appConfigSettingsService.GetSettingValue("Logging:Enabled", false);
            Assert.True(settingValue);
        }

        [Theory]
        [AutoData]
        public async Task GetSettingValueNotExistingShouldReturnDefault(
            TestAspNetCoreSettingsService appConfigSettingsService,
            string defaultValue)
        {
            var settingValue = await appConfigSettingsService.GetSettingValue("UnexistingSetting", defaultValue);
            Assert.Equal(defaultValue, settingValue);
        }

        [Theory]
        [AutoData]
        public async Task GetSettingsExistingShouldReturnPopulatedType(
            TestAspNetCoreSettingsService appConfigSettingsService)
        {
            var expectedLoggingSettings = new LoggingSettings { Enabled = true, IncludeDetail = true };
            var loggingSettings = await appConfigSettingsService.GetSettings<LoggingSettings>();
            loggingSettings.ShouldBeEquivalentTo(expectedLoggingSettings);
        }

        [Theory]
        [AutoData]
        public async Task GetSettingsExistingAsObjectShouldReturnObject(
            TestAspNetCoreSettingsService appConfigSettingsService)
        {
            var expectedLoggingSettings = new { Enabled = true, IncludeDetail = true };
            var loggingSettings = await appConfigSettingsService.GetSettings(typeof(LoggingSettings));
            loggingSettings.ShouldBeEquivalentTo(expectedLoggingSettings);
        }
    }
}
