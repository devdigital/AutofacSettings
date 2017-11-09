using System.Threading.Tasks;
using AutofacSettings.UnitTests.Models;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace AutofacSettings.UnitTests.Tests.Source
{
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