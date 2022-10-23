using Azure.AI.TextAnalytics;
using Azure.Storage.Blobs;
using AzureServicesWeb.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;


namespace AzureServicesWeb.Services
{
    public class TextoImagenServices : AzureServicesGeneral
    {
        public async Task<List<TextoImagen>> GetAllDataAsync()
        {
            string Directory = "Data/textoimagen";
            await DownloadFilesAsync(ConnectAzureStorage(), Directory);
            return GetTextFromImage(SetUpCognitiveService(), Directory);
        }

        public virtual ComputerVisionClient SetUpCognitiveService()
        {
            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(Credentials));
            client.Endpoint = Endpoint;
            return client;
        }

        public override BlobContainerClient ConnectAzureStorage()
        {
            var blobUri = new System.Uri(Environment.GetEnvironmentVariable("URI_AZURE_TI", EnvironmentVariableTarget.User));
            BlobContainerClient blobContainerClient = new BlobContainerClient(blobUri, null);
            return blobContainerClient;
        }

        public List<TextoImagen> GetTextFromImage(ComputerVisionClient client, string dierctorioName)
        {
            List<TextoImagen> textFromImages = new List<TextoImagen>();
            string[] ficheros = Directory.GetFiles(dierctorioName);
            foreach (string file in ficheros)
            {
                TextoImagen textoImagen = new TextoImagen();        
                var myfile = File.OpenRead(file);
                string[] words = file.Split('\\');
                foreach (string ul in uris)
                {
                    if (ul.Contains(words[1]))
                    {
                        textoImagen.URImagen = ul;
                    }
                }
                var result = client.RecognizePrintedTextInStreamAsync(false, myfile);
                result.Wait();

                var rst = result.Result;
                foreach (var r in rst.Regions)
                {
                    foreach (var t in r.Lines)
                    {
                        foreach (var w in t.Words)
                        {
                            textoImagen.Texto+=" "+w.Text;
                        }
                    }
                }
                textFromImages.Add(textoImagen);
            }
            return textFromImages;
        }
    }
}
