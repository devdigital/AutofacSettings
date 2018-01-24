// <copyright file="InMemorySettingsSource.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.Sources
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// In memory settings source.
    /// </summary>
    /// <seealso cref="AutofacSettings.ISettingsSource" />
    public class InMemorySettingsSource : ISettingsSource
    {
        private readonly IDictionary<string, string> settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemorySettingsSource"/> class.
        /// </summary>
        public InMemorySettingsSource()
        {
            this.settings = new Dictionary<string, string>();
        }

        /// <summary>
        /// Adds a setting.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void AddSetting(string key, string value)
        {
            if (this.settings.ContainsKey(key))
            {
                throw new ArgumentException($"Key value '{key}' already added.");
            }

            this.settings.Add(key, value);
        }

        /// <inheritdoc />
        public Task<string> GetSetting(string settingName)
        {
            return this.settings.ContainsKey(settingName)
                ? Task.FromResult(this.settings[settingName])
                : Task.FromResult<string>(null);
        }

        /// <inheritdoc />
        public Task<IDictionary<string, string>> GetSettings()
        {
            return Task.FromResult(this.settings);
        }
    }
}