// <copyright file="ISettingsSource.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Settings source.
    /// </summary>
    public interface ISettingsSource
    {
        /// <summary>
        /// Gets the setting.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns>The setting value.</returns>
        Task<string> GetSetting(string settingName);

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <returns>The settings.</returns>
        Task<IDictionary<string, string>> GetSettings();
    }
}