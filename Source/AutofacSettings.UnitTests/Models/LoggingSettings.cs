// <copyright file="LoggingSettings.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.UnitTests.Models
{
    /// <summary>
    /// Logging settings.
    /// </summary>
    internal class LoggingSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LoggingSettings"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include detail.
        /// </summary>
        /// <value>
        ///   <c>true</c> if include detail; otherwise, <c>false</c>.
        /// </value>
        public bool IncludeDetail { get; set; }
    }
}