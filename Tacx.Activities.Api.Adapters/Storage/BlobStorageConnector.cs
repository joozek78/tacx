using System;
using Azure.Core;
using Azure.Storage.Blobs;

namespace Tacx.Activities.Api.Adapters.Storage
{
    public class BlobStorageConnector
    {
        public static BlobServiceClient CreateClient(AppConfiguration appConfiguration, TokenCredential credential)
        {
            string containerEndpoint = $"https://{appConfiguration.StorageAccountName}.blob.core.windows.net";
            return new BlobServiceClient(new Uri(containerEndpoint), credential);
        }
    }
}