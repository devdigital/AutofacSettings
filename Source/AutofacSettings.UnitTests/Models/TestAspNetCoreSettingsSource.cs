// <copyright file="TestAspNetCoreSettingsSource.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.UnitTests.Models
{
    using System.IO;
    using AutofacSettings.Sources;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Test ASP.NET Core settings source.
    /// </summary>
    /// <seealso cref="AutofacSettings.Sources.AspNetCoreSettingsSource" />
    public class TestAspNetCoreSettingsSource : AspNetCoreSettingsSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestAspNetCoreSettingsSource"/> class.
        /// </summary>
        public TestAspNetCoreSettingsSource()
            : base(Settings())
        {
        }

        private static IConfiguration Settings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }
}
