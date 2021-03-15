#nullable enable
using System;
using System.Collections.Generic;
using BoDi;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Tacx.Activities.Api.Core.Adapters;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Tracing;

namespace Tacx.Activities.Api.AcceptanceTests
{
    [Binding]
    public class Hooks
    {
        // SpecFlow runs tests in a single thread, nothing to worry about
        private static List<Mock> _mocks = new();
        
        [BeforeTestRun]
        public static void BeforeTestRun(IObjectContainer container, ITraceListener testTracer)
        {
            var serviceCollection = new ServiceCollection();
            Startup.ConfigureCoreServices(serviceCollection);
            RegisterStub<IActivityRepository, InMemoryActivityRepository>(container, serviceCollection, new InMemoryActivityRepository());
            RegisterMock<IFileRepository>(container, serviceCollection);
            RegisterMock<IClock>(container, serviceCollection);
            container.RegisterInstanceAs<IServiceProvider>(serviceCollection.BuildServiceProvider());
        }
        
        private static void RegisterStub<TInterface, TImplementation>(
            IObjectContainer specFlowContainer,
            IServiceCollection appContainer,
            TImplementation stub)
        where TImplementation: class, TInterface where TInterface : class
        {
            specFlowContainer.RegisterInstanceAs(stub);
            appContainer.AddSingleton<TInterface>(stub);
        }

        private static void RegisterMock<TInterface>(
            IObjectContainer specFlowContainer,
            IServiceCollection appContainer)
        where TInterface : class
        {
            var mock = new Mock<TInterface>();
            specFlowContainer.RegisterInstanceAs(mock);
            appContainer.AddSingleton<TInterface>(mock.Object);
            _mocks.Add(mock);
        }

        [AfterScenario]
        public void AfterScenario(IObjectContainer container)
        {
            container.Resolve<InMemoryActivityRepository>().Reset();
            foreach (var mock in _mocks)
            {
                mock.Reset();
            }
        }
    }
}