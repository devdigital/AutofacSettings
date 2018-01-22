// <copyright file="ThrowOnInvalidSettingHandler.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.Handlers
{
    using System;

    /// <summary>
    /// Throw on invalid setting handler.
    /// </summary>
    /// <seealso cref="AutofacSettings.IInvalidSettingHandler" />
    public class ThrowOnInvalidSettingHandler : IInvalidSettingHandler
    {
        /// <inheritdoc />
        public void HandleMissingSetting(Type settingsType, string settingName)
        {
            throw new InvalidOperationException(
                $"Could not find setting '{settingName}' corresponding to the equivalent property on settings type '{settingsType.Name}'.");
        }
    }
}