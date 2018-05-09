// <copyright file="DefaultSettingConverterTests.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.UnitTests.Tests.Converter
{
    using System;
    using System.Collections.Generic;
    using AutoFixture.Xunit2;
    using Converters;
    using Models;
    using Xunit;

    #pragma warning disable SA1600
    #pragma warning disable 1591

    /// <summary>
    /// Default setting converter tests.
    /// </summary>
    public class DefaultSettingConverterTests
    {
        [Theory]
        [AutoData]
        public void GetNullValueReturnsNull(
            DefaultSettingConverter converter)
        {
            var converted = converter.Convert<string>(null);
            Assert.Null(converted);
        }

        [Theory]
        [AutoData]
        public void GetStringReturnsString(
            DefaultSettingConverter converter,
            string settingValue)
        {
            var converted = converter.Convert<string>(settingValue);
            Assert.Equal(settingValue, converted);
        }

        [Theory]
        [AutoData]
        public void GetValidIntReturnsInt(
            DefaultSettingConverter converter,
            int settingValue)
        {
            var converted = converter.Convert<int>(settingValue.ToString());
            Assert.Equal(settingValue, converted);
        }

        [Theory]
        [AutoData]
        public void GetInvalidIntThrowsFormatException(
            DefaultSettingConverter converter,
            string settingValue)
        {
            Assert.Throws<FormatException>(
                () => converter.Convert<int>(settingValue));
        }

        [Theory]
        [AutoData]
        public void GetEnumConvertsToEnum(
            DefaultSettingConverter converter)
        {
            var converted = converter.Convert<TestEnum>("None");
            Assert.Equal(TestEnum.None, converted);
        }

        [Theory]
        [AutoData]
        public void GetInvalidEnumThrowsArgumentException(
            DefaultSettingConverter converter)
        {
            Assert.Throws<ArgumentException>(
                () => converter.Convert<TestEnum>("Foo"));
        }

        [Theory]
        [AutoData]
        public void GetSingleStringAsCollectionConvertsToCollection(
            DefaultSettingConverter converter,
            string settingValue)
        {
            var converted = converter.Convert<IEnumerable<string>>(settingValue);
            Assert.Equal(new List<string> { settingValue }, converted);
        }

        [Theory]
        [AutoData]
        public void GetSeparatedStringAsCollectionConvertsToCollection(
            DefaultSettingConverter converter,
            string settingValue)
        {
            var converted = converter.Convert<IEnumerable<string>>("foo|bar");
            Assert.Equal(new List<string> { "foo", "bar" }, converted);
        }

        [Theory]
        [AutoData]
        public void GetStringAsIntCollectionConvertsToCollection(
            DefaultSettingConverter converter,
            string settingValue)
        {
            var converted = converter.Convert<IEnumerable<int>>("1");
            Assert.Equal(new List<int> { 1 }, converted);
        }

        [Theory]
        [AutoData]
        public void GetSeparatedStringAsIntCollectionConvertsToCollection(
            DefaultSettingConverter converter,
            string settingValue)
        {
            var converted = converter.Convert<IEnumerable<int>>("1|2|3");
            Assert.Equal(new List<int> { 1, 2, 3 }, converted);
        }
    }
}