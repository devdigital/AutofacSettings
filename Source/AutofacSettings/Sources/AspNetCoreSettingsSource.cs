// <copyright file="AspNetCoreSettingsSource.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.Sources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// ASP.NET Core settings source.
    /// </summary>
    /// <seealso cref="AutofacSettings.ISettingsSource" />
    public class AspNetCoreSettingsSource : ISettingsSource
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AspNetCoreSettingsSource"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public AspNetCoreSettingsSource(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <inheritdoc />
        public Task<string> GetSetting(string settingName)
        {
            if (string.IsNullOrWhiteSpace(settingName))
            {
                throw new ArgumentNullException(nameof(settingName));
            }

            return Task.FromResult(this.configuration[settingName]);
        }

        /// <inheritdoc />
        public Task<IDictionary<string, string>> GetSettings()
        {
            var dictionary = this.configuration.AsEnumerable().ToDictionary(
                c => c.Key,
                c => c.Value) as IDictionary<string, string>;

            return Task.FromResult(dictionary);
        }
    }
}