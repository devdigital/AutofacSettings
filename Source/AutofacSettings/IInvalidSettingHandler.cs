// <copyright file="IInvalidSettingHandler.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings
{
    using System;

    /// <summary>
    /// Invalid settings handler.
    /// </summary>
    public interface IInvalidSettingHandler
    {
        /// <summary>
        /// Handles the missing setting.
        /// </summary>
        /// <param name="settingsType">Type of the settings.</param>
        /// <param name="settingName">Name of the setting.</param>
        void HandleMissingSetting(Type settingsType, string settingName);
    }
}