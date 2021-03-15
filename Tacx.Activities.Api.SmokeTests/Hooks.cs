using System;
using System.Net.Http;
using Azure.Core;
using Azure.Identity;
using BoDi;
using TechTalk.SpecFlow;

namespace Tacx.Activities.Api.SmokeTests
{
    [Binding]
    public class Hooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun(IObjectContainer container)
        {
            container.RegisterFactoryAs(c => new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5000/")
            });
            container.RegisterInstanceAs<TokenCredential>(new DefaultAzureCredential(includeInteractiveCredentials: true));
            container.RegisterInstanceAs(TestConfigurationProvider.Get());
        }

        [AfterScenario]
        public void AfterScenario(IObjectContainer container)
        {
        }
    }
}