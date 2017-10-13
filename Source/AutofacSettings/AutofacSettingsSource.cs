namespace AutofacSettings
{
    using System;
    using System.Collections.Generic;

    using Autofac;
    using Autofac.Builder;
    using Autofac.Core;

    public class AutofacSettingsSource : IRegistrationSource
    {
        private readonly ISettingsService settingsService;

        private readonly string settingsPostfix;
        
        public AutofacSettingsSource(ISettingsService settingsService, string settingsPostfix = "Settings")
        {
            this.settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsPostfix));
            this.settingsPostfix = settingsPostfix;
        }

        public IEnumerable<IComponentRegistration> RegistrationsFor(
            Service service,
            Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            if (service is IServiceWithType typedService && typedService.ServiceType.IsClass
                && typedService.ServiceType.Name.EndsWith(this.settingsPostfix))
            {
                yield return
                    RegistrationBuilder.ForDelegate(
                        (c, p) => this.settingsService.GetSettings(typedService.ServiceType).GetAwaiter().GetResult())
                        .As(typedService.ServiceType)
                        .CreateRegistration();
            }
        }

        public bool IsAdapterForIndividualComponents => false;
    }
}