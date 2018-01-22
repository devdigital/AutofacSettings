// <copyright file="TestAppConfigSettingsSource.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.UnitTests.Models
{
    using System.Collections.Specialized;
    using AutofacSettings.Sources;

    /// <summary>
    /// Test app config settings source.
    /// </summary>
    /// <seealso cref="AutofacSettings.Sources.AppConfigSettingsSource" />
    public class TestAppConfigSettingsSource : AppConfigSettingsSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestAppConfigSettingsSource"/> class.
        /// </summary>
        public TestAppConfigSettingsSource()
            : base(Settings())
        {
        }

        private static NameValueCollection Settings()
        {
            return new NameValueCollection
            {
                { "Logging:Enabled", "true" },
                { "Logging:IncludeDetail", "true" },
            };
        }
    }
}