// <copyright file="ISettingConverter.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings
{
    using System;

    /// <summary>
    /// Setting converter.
    /// </summary>
    public interface ISettingConverter
    {
        /// <summary>
        /// Converts the specified setting value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="settingValue">The setting value.</param>
        /// <returns>The converted setting value.</returns>
        TValue Convert<TValue>(string settingValue);

        /// <summary>
        /// Converts the specified setting value.
        /// </summary>
        /// <param name="settingValue">The setting value.</param>
        /// <param name="type">The type.</param>
        /// <returns>The converted setting value.</returns>
        object Convert(string settingValue, Type type);
    }
}