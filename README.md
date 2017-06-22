# AutofacSettings

Provides setting type injection via convention

```
install-package AutofacSettings
```

AutofacSettings provides a registration source for Autofac which allows settings to be injected and populated via a naming convention.

Add the registration source to the Autofac container builder:

```
var builder = new ContainerBuilder();

builder.RegisterSource(new AutofacSettingsSource());

builder.RegisterInstance(new AppConfigSettingsService("myApp", "Settings"))
  .As<ISettingsService>();
```

The `AutofacSettingsSource` requires an `ISettingsSource` type registered within the container. This provides the following methods:

```
public interface ISettingsService
{
    Task<TValue> GetSettingValue<TValue>(string settingName, TValue defaultValue);

    Task<TSettings> GetSettings<TSettings>() where TSettings : class;

    Task<object> GetSettings(Type type);
}
```

You can implement this interface to read settings from any source (or compose settings), but `AppConfigSettingsService` is an implementation provided out of the box for reading app settings from an app/web.config file. 

The constructor requires:
* `appKeyPrefix` - a string prefix for your setting keys, if you don't wish to use a prefix, you can pass null/empty string
* `settingsPostfix` - what your setting types are postfixed with. Any type with this postfix when injected will use the `AutofacSettings` registration source

For example, in the above code we are using the `appKeyPrefix` of `myApp`, and the `settingsPostfix` of `Settings`. This means we can now create app/web.config settings in the following format:

```
<appSettings>
    <add key="myApp:Logging:Enabled" value="true" />
    <add key="myApp:Logging:Level" value="Trace" />
    ...
</appSettings>
```

These setting values will automatically be mapped to any type injected whose prefix matches the setting key, and whose postfix is `Settings` as configured with the `settingsPostfix` value. In this example, that would be a type called `LoggingSettings`:

```
public class LoggingSettings
{
    public bool Enabled { get; set; }
    public LogLevel Level { get; set; }
}
```

We can now inject this type anywhere we need logging settings, and the properties will automatically be populated from the app/web.config or any `ISettingsSource` that we have registered in the container:

```
public class ConfigureLoggingTask
{
   public ConfigureLoggingTask(LoggingSettings settings)
   {
       this.settings = settings;
   }
   
   public void Run()
   {
      // configure logging based on this.settings
   }
}
```

# Getting settings directly

There may be times where you wish to get a single setting value, or not inject setting types. In this case you can use the other methods available on `ISettingsSource`:

```
public class MyInfrastructureCode
{
   public MyInfrastructureCode(ISettingsSource settingsSource)
   {
       this.settingsSource = settingsSource;
   }
   
   public async Task Run()
   {
      var isLoggingEnabled = await this.settingsSource.GetSettingValue(
        "myApp:Logging:Enabled", 
        defaultValue: false);
      
      // OR
      
      var loggingSettings = await this.settingsSource.GetSettings<LoggingSettings>();      
   }
}
```

The `GetSettingValue` method can retrieve a single setting value. If the value is not available or is not valid for the setting type, then the provided `defaultValue` is returned.

> Note if you require settings in your *domain* types but do not wish to inject them, then it's not recommended that you inject `ISettingsSource` directly otherwise this would violate DIP. Instead, either inject the scalar setting values and configure Autofac to resolve them via `ISettingsSource`, or create a domain abstraction for retrieving settings and an implementation which uses `ISettingsSource`.
