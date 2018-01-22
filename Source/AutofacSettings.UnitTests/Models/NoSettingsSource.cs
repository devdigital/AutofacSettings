// <copyright file="NoSettingsSource.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.UnitTests.Models
{
    using System.Collections.Specialized;
    using AutofacSettings.Sources;

    /// <summary>
    /// No settings source.
    /// </summary>
    /// <seealso cref="AutofacSettings.Sources.AppConfigSettingsSource" />
    public class NoSettingsSource : AppConfigSettingsSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoSettingsSource"/> class.
        /// </summary>
        public NoSettingsSource()
            : base(new NameValueCollection())
        {
        }
    }
}