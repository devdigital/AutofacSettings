using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutofacSettings.Sources;
using AutofacSettings.UnitTests.Models;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace AutofacSettings.UnitTests.Tests.Source
{
    public class AliasSettingsSourceTests
    {
        [Theory]
        [AutoData]
        public async Task NoAliasReturnsUnderlyingSettingValue(
            TestAppConfigSettingsSource wrappedSource)
        {
            var source = new AliasSettingsSource(wrappedSource);
            var settingValue = await source.GetSetting("Logging:Enabled");
            Assert.Equal("true", settingValue);
        }

        [Theory]
        [AutoData]
        public void DuplicateAliasThrowsException(
            TestAppConfigSettingsSource wrappedSource,
            string alias,
            List<string> keys)
        {
            var source = new AliasSettingsSource(wrappedSource);
            source.RegisterAlias(alias, keys);

            Assert.Throws<InvalidOperationException>(
                () => source.RegisterAlias(alias, keys));
        }

        [Theory]
        [AutoData]
        public async Task AliasReturnsOnlyKey(
            TestAppConfigSettingsSource wrappedSource,
            string alias,
            List<string> keys)
        {
            var source = new AliasSettingsSource(wrappedSource);
            source.RegisterAlias(alias, new List<string> { "Logging:Enabled" });

            var settingValue = await source.GetSetting(alias);
            Assert.Equal("true", settingValue);
        }

        [Theory]
        [AutoData]
        public async Task AliasReturnsSecondKeyValueWhenFirstKeyValueReturnsNull(
            TestAppConfigSettingsSource wrappedSource,
            string alias,
            List<string> keys)
        {
            var source = new AliasSettingsSource(wrappedSource);
            source.RegisterAlias(alias, new List<string> { "baz", "Logging:Enabled" });

            var settingValue = await source.GetSetting(alias);
            Assert.Equal("true", settingValue);
        }

        [Theory]
        [AutoData]
        public async Task AliasReturnsNullWhenAllKeysReturnNull(
            TestAppConfigSettingsSource wrappedSource,
            string alias,
            List<string> keys)
        {
            var source = new AliasSettingsSource(wrappedSource);
            source.RegisterAlias(alias, new List<string> { "baz", "bar" });

            var settingValue = await source.GetSetting(alias);
            Assert.Null(settingValue);
        }
    }
}