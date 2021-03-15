using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Nelibur.ObjectMapper;
using Tacx.Activities.Api.Core;
using Tacx.Activities.Api.Core.Adapters;
using Tacx.Activities.Api.Core.Domain;

namespace Tacx.Activities.Api.Adapters.Cosmos
{
    public class CosmosActivityRepository:IActivityRepository
    {
        private readonly Container _activityContainer;

        public CosmosActivityRepository(CosmosClient cosmosClient, IOptions<AppConfiguration> configuration)
        {
            _activityContainer = cosmosClient.GetContainer(configuration.Value.CosmosDatabaseName, "Activity");
        }

        public async Task Create(Activity activity)
        {
            var activityDocument = ActivityDocumentMapper.Map(activity);
            await _activityContainer.UpsertItemAsync(activityDocument);
        }
        
        public async Task Delete(string activityId)
        {
            await _activityContainer.DeleteItemAsync<ActivityDocument>(activityId, new PartitionKey(activityId));
        }

        public async Task<Activity> GetById(string activityId)
        {
            try
            {
                var response = await _activityContainer.ReadItemAsync<ActivityDocument>(activityId, new PartitionKey(activityId));
                return ActivityDocumentMapper.Map(response.Resource);
            }
            catch (CosmosException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                throw;
            }
        }
    }
}