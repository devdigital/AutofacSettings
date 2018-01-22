// <copyright file="AppConfigSettingsSource.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.Sources
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// App Config settings source.
    /// </summary>
    /// <seealso cref="AutofacSettings.ISettingsSource" />
    public class AppConfigSettingsSource : ISettingsSource
    {
        /// <summary>
        /// The settings
        /// </summary>
        private readonly NameValueCollection settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppConfigSettingsSource"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public AppConfigSettingsSource(NameValueCollection settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <inheritdoc />
        public async Task<string> GetSetting(string settingName)
        {
            if (string.IsNullOrWhiteSpace(settingName))
            {
                throw new ArgumentNullException(nameof(settingName));
            }

            var dictionary = await this.GetSettings();
            return dictionary.ContainsKey(settingName)
                ? dictionary[settingName]
                : null;
        }

        /// <inheritdoc />
        public Task<IDictionary<string, string>> GetSettings()
        {
            var dictionary = this.settings.AllKeys.ToDictionary(
                k => k,
                k => this.settings[k]) as IDictionary<string, string>;

            return Task.FromResult(dictionary);
        }
    }
}