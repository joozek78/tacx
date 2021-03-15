using System;
using System.Net;
using System.Threading.Tasks;
using Azure.Identity;
using FluentAssertions;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Tacx.Activities.Api.Adapters;
using Tacx.Activities.Api.Adapters.Cosmos;
using Activity = Tacx.Activities.Api.Core.Domain.Activity;

namespace Tacx.Activities.Api.AdapterTests
{
    public class ActivityRepositoryTests:IDisposable
    {
        private CosmosClient _cosmosClient;
        private CosmosActivityRepository _sut;
        private Container _activityContainer;
        private string _createdItemId;

        [SetUp]
        public void Setup()
        {
            // TACX you can use environment variables to detect if you're running in pipeline and disable interactive login 
            var credential = new DefaultAzureCredential(includeInteractiveCredentials: true);
            var appConfiguration = TestConfigurationProvider.AppConfiguration;
            _cosmosClient = CosmosClientConnector.CreateClient(appConfiguration, credential);
            _sut = new CosmosActivityRepository(_cosmosClient, Options.Create(appConfiguration));
            _activityContainer = _cosmosClient.GetContainer(appConfiguration.CosmosDatabaseName, "Activity");
        }

        [TearDown]
        public async Task TearDown()
        {
            if (_createdItemId != null)
            {
                await _activityContainer.DeleteItemStreamAsync(_createdItemId, new PartitionKey(_createdItemId));
            }
        }
        
        [Test]
        public async Task ShouldAddActivity()
        {
            string id = GenerateId();
            _createdItemId = id;
            var activity = new Activity()
            {
                ActivityId = id,
                Name = "test",
                Description = "Running around",
                DistanceKm = 10,
            };
            
            await _sut.Create(activity);

            var actualDocument = (await _activityContainer.ReadItemAsync<Activity>(activity.ActivityId, new PartitionKey(activity.ActivityId))).Resource;
            actualDocument.Should().BeEquivalentTo(activity);
        }

        [Test]
        public async Task ShouldUpdateActivity()
        {
            string id = Guid.NewGuid().ToString();
            _createdItemId = id;
            await _activityContainer.UpsertItemAsync(new {id, Name = "old name"});
            var activity = new Activity()
            {
                ActivityId = id,
                Name = "new name",
                Description = "Running around",
                DistanceKm = 10,
            };
            
            await _sut.Create(activity);
            
            var returnedItem = (await _activityContainer.ReadItemAsync<Activity>(activity.ActivityId, new PartitionKey(activity.ActivityId))).Resource;
            returnedItem.Should().BeEquivalentTo(activity);
        }

        [Test]
        public async Task ShouldDeleteActivity()
        {
            string id = GenerateId();
            await _activityContainer.UpsertItemAsync(new {id});
            
            await _sut.Delete(id);

            using var response = await _activityContainer.ReadItemStreamAsync(id, new PartitionKey(id));
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private static string GenerateId()
        {
            return $"integration-{Guid.NewGuid().ToString()}";
        }

        public void Dispose()
        {
            _cosmosClient.Dispose();
        }
    }
}