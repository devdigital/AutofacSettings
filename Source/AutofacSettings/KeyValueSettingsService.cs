using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutofacSettings
{
    public abstract class KeyValueSettingsService : ISettingsService
    {
        private readonly IDictionary<string, string> settings;

        private readonly string appKeyPrefix;

        private readonly string settingsPostfix;

        protected KeyValueSettingsService(
            IDictionary<string, string> settings, 
            string appKeyPrefix, 
            string settingsPostfix)
        {
            if (string.IsNullOrWhiteSpace(settingsPostfix))
            {
                throw new ArgumentNullException(nameof(settingsPostfix));
            }

            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.appKeyPrefix = appKeyPrefix;
            this.settingsPostfix = settingsPostfix;            
        }

        public Task<TValue> GetSettingValue<TValue>(string settingName, TValue defaultValue)
        {
            if (string.IsNullOrWhiteSpace(settingName))
            {
                throw new ArgumentNullException(nameof(settingName));
            }

            if (!this.settings.ContainsKey(settingName))
            {
                return Task.FromResult(defaultValue);
            }

            var value = this.settings[settingName];
            if (string.IsNullOrWhiteSpace(value))
            {
                return Task.FromResult(defaultValue);
            }

            try
            {
                return Task.FromResult((TValue)Convert.ChangeType(value, typeof(TValue)));
            }
            catch
            {
                return Task.FromResult(defaultValue);
            }
        }

        public async Task<TSetting> GetSettings<TSetting>() where TSetting : class
        {
            return await this.GetSettings(typeof(TSetting)) as TSetting;
        }

        public Task<object> GetSettings(Type type)
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

            foreach (var kvp in this.settings.Where(kvp => kvp.Key.StartsWith(settingsPrefix)))
            {
                var key = kvp.Key;
                var propertyName = key.Replace(settingsPrefix, string.Empty);
                var property = type.GetProperty(propertyName);
                if (property == null)
                {
                    throw new InvalidOperationException(
                        $"Settings class '{type.Name}' has no '{propertyName}' property");
                }

                var propertyValue = property.PropertyType.IsEnum
                    ? Enum.Parse(property.PropertyType, this.settings[key])
                    : Convert.ChangeType(this.settings[key], property.PropertyType);

                property.SetValue(instance, propertyValue, null);
            }

            return Task.FromResult(instance);
        }
    }
}