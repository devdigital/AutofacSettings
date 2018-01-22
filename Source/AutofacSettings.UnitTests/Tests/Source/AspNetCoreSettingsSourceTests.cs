// <copyright file="AspNetCoreSettingsSourceTests.cs" company="DevDigital">
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

    public class AspNetCoreSettingsSourceTests
    {
        [Theory]
        [AutoData]
        public async Task GetSettingValueNotExistingShouldReturnNull(
            TestAspNetCoreSettingsSource source)
        {
            var settingValue = await source.GetSetting("Foo");
            Assert.Null(settingValue);
        }
    }
}