using System;
using System.Linq;
using System.Threading.Tasks;
using AutofacSettings.Converters;
using AutofacSettings.Handlers;

namespace AutofacSettings
{
    public class DefaultSettingsService : ISettingsService
    {
        private readonly string appKeyPrefix;

        private readonly string settingsPostfix;

        private readonly ISettingsSource source;

        private readonly ISettingConverter converter;

        private readonly IMissingSettingHandler handler;

        public DefaultSettingsService(
            ISettingsSource source,
            string appKeyPrefix = "",
            string settingsPostfix = "Settings",            
            ISettingConverter converter = null,
            IMissingSettingHandler handler = null)
        {
            if (string.IsNullOrWhiteSpace(settingsPostfix))
            {
                throw new ArgumentNullException(nameof(settingsPostfix));
            }

            this.appKeyPrefix = appKeyPrefix;
            this.settingsPostfix = settingsPostfix;
            this.source = source ?? throw new ArgumentNullException(nameof(source));
            this.converter = converter ?? new DefaultSettingConverter();
            this.handler = handler ?? new ThrowOnMissingSettingHandler();
        }

        public async Task<TValue> GetSettingValue<TValue>(string settingName, TValue defaultValue)
        {
            if (string.IsNullOrWhiteSpace(settingName))
            {
                throw new ArgumentNullException(nameof(settingName));
            }

            var settingValue = await this.source.GetSetting(settingName);
            return string.IsNullOrWhiteSpace(settingValue) 
                ? defaultValue 
                : this.converter.Convert<TValue>(settingValue);
        }

        public async Task<TSettings> GetSettings<TSettings>() where TSettings : class
        {
            return await this.GetSettings(typeof(TSettings)) as TSettings;
        }

        public async Task<object> GetSettings(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var instance = Activator.CreateInstance(type);

            var prefix = string.IsNullOrWhiteSpace(this.appKeyPrefix)
                ? string.Empty
                : $"{this.appKeyPrefix}:";

            var settingsPrefix = $"{prefix}{type.Name.Replace(this.settingsPostfix, string.Empty)}:";

            var settings = await this.source.GetSettings();
            foreach (var kvp in settings.Where(kvp => kvp.Key.StartsWith(settingsPrefix)))
            {
                var key = kvp.Key;
                var propertyName = key.Replace(settingsPrefix, string.Empty);
                var property = type.GetProperty(propertyName);
                if (property == null)
                {
                    this.handler.HandleMissingProperty(type, propertyName);
                    continue;
                }

                var propertyValue = this.converter.Convert(
                    settings[key],
                    property.PropertyType);

                property.SetValue(instance, propertyValue, null);
            }

            return instance;
        }
    }
}