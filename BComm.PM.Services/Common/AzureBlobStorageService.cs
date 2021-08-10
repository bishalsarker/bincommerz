using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Services.Common
{
    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobStorageService(IConfiguration configuration)
        {
            var connStr = configuration.GetSection("AzureSettings:BlobStorageConnStr").Value;
            _blobServiceClient = new BlobServiceClient(connStr);
        }

        public async Task UploadFileAsync(string blobContainer, string fileName, Stream stream)
        {
            BlobContainerClient containerClient =  _blobServiceClient.GetBlobContainerClient(blobContainer);
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(stream, true);
        }

        public async Task DeleteFileAsync(string blobContainer, string fileName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(blobContainer);
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.DeleteAsync();
        }
    }
}
