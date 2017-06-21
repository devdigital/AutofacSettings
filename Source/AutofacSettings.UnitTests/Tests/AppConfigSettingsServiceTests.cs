namespace AutofacSettings.UnitTests.Tests
{
    using System.Threading.Tasks;

    using AutofacSettings.UnitTests.Models;

    using FluentAssertions;

    using Ploeh.AutoFixture.Xunit2;

    using Xunit;

    public class AppConfigSettingsServiceTests
    {
        [Theory]
        [AutoData]
        public async Task GetSettingValueExistingShouldReturnValue(
            AppConfigSettingsService appConfigSettingsService)
        {
            var settingValue = await appConfigSettingsService.GetSettingValue("Logging:Enabled", false);
            Assert.True(settingValue);
        }

        [Theory]
        [AutoData]
        public async Task GetSettingValueNotExistingShouldReturnDefault(
            AppConfigSettingsService appConfigSettingsService,
            string defaultValue)
        {
            var settingValue = await appConfigSettingsService.GetSettingValue("UnexistingSetting", defaultValue);
            Assert.Equal(defaultValue, settingValue);
        }

        [Theory]
        [AutoData]
        public async Task GetSettingsExistingShouldReturnPopulatedType(
            AppConfigSettingsService appConfigSettingsService)
        {
            var expectedLoggingSettings = new LoggingSettings { Enabled = true, IncludeDetail = true };
            var loggingSettings = await appConfigSettingsService.GetSettings<LoggingSettings>();
            expectedLoggingSettings.ShouldBeEquivalentTo(loggingSettings);
        }

        [Theory]
        [AutoData]
        public async Task GetSettingsExistingAsObjectShouldReturnObject(
            AppConfigSettingsService appConfigSettingsService)
        {
            var expectedLoggingSettings = new { Enabled = true, IncludeDetail = true };
            var loggingSettings = await appConfigSettingsService.GetSettings(typeof(LoggingSettings));
            expectedLoggingSettings.ShouldBeEquivalentTo(loggingSettings);
        }
    }
}
