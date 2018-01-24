// <copyright file="TokenReplacementSettingsSource.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.Sources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Token replacement settings source.
    /// </summary>
    /// <seealso cref="AutofacSettings.ISettingsSource" />
    public class TokenReplacementSettingsSource : ISettingsSource
    {
        /// <summary>
        /// The token
        /// </summary>
        private readonly string token;

        /// <summary>
        /// The source
        /// </summary>
        private readonly ISettingsSource source;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenReplacementSettingsSource" /> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="source">The source.</param>
        /// <exception cref="ArgumentNullException">source</exception>
        public TokenReplacementSettingsSource(string token, ISettingsSource source)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            this.token = token;
            this.source = source ?? throw new ArgumentNullException(nameof(source));
        }

        /// <inheritdoc />
        public async Task<string> GetSetting(string settingName)
        {
            var settingValue = await this.source.GetSetting(settingName);
            return await this.ReplaceToken(settingValue);
        }

        /// <inheritdoc />
        public async Task<IDictionary<string, string>> GetSettings()
        {
            var settings = await this.source.GetSettings();
            var tasks = settings.Select(async kvp => new
            {
                kvp.Key,
                Value = await this.ReplaceToken(kvp.Value),
            });

            var tokenReplacedSettings = await Task.WhenAll(tasks);

            return tokenReplacedSettings
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        private async Task<string> ReplaceToken(string value)
        {
            if (value == null)
            {
                return null;
            }

            var pattern = $"{this.token}(.*?){this.token}";
            var matches = Regex.Matches(value, pattern);
            foreach (Match match in matches)
            {
                var settingName = match.Groups[1].Value;
                var settingValue = await this.source.GetSetting(settingName);
                value = value.Replace(match.Value, settingValue);
            }

            return value;
        }
    }
}