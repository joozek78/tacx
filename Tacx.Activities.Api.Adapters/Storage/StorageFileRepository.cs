using System.IO;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Azure.Storage.Blobs;
using Tacx.Activities.Api.Core.Adapters;

namespace Tacx.Activities.Api.Adapters.Storage
{
   public class StorageFileRepository: IFileRepository
    {
        private readonly BlobContainerClient _blobContainerClient;

        public StorageFileRepository(BlobServiceClient blobServiceClient)
        {
            _blobContainerClient = blobServiceClient.GetBlobContainerClient("activities");
        }
        
        public async Task Upload(string fileName, byte[] contents)
        {
            await using var memoryStream = new MemoryStream(contents);
            await _blobContainerClient.UploadBlobAsync(fileName, memoryStream);
        }

        public async Task Delete(string fileName)
        {
            await _blobContainerClient.DeleteBlobAsync(fileName);
        }
    }
}