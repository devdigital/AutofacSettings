using System;

namespace AutofacSettings
{
    public interface ISettingConverter
    {
        TValue Convert<TValue>(string settingValue);

        object Convert(string settingValue, Type type);
    }
}