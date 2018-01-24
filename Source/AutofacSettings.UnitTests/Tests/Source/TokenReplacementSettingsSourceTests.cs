// <copyright file="TokenReplacementSettingsSourceTests.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.UnitTests.Tests.Source
{
    using System.Threading.Tasks;
    using AutofacSettings.UnitTests.Models;
    using Ploeh.AutoFixture.Xunit2;
    using Xunit;

    #pragma warning disable SA1600
    #pragma warning disable 1591

    public class TokenReplacementSettingsSourceTests
    {
        [Theory]
        [AutoData]
        public async Task GetNonExistingSettingReturnsNull(
            TestTokenReplacementSettingSource settingsSource,
            string setting)
        {
            var value = await settingsSource.GetSetting(setting);
            Assert.Null(value);
        }

        [Theory]
        [AutoData]
        public async Task GetSettingNoTokenReturnsUnmodified(
            TestTokenReplacementSettingSource settingsSource,
            string setting,
            string settingValue)
        {
            settingsSource.AddSetting(setting, settingValue);
            var value = await settingsSource.GetSetting(setting);
            Assert.Equal(settingValue, value);
        }

        [Theory]
        [AutoData]
        public async Task GetSettingWithSingleTokenReturnsUnmodified(
            TestTokenReplacementSettingSource settingsSource,
            string setting)
        {
            settingsSource.AddSetting(setting, "__bar");
            var value = await settingsSource.GetSetting(setting);
            Assert.Equal("__bar", value);
        }

        [Theory]
        [AutoData]
        public async Task GetSettingWithTokenForMissingSettingReturnsModified(
            TestTokenReplacementSettingSource settingsSource,
            string setting)
        {
            settingsSource.AddSetting(setting, "__bar__");
            var value = await settingsSource.GetSetting(setting);
            Assert.Equal(string.Empty, value);
        }

        [Theory]
        [AutoData]
        public async Task GetSettingWithTokenReturnsModified(
            TestTokenReplacementSettingSource settingsSource,
            string setting)
        {
            settingsSource.AddSetting("bar", "foo");
            settingsSource.AddSetting(setting, "__bar__");
            var value = await settingsSource.GetSetting(setting);
            Assert.Equal("foo", value);
        }

        [Theory]
        [AutoData]
        public async Task GetSettingWithTextAndTokenReturnsModified(
            TestTokenReplacementSettingSource settingsSource,
            string setting)
        {
            settingsSource.AddSetting("bar", "foo");
            settingsSource.AddSetting(setting, "baz__bar__");
            var value = await settingsSource.GetSetting(setting);
            Assert.Equal("bazfoo", value);
        }

        [Theory]
        [AutoData]
        public async Task GetSettingWithTextAndMultipleTokensReturnsModified(
            TestTokenReplacementSettingSource settingsSource,
            string setting)
        {
            settingsSource.AddSetting("bar", "foo");
            settingsSource.AddSetting("baz", "bar");
            settingsSource.AddSetting(setting, "baz__bar__foo__baz__");
            var value = await settingsSource.GetSetting(setting);
            Assert.Equal("bazfoofoobar", value);
        }
    }
}