using Microsoft.Extensions.Configuration;
using Tacx.Activities.Api.Adapters;

namespace Tacx.Activities.Api.SmokeTests
{
    public class TestConfigurationProvider
    {
        public static AppConfiguration Get()
        {
            var appConfiguration = new AppConfiguration();
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build()
                .Bind(appConfiguration);
            return appConfiguration;
        }
    }
}