using Azure;
using Azure.AI.TextAnalytics;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Diagnostics;

namespace AzureServicesWeb.Services
{
    public abstract class AzureServicesGeneral : IAzureServices
    {
        protected string Credentials = Environment.GetEnvironmentVariable("URI_AZURE_CREDENTIALS", EnvironmentVariableTarget.User);
        protected string Endpoint = Environment.GetEnvironmentVariable("URI_AZURE_ENDPOINT", EnvironmentVariableTarget.User);
        protected List<string> uris = new List<string>();
        public async Task DownloadFilesAsync(BlobContainerClient blobContainerClient, string directoryName)
        {
            try
            {
                var resultSegment = blobContainerClient.GetBlobsAsync().AsPages();

                await foreach (Azure.Page<BlobItem> blobPage in resultSegment)
                {
                    foreach (BlobItem blobItem in blobPage.Values)
                    {
                        Console.WriteLine("Blob name: {0}", blobItem.Name);
                        string fileName = blobItem.Name;
                        Directory.CreateDirectory(directoryName);
                        string localFilePath = Path.Combine(directoryName, fileName);

                        using (var file = File.Open(localFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            var blobClient = blobContainerClient.GetBlobClient(blobItem.Name);
                            uris.Add(blobClient.Uri.AbsoluteUri);
                            await blobClient.DownloadToAsync(file);
                        }
                    }
                    Console.WriteLine();
                }
            }
            catch (RequestFailedException e)
            {
                if (e.Status == 403)
                {
                    Console.WriteLine("Blob listing operation failed for SAS");
                    Console.WriteLine("Additional error information: " + e.Message);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                    throw;
                }
            }
        }

        public abstract BlobContainerClient ConnectAzureStorage();
    }
}
