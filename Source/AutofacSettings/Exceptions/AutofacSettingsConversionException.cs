using System;

namespace AutofacSettings.Exceptions
{
    public class AutofacSettingsConversionException : Exception
    {
        public AutofacSettingsConversionException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}