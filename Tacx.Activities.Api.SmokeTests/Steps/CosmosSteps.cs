using System;
using System.Net;
using System.Threading.Tasks;
using Azure.Core;
using FluentAssertions;
using Microsoft.Azure.Cosmos;
using Tacx.Activities.Api.Adapters;
using Tacx.Activities.Api.Adapters.Cosmos;
using Tacx.Activities.Api.SmokeTests.Contexts;
using TechTalk.SpecFlow;

namespace Tacx.Activities.Api.SmokeTests.Steps
{
    [Binding]
    public class CosmosSteps: IDisposable
    {
        private readonly ActivityIdContext _activityIdContext;
        private readonly CosmosClient _cosmosClient;
        private readonly Database _cosmosDatabase;

        public CosmosSteps(ActivityIdContext activityIdContext, AppConfiguration appConfiguration, TokenCredential tokenCredential)
        {
            _activityIdContext = activityIdContext;
            _cosmosClient = CosmosClientConnector.CreateClient(appConfiguration, tokenCredential);
            _cosmosDatabase = _cosmosClient.GetDatabase(appConfiguration.CosmosDatabaseName);
        }
        
        [Then(@"a document exists in container '(.*)' with generated ID")]
        public async Task ThenADocumentExistsInContainerActivitiesWithGeneratedId(string containerId)
        {
            var responseMessage = await _cosmosDatabase.GetContainer(containerId).ReadItemStreamAsync(_activityIdContext.ActivityId, new PartitionKey(_activityIdContext.ActivityId));
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        public void Dispose()
        {
            _cosmosClient?.Dispose();
        }
    }
}