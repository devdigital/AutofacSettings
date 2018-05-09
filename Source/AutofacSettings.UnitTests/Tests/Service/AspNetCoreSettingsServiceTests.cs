// <copyright file="AspNetCoreSettingsServiceTests.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.UnitTests.Tests.Service
{
    using System.Threading.Tasks;
    using AutofacSettings.Exceptions;
    using AutofacSettings.UnitTests.Models;
    using AutofacSettings.UnitTests.Services;
    using AutoFixture.Xunit2;
    using FluentAssertions;
    using Xunit;

    #pragma warning disable SA1600
    #pragma warning disable 1591

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
            loggingSettings.Should().BeEquivalentTo(expectedLoggingSettings);
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
            loggingSettings.Should().BeEquivalentTo(expectedLoggingSettings);
        }

        [Theory]
        [AutoData]
        public async Task GetSettingsInvalidTypeShouldThrow(
            TestAspNetCoreSettingsSource source,
            SettingsServiceBuilder serviceBuilder)
        {
            var service = serviceBuilder.WithSource(source).Build();
            await Assert.ThrowsAsync<AutofacSettingsConversionException>(
                () => service.GetSettings<InvalidSettings>());
        }

        [Theory]
        [AutoData]
        public async Task GetSettingEmptyStringShouldReturnEmptyString(
            TestAspNetCoreSettingsSource source,
            SettingsServiceBuilder serviceBuilder,
            string defaultValue)
        {
            var service = serviceBuilder.WithSource(source).Build();
            var setting = await service.GetSettingValue("Api:Path", defaultValue);
            Assert.Equal(string.Empty, setting);
        }

        [Theory]
        [AutoData]
        public async Task GetSettingsEmptyStringShouldReturnEmptyString(
            TestAspNetCoreSettingsSource source,
            SettingsServiceBuilder serviceBuilder,
            string defaultValue)
        {
            var service = serviceBuilder.WithSource(source).Build();
            var settings = await service.GetSettings<ApiSettings>();
            Assert.Equal(string.Empty, settings.Path);
        }

        [Theory]
        [AutoData]
        public async Task GetSettingNullShouldReturnDefaultSetting(
            TestAspNetCoreSettingsSource source,
            SettingsServiceBuilder serviceBuilder,
            string defaultValue)
        {
            var service = serviceBuilder.WithSource(source).Build();
            var setting = await service.GetSettingValue("Api:Foo", defaultValue);
            Assert.Equal(defaultValue, setting);
        }
    }
}
