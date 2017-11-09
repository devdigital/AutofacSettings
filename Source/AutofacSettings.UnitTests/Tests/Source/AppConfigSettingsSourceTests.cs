using System.Threading.Tasks;
using AutofacSettings.UnitTests.Models;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace AutofacSettings.UnitTests.Tests.Source
{
    public class AppConfigSettingsSourceTests
    {
        [Theory]
        [AutoData]
        public async Task GetSettingValueNotExistingShouldReturnNull(
            TestAppConfigSettingsSource source)
        {
            var settingValue = await source.GetSetting("Foo");
            Assert.Null(settingValue);
        }
    }
}