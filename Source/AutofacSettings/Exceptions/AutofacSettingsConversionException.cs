// <copyright file="AutofacSettingsConversionException.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.Exceptions
{
    using System;

    /// <summary>
    /// AutofacSettings conversion exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class AutofacSettingsConversionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacSettingsConversionException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public AutofacSettingsConversionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}