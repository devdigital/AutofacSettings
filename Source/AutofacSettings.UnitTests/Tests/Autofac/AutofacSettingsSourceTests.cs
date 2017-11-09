using Autofac;
using AutofacSettings.UnitTests.Models;
using AutofacSettings.UnitTests.Services;
using FluentAssertions;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace AutofacSettings.UnitTests.Tests.Autofac
{
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
                IncludeDetail = true
            };

            loggingSettings.ShouldBeEquivalentTo(expectedLoggingSettings);
        }
    }
}
