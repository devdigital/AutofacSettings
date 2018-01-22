// <copyright file="AppConfigSettingsService.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings
{
    using System.Collections.Specialized;
    using AutofacSettings.Sources;

    /// <summary>
    /// App Config settings service.
    /// </summary>
    /// <seealso cref="AutofacSettings.DefaultSettingsService" />
    public class AppConfigSettingsService : DefaultSettingsService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppConfigSettingsService"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="appKeyPrefix">The application key prefix.</param>
        /// <param name="settingsPostfix">The settings postfix.</param>
        /// <param name="converter">The converter.</param>
        /// <param name="handler">The handler.</param>
        public AppConfigSettingsService(
            NameValueCollection settings,
            string appKeyPrefix = "",
            string settingsPostfix = "Settings",
            ISettingConverter converter = null,
            IInvalidSettingHandler handler = null)
            : base(new AppConfigSettingsSource(settings), appKeyPrefix, settingsPostfix, converter, handler)
        {
        }
    }
}