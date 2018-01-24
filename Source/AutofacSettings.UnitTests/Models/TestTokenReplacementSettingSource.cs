// <copyright file="TestTokenReplacementSettingSource.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.UnitTests.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutofacSettings.Sources;

    /// <summary>
    /// Test token replacement settings source.
    /// </summary>
    /// <seealso cref="AutofacSettings.ISettingsSource" />
    public class TestTokenReplacementSettingSource : ISettingsSource
    {
        /// <summary>
        /// The wrapped source
        /// </summary>
        private readonly InMemorySettingsSource wrappedSource;

        /// <summary>
        /// The source
        /// </summary>
        private readonly TokenReplacementSettingsSource source;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestTokenReplacementSettingSource"/> class.
        /// </summary>
        public TestTokenReplacementSettingSource()
        {
            this.wrappedSource = new InMemorySettingsSource();
            this.source = new TokenReplacementSettingsSource("__", this.wrappedSource);
        }

        /// <summary>
        /// Adds a setting.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void AddSetting(string key, string value)
        {
            this.wrappedSource.AddSetting(key, value);
        }

        /// <inheritdoc />
        public async Task<string> GetSetting(string settingName)
        {
            return await this.source.GetSetting(settingName);
        }

        /// <inheritdoc />
        public async Task<IDictionary<string, string>> GetSettings()
        {
            return await this.source.GetSettings();
        }
    }
}