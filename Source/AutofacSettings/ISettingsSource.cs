using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutofacSettings
{
    public interface ISettingsSource
    {
        Task<string> GetSetting(string settingName);

        Task<IDictionary<string, string>> GetSettings();
    }
}