// <copyright file="AliasSettingsSource.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.Sources
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Alias settings source.
    /// </summary>
    /// <seealso cref="AutofacSettings.ISettingsSource" />
    public class AliasSettingsSource : ISettingsSource
    {
        private readonly Dictionary<string, IEnumerable<string>> aliases;

        private readonly ISettingsSource source;

        /// <summary>
        /// Initializes a new instance of the <see cref="AliasSettingsSource"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public AliasSettingsSource(ISettingsSource source)
        {
            this.aliases = new Dictionary<string, IEnumerable<string>>();
            this.source = source ?? throw new ArgumentNullException(nameof(source));
        }

        /// <summary>
        /// Registers the alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="keys">The keys.</param>
        public void RegisterAlias(string alias, IEnumerable<string> keys)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                throw new ArgumentNullException(nameof(alias));
            }

            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            if (this.aliases.ContainsKey(alias))
            {
                throw new InvalidOperationException($"Alias '{alias}' already registered.");
            }

            this.aliases.Add(alias, keys);
        }

        /// <inheritdoc />
        public async Task<string> GetSetting(string settingName)
        {
            if (string.IsNullOrWhiteSpace(settingName))
            {
                throw new ArgumentNullException(nameof(settingName));
            }

            if (!this.aliases.ContainsKey(settingName))
            {
                return await this.source.GetSetting(settingName);
            }

            var keys = this.aliases[settingName];
            foreach (var key in keys)
            {
                var settingValue = await this.source.GetSetting(key);
                if (settingValue != null)
                {
                    return settingValue;
                }
            }

            return null;
        }

        /// <inheritdoc />
        public async Task<IDictionary<string, string>> GetSettings()
        {
            return await this.source.GetSettings();
        }
    }
}