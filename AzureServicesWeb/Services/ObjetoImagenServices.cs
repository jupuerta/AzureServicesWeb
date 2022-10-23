using Azure.Storage.Blobs;
using AzureServicesWeb.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace AzureServicesWeb.Services
{
    public class ObjetoImagenServices : AzureServicesGeneral
    {
        public async Task<List<ObjetoImagen>> GetAllDataAsync()
        {
            string Directory = "Data/objetoimagen";
            await DownloadFilesAsync(ConnectAzureStorage(), Directory);
            return (await GetObjectFromImageAsync(SetUpCognitiveService(), Directory)) as List<ObjetoImagen>;
        }

        public virtual ComputerVisionClient SetUpCognitiveService()
        {
            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(Credentials));
            client.Endpoint = Endpoint;
            return client;
        }

        public override BlobContainerClient ConnectAzureStorage()
        {
            var blobUri = new System.Uri(Environment.GetEnvironmentVariable("URI_AZURE_OI", EnvironmentVariableTarget.User));
            BlobContainerClient blobContainerClient = new BlobContainerClient(blobUri, null);
            return blobContainerClient;
        }

        public async Task<List<ObjetoImagen>> GetObjectFromImageAsync(ComputerVisionClient client, string dierctorioName)
        {
            List<ObjetoImagen> objectFromImages = new List<ObjetoImagen>();
            List<VisualFeatureTypes?> features = new()
            {
                VisualFeatureTypes.Tags,
                VisualFeatureTypes.Objects
            };

            string[] ficheros = Directory.GetFiles(dierctorioName);
            foreach (string file in ficheros)
            {
                ObjetoImagen objim = new ObjetoImagen();
                string[] words = file.Split('\\');
                foreach (string ul in uris)
                {
                    if (ul.Contains(words[1]))
                    {
                        objim.URImagen = ul;
                    }
                }
                ImageAnalysis analysis;
                using (Stream imageStream = File.OpenRead(file))
                {
                    analysis = await client.AnalyzeImageInStreamAsync(imageStream, features);
                }

                objim.Objetos = "Objetos encontrados: ";
                foreach (var obj in analysis.Objects)
                {
                    objim.Objetos += obj.ObjectProperty + ", ";
                }

                objim.Objetos += "  -- Descripción Imagen: ";
                foreach (var tag in analysis.Tags)
                {
                    objim.Objetos += tag.Name + ", ";
                }
                objectFromImages.Add(objim);
            }  
            return objectFromImages;
        }
    }
}
