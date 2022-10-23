namespace AzureServicesWeb.Models
{
    public class TextoFichero : IOjetosAzureGeneral
    {
        public string? Texto { get; set; }
        public string? Sentimiento { get; set; }
        public double ProbPositivo { get; set; }
        public double ProbNeutro { get; set; }
        public double ProbNegativo { get; set; }

        public string QueTipoSoy()
        {
            return "Soy un TextoFichero";
        }
    }
}
