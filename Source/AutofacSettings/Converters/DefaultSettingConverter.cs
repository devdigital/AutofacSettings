// <copyright file="DefaultSettingConverter.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.Converters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Default setting converter
    /// </summary>
    /// <seealso cref="AutofacSettings.ISettingConverter" />
    public class DefaultSettingConverter : ISettingConverter
    {
        /// <summary>
        /// The collection separator
        /// </summary>
        private readonly string collectionSeparator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSettingConverter"/> class.
        /// </summary>
        public DefaultSettingConverter()
        {
            this.collectionSeparator = "|";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSettingConverter"/> class.
        /// </summary>
        /// <param name="collectionSeparator">The collection separator.</param>
        public DefaultSettingConverter(string collectionSeparator)
        {
            if (string.IsNullOrWhiteSpace(collectionSeparator))
            {
                throw new ArgumentNullException(nameof(collectionSeparator));
            }

            this.collectionSeparator = collectionSeparator;
        }

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

            return this.IsCollection(type)
                ? this.ConvertCollection(settingValue, type)
                : this.ConvertSingle(settingValue, type);
        }

        private bool IsCollection(Type type)
        {
            if (type == typeof(string))
            {
                return false;
            }

            return type.GetInterface(nameof(IEnumerable)) != null;
        }

        private object ConvertSingle(string settingValue, Type type)
        {
            if (settingValue == null)
            {
                return null;
            }

            if (type.IsEnum)
            {
                return Enum.Parse(type, settingValue);
            }

            return System.Convert.ChangeType(settingValue, type);
        }

        private IList ConvertCollection(string settingValue, Type type)
        {
            var stringValues = settingValue.Split(
                new[] { this.collectionSeparator },
                StringSplitOptions.RemoveEmptyEntries);

            var collectionElementType =
                type.GetGenericArguments().Single();

            var values = stringValues.Select(
                v => this.ConvertSingle(v, collectionElementType));

            return this.CreateList(
                collectionElementType,
                values);
        }

        private IList CreateList(Type type, IEnumerable values)
        {
            var listType = typeof(List<>);
            var concreteType = listType.MakeGenericType(type);
            if (!(Activator.CreateInstance(concreteType) is IList newList))
            {
                throw new InvalidOperationException("Unexpected list type.");
            }

            foreach (var value in values)
            {
                newList.Add(value);
            }

            return newList;
        }
    }
}