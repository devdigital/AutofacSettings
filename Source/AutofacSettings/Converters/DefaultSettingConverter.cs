using System;

namespace AutofacSettings.Converters
{
    public class DefaultSettingConverter : ISettingConverter
    {
        public TValue Convert<TValue>(string settingValue)
        {
            return (TValue)this.Convert(settingValue, typeof(TValue));
        }

        public object Convert(string settingValue, Type type)
        {            
            if (string.IsNullOrWhiteSpace(settingValue))
            {
                return null;
            }

            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsEnum
                ? Enum.Parse(type, settingValue)
                : System.Convert.ChangeType(settingValue, type);
        }
    }
}