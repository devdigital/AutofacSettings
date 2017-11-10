using System;
using AutofacSettings.Converters;
using AutofacSettings.Handlers;

namespace AutofacSettings.UnitTests.Services
{
    public class SettingsServiceBuilder
    {
        private string appKeyPrefix;

        private string settingsPostfix;

        private ISettingsSource source;

        private ISettingConverter converter;

        private IInvalidSettingHandler handler;

        public SettingsServiceBuilder()
        {
            this.appKeyPrefix = string.Empty;
            this.settingsPostfix = "Settings";
            this.converter = new DefaultSettingConverter();
            this.handler = new ThrowOnInvalidSettingHandler();
        }

        public SettingsServiceBuilder WithSource(ISettingsSource source)
        {
            this.source = source ?? throw new ArgumentNullException(nameof(source));
            return this;
        }

        public ISettingsService Build()
        {
            return new DefaultSettingsService(
                this.source,
                this.appKeyPrefix,
                this.settingsPostfix,
                this.converter, 
                this.handler);
        }
    }
}