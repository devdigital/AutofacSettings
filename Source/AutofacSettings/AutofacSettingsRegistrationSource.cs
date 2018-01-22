// <copyright file="AutofacSettingsRegistrationSource.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings
{
    using System;
    using System.Collections.Generic;
    using Autofac.Builder;
    using Autofac.Core;

    /// <summary>
    /// AutofacSettings registration source.
    /// </summary>
    /// <seealso cref="Autofac.Core.IRegistrationSource" />
    public class AutofacSettingsRegistrationSource : IRegistrationSource
    {
        private readonly ISettingsService settingsService;

        private readonly string settingsPostfix;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacSettingsRegistrationSource"/> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="settingsPostfix">The settings postfix.</param>
        public AutofacSettingsRegistrationSource(ISettingsService settingsService, string settingsPostfix = "Settings")
        {
            this.settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsPostfix));
            this.settingsPostfix = settingsPostfix;
        }

        /// <inheritdoc />
        public bool IsAdapterForIndividualComponents => false;

        /// <inheritdoc />
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
    }
}