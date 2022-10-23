namespace AzureServicesWeb.Models
{
    public class TextoImagen : IOjetosAzureGeneral
    {
        public string? URImagen { get; set; }
        public string? Texto { get; set; }

        public string QueTipoSoy()
        {
            return "Soy un TextoImagen";
        }
    }
}
