// <copyright file="NullInvalidSettingHandler.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.Handlers
{
    using System;

    /// <summary>
    /// Null invalid setting handler
    /// </summary>
    /// <seealso cref="AutofacSettings.IInvalidSettingHandler" />
    public class NullInvalidSettingHandler : IInvalidSettingHandler
    {
        /// <inheritdoc />
        public void HandleMissingSetting(Type settingsType, string settingName)
        {
        }
    }
}