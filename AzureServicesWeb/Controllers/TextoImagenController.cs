using AzureServicesWeb.Models;
using AzureServicesWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureServicesWeb.Controllers
{
    public class TextoImagenController : Controller
    {
        public async Task<IActionResult> Index()
        {
            TextoImagenServices service = new TextoImagenServices();
            List<TextoImagen> textima = (await service.GetAllDataAsync()) as List<TextoImagen>;
            return View(textima);
        }
    }
}
