// <copyright file="ISettingsService.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Settings service.
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Gets the setting value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The setting value.</returns>
        Task<TValue> GetSettingValue<TValue>(string settingName, TValue defaultValue);

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <typeparam name="TSettings">The type of the settings.</typeparam>
        /// <returns>The settings.</returns>
        Task<TSettings> GetSettings<TSettings>()
            where TSettings : class;

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The settings.</returns>
        Task<object> GetSettings(Type type);
    }
}