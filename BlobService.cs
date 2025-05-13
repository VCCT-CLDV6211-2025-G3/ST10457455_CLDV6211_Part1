using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

public class BlobService
{
    private readonly string _connectionString;
    private readonly string _containerName;

    public BlobService(IConfiguration configuration)
    {
        _connectionString = configuration["AzureStorage:ConnectionString"]
            ?? throw new ArgumentNullException("AzureStorage:ConnectionString", "Connection string is missing in configuration.");
        
        _containerName = configuration["AzureStorage:ContainerName"]
            ?? throw new ArgumentNullException("AzureStorage:ContainerName", "Container name is missing in configuration.");
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
    {
        var containerClient = new BlobContainerClient(_connectionString, _containerName);
        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream, overwrite: true);

        return blobClient.Uri.ToString(); // Store this URL in the DB
    }

    internal void DeleteFile(string fileName)
    {
        throw new NotImplementedException();
    }
}

