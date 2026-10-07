
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace WebAppInHollandMTF.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly string _connectionString;
        private const string ContainerName = "note-attachments";

        public BlobStorageService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AzureBlobStorage")
                ?? throw new InvalidOperationException("Blob storage connection string missing.");
        }
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var containerClient = new BlobContainerClient(_connectionString, ContainerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            string fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var blobClient = containerClient.GetBlobClient(fileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
            }

            return blobClient.Uri.ToString();
        }
    }
}
