using System.Linq;
using Azure.Core;
using Azure.ResourceManager.CosmosDB;
using Microsoft.Azure.Cosmos;

namespace Tacx.Activities.Api.Adapters.Cosmos
{
    public class CosmosClientConnector
    {
        public static CosmosClient CreateClient(AppConfiguration appAppConfiguration, TokenCredential credential)
        {
            var cosmosDbManagementClient = new CosmosDBManagementClient(appAppConfiguration.SubscriptionId, credential);
            var connectionStringsResponse = cosmosDbManagementClient.DatabaseAccounts.ListConnectionStrings(
                appAppConfiguration.ResourceGroupName,
                appAppConfiguration.CosmosAccountName);
            var connectionString = connectionStringsResponse.Value.ConnectionStrings.First().ConnectionString;
            return new CosmosClient(connectionString);
        }
    }
}