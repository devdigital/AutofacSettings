// <copyright file="DefaultSettingConverter.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.Converters
{
    using System;

    /// <summary>
    /// Default setting converter
    /// </summary>
    /// <seealso cref="AutofacSettings.ISettingConverter" />
    public class DefaultSettingConverter : ISettingConverter
    {
        /// <inheritdoc />
        public TValue Convert<TValue>(string settingValue)
        {
            return (TValue)this.Convert(settingValue, typeof(TValue));
        }

        /// <inheritdoc />
        public object Convert(string settingValue, Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (settingValue == null)
            {
                return null;
            }

            return type.IsEnum
                ? Enum.Parse(type, settingValue)
                : System.Convert.ChangeType(settingValue, type);
        }
    }
}