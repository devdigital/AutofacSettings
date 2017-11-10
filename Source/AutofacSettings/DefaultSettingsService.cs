using System;
using System.Reflection;
using System.Threading.Tasks;
using AutofacSettings.Converters;
using AutofacSettings.Exceptions;
using AutofacSettings.Handlers;

namespace AutofacSettings
{
    public class DefaultSettingsService : ISettingsService
    {
        private readonly string appKeyPrefix;

        private readonly string settingsPostfix;

        private readonly ISettingsSource source;

        private readonly ISettingConverter converter;

        private readonly IInvalidSettingHandler handler;

        public DefaultSettingsService(
            ISettingsSource source,
            string appKeyPrefix = "",
            string settingsPostfix = "Settings",            
            ISettingConverter converter = null,
            IInvalidSettingHandler handler = null)
        {
            if (string.IsNullOrWhiteSpace(settingsPostfix))
            {
                throw new ArgumentNullException(nameof(settingsPostfix));
            }

            this.appKeyPrefix = appKeyPrefix;
            this.settingsPostfix = settingsPostfix;
            this.source = source ?? throw new ArgumentNullException(nameof(source));
            this.converter = converter ?? new DefaultSettingConverter();
            this.handler = handler ?? new ThrowOnInvalidSettingHandler();
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

            var settingTypeProperties = type.GetProperties(
                BindingFlags.Instance | BindingFlags.Public);

            foreach (var property in settingTypeProperties)
            {
                var settingName = $"{settingsPrefix}{property.Name}";

                var settingValue = await this.source.GetSetting(settingName);
                if (string.IsNullOrWhiteSpace(settingValue))
                {
                    this.handler.HandleMissingSetting(type, settingName);
                    continue;
                }

                object propertyValue;
                try
                {
                    propertyValue = this.converter.Convert(
                        settingValue,
                        property.PropertyType);
                }
                catch (Exception exception)
                {
                    var message =
                        $"There was an exception converting setting '{settingName}' value of '{settingValue}' to type {property.PropertyType}.";

                    throw new AutofacSettingsConversionException(message, exception);
                } 

                property.SetValue(instance, propertyValue, null);
            }
          
            return instance;
        }
    }
}