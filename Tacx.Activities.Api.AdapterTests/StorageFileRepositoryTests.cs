using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage.Blobs;
using FluentAssertions;
using NUnit.Framework;
using Tacx.Activities.Api.Adapters.Storage;

namespace Tacx.Activities.Api.AdapterTests
{
    public class StorageFileRepositoryTests
    {
        private StorageFileRepository _sut;
        private BlobContainerClient _containerClient;
        private string _createdBlobName;

        [SetUp]
        public void SetUp()
        {
            var appConfiguration = TestConfigurationProvider.AppConfiguration;
            var blobServiceClient = BlobStorageConnector.CreateClient(appConfiguration, new DefaultAzureCredential(includeInteractiveCredentials: true));
            _containerClient = blobServiceClient.GetBlobContainerClient("activities");
            _sut = new StorageFileRepository(blobServiceClient);
        }

        [TearDown]
        public async Task TearDown()
        {
            if (_createdBlobName != null)
            {
                await _containerClient.DeleteBlobIfExistsAsync(_createdBlobName);
            }
        }
        
        [Test]
        public async Task ShouldUploadFile()
        {
            var expectedContents = Encoding.UTF8.GetBytes("testcontent");
            var blobName = $"integration-{Guid.NewGuid()}";
            _createdBlobName = blobName;

            await _sut.Upload(blobName, expectedContents);

            var memoryStream = new MemoryStream();
            await _containerClient.GetBlobClient(blobName).DownloadToAsync(memoryStream);
            memoryStream.ToArray().Should().BeEquivalentTo(expectedContents);
        }

        [Test]
        public async Task ShouldDeleteFile()
        {
            var blobName = $"integration-{Guid.NewGuid()}";
            _createdBlobName = blobName;
            await _containerClient.UploadBlobAsync(blobName, Stream.Null);

            await _sut.Delete(blobName);

            var blobExists = await _containerClient.GetBlobClient(blobName).ExistsAsync();
            blobExists.Value.Should().BeFalse();
        }
    }
}