using System.Collections.Specialized;
using AutofacSettings.Sources;

namespace AutofacSettings.UnitTests.Models
{
    public class NoSettingsSource : AppConfigSettingsSource
    {
        public NoSettingsSource() : base(new NameValueCollection())
        {
        }
    }
}