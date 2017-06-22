namespace AutofacSettings
{
    using System;
    using System.Collections.Generic;

    using Autofac;
    using Autofac.Builder;
    using Autofac.Core;

    public class AutofacSettingsSource : IRegistrationSource
    {
        private readonly string settingsPostfix;
        
        public AutofacSettingsSource(string settingsPostfix = "Settings")
        {
            this.settingsPostfix = settingsPostfix;
        }

        public IEnumerable<IComponentRegistration> RegistrationsFor(
            Service service,
            Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            var typedService = service as IServiceWithType;
            if (typedService != null && typedService.ServiceType.IsClass
                && typedService.ServiceType.Name.EndsWith(this.settingsPostfix))
            {
                yield return
                    RegistrationBuilder.ForDelegate(
                        (c, p) => c.Resolve<ISettingsService>().GetSettings(typedService.ServiceType).Result)
                        .As(typedService.ServiceType)
                        .CreateRegistration();
            }
        }

        public bool IsAdapterForIndividualComponents => false;
    }
}