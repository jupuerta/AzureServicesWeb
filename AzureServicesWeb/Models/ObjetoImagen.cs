namespace AzureServicesWeb.Models
{
    public class ObjetoImagen:IOjetosAzureGeneral
    {
        public string? URImagen { get; set; }
        public string? Objetos { get; set; }

        public string QueTipoSoy()
        {
            return "Soy un ObjetoImagen";
        }
    }
}
