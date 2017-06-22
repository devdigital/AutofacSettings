namespace AutofacSettings.UnitTests.Models
{
    public class TestAppConfigSettingsService : AppConfigSettingsService
    {
        public TestAppConfigSettingsService()
            : base(string.Empty, "Settings")
        {
        }
    }
}