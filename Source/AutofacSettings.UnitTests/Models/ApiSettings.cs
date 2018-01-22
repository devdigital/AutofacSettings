// <copyright file="ApiSettings.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.UnitTests.Models
{
    /// <summary>
    /// API settings.
    /// </summary>
    internal class ApiSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ApiSettings"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path { get; set; }
    }
}