// <copyright file="SettingsServiceBuilder.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.UnitTests.Services
{
    using System;
    using AutofacSettings.Converters;
    using AutofacSettings.Handlers;

    /// <summary>
    /// Settings service builder.
    /// </summary>
    public class SettingsServiceBuilder
    {
        /// <summary>
        /// The application key prefix
        /// </summary>
        private readonly string appKeyPrefix;

        /// <summary>
        /// The settings postfix
        /// </summary>
        private readonly string settingsPostfix;

        /// <summary>
        /// The converter
        /// </summary>
        private readonly ISettingConverter converter;

        /// <summary>
        /// The handler
        /// </summary>
        private readonly IInvalidSettingHandler handler;

        /// <summary>
        /// The source
        /// </summary>
        private ISettingsSource source;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsServiceBuilder"/> class.
        /// </summary>
        public SettingsServiceBuilder()
        {
            this.appKeyPrefix = string.Empty;
            this.settingsPostfix = "Settings";
            this.converter = new DefaultSettingConverter();
            this.handler = new ThrowOnInvalidSettingHandler();
        }

        /// <summary>
        /// With setting source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The builder.</returns>
        public SettingsServiceBuilder WithSource(ISettingsSource source)
        {
            this.source = source ?? throw new ArgumentNullException(nameof(source));
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>The settings service.</returns>
        public ISettingsService Build()
        {
            return new DefaultSettingsService(
                this.source,
                this.appKeyPrefix,
                this.settingsPostfix,
                this.converter,
                this.handler);
        }
    }
}