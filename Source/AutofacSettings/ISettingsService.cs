namespace AutofacSettings
{
    using System;
    using System.Threading.Tasks;

    public interface ISettingsService
    {
        Task<TValue> GetSettingValue<TValue>(string settingName, TValue defaultValue);

        Task<TSettings> GetSettings<TSettings>() where TSettings : class;

        Task<object> GetSettings(Type type);
    }
}