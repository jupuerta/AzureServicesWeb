using Azure;
using Azure.AI.TextAnalytics;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureServicesWeb.Models;

namespace AzureServicesWeb.Services
{
    public class TextoFicheroServices : AzureServicesGeneral
    {
        public async Task<List<TextoFichero>> GetAllDataAsync()
        {
            string Directory = "Data/textofichero";
            await DownloadFilesAsync(ConnectAzureStorage(), Directory);
            return AnalizeSentiment(GetTextFromFile(Directory));
        }

        public virtual TextAnalyticsClient SetUpCognitiveService()
        {
            AzureKeyCredential credentials = new AzureKeyCredential(Credentials);
            Uri endpoint = new Uri(Endpoint);
            TextAnalyticsClient client = new TextAnalyticsClient(endpoint, credentials);
            return client;
        }

        public override BlobContainerClient ConnectAzureStorage()
        {
            var blobUri = new System.Uri(Environment.GetEnvironmentVariable("URI_AZURE_TF", EnvironmentVariableTarget.User));
            BlobContainerClient blobContainerClient = new BlobContainerClient(blobUri, null);
            return blobContainerClient;
        }

        public List<string> GetTextFromFile(string dierctorioName)
        {
            List<string> textFromFiles = new List<string>();
            string[] ficheros = Directory.GetFiles(dierctorioName);
            foreach (string file in ficheros)
            {
                Console.WriteLine(file);
                textFromFiles.Add(System.IO.File.ReadAllText(file));
            }
            return textFromFiles;
        }

        public List<TextoFichero> AnalizeSentiment(List<string> opiniones)
        {
            List<TextoFichero> textoFicherosList = new List<TextoFichero>();
            TextAnalyticsClient client = SetUpCognitiveService();
            AnalyzeSentimentResultCollection reviews = client.AnalyzeSentimentBatch(opiniones, options: new AnalyzeSentimentOptions()
            {
                IncludeOpinionMining = true
            });

            foreach (AnalyzeSentimentResult review in reviews)
            {
                textoFicherosList.Add(ToObjectTextFichero(review));
            }
            return textoFicherosList;
        }

        public TextoFichero ToObjectTextFichero(AnalyzeSentimentResult review)
        {
            TextoFichero textoFichero = new TextoFichero();
            foreach (SentenceSentiment sentence in review.DocumentSentiment.Sentences)
            {
                textoFichero.Texto += sentence.Text;
            }
            textoFichero.Sentimiento = review.DocumentSentiment.Sentiment.ToString();
            textoFichero.ProbPositivo = review.DocumentSentiment.ConfidenceScores.Positive;
            textoFichero.ProbNeutro = review.DocumentSentiment.ConfidenceScores.Neutral;
            textoFichero.ProbNegativo = review.DocumentSentiment.ConfidenceScores.Negative;
            return textoFichero;
        }
    }
}
