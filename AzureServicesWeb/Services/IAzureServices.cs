using Azure.AI.TextAnalytics;
using Azure.Storage.Blobs;

namespace AzureServicesWeb.Services
{
    public interface IAzureServices
    {
        public BlobContainerClient ConnectAzureStorage();
    }
}
