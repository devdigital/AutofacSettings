namespace AutofacSettings
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;

    public class AppConfigSettingsService : ISettingsService
    {
        private readonly NameValueCollection settings;

        private readonly string appKeyPrefix;

        private readonly string settingsPostfix;

        public AppConfigSettingsService(string appKeyPrefix = "", string settingsPostfix = "Settings")
        {
            this.settings = ConfigurationManager.AppSettings;
            this.appKeyPrefix = appKeyPrefix;
            this.settingsPostfix = settingsPostfix;            
        }

        public Task<TValue> GetSettingValue<TValue>(string settingName, TValue defaultValue)
        {
            if (string.IsNullOrWhiteSpace(settingName))
            {
                throw new ArgumentNullException(nameof(settingName));
            }

            var value = ConfigurationManager.AppSettings[settingName];
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

            foreach (var key in this.settings.AllKeys.Where(k => k.StartsWith(settingsPrefix)))
            {
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
