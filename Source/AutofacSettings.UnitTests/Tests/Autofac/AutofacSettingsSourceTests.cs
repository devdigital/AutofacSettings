// <copyright file="AutofacSettingsSourceTests.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutofacSettings.UnitTests.Tests.Autofac
{
    using AutofacSettings.UnitTests.Models;
    using AutofacSettings.UnitTests.Services;
    using FluentAssertions;
    using global::Autofac;
    using Ploeh.AutoFixture.Xunit2;
    using Xunit;

    #pragma warning disable SA1600
    #pragma warning disable 1591

    public class AutofacSettingsSourceTests
    {
        [Theory]
        [AutoData]
        public void AutofacSettingsSourceReturnsExpectedSettings(
            TestAspNetCoreSettingsSource source,
            SettingsServiceBuilder serviceBuilder)
        {
            var service = serviceBuilder.WithSource(source).Build();

            var builder = new ContainerBuilder();
            builder.RegisterSource(
                new AutofacSettingsRegistrationSource(service));

            var container = builder.Build();
            var loggingSettings = container.Resolve<LoggingSettings>();
            var expectedLoggingSettings = new LoggingSettings
            {
                Enabled = true,
                IncludeDetail = true,
            };

            loggingSettings.ShouldBeEquivalentTo(expectedLoggingSettings);
        }
    }
}
