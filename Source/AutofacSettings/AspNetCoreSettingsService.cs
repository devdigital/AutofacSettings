// <copyright file="AspNetCoreSettingsService.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings
{
    using AutofacSettings.Sources;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// ASP.NET Core settings service.
    /// </summary>
    /// <seealso cref="AutofacSettings.DefaultSettingsService" />
    public class AspNetCoreSettingsService : DefaultSettingsService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AspNetCoreSettingsService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="appKeyPrefix">The application key prefix.</param>
        /// <param name="settingsPostfix">The settings postfix.</param>
        /// <param name="converter">The converter.</param>
        /// <param name="handler">The handler.</param>
        public AspNetCoreSettingsService(
            IConfiguration configuration,
            string appKeyPrefix = "",
            string settingsPostfix = "Settings",
            ISettingConverter converter = null,
            IInvalidSettingHandler handler = null)
            : base(new AspNetCoreSettingsSource(configuration), appKeyPrefix, settingsPostfix, converter, handler)
        {
        }
    }
}