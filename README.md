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
```

The `AutofacSettingsSource` requires an `ISettingsSource` type registered within the containter. This provides the following methods:

You can implement this interface to read settings from any source (or compose settings), but there is one implementation provided out of the box for reading settings from an app/web.config file:

Create a type representing your settings:


Create your settings:

Now you can inject your settings where needed:


