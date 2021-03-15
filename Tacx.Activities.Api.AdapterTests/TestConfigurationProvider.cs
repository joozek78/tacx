using Tacx.Activities.Api.Adapters;

namespace Tacx.Activities.Api.AdapterTests
{
    public class TestConfigurationProvider
    {
        // TACX you can load a config file to support multiple environments (testing local + azure)
        // but it's more useful for smoke tests
        public static AppConfiguration AppConfiguration => new AppConfiguration()
        {
            SubscriptionId = "620d8e19-9cb3-42a7-96d2-5033ff997a04",
            ResourceGroupName = "tacx",
            CosmosAccountName = "tacx",
            CosmosDatabaseName = "tacx",
            StorageAccountName = "tacx"
        };
    }
}