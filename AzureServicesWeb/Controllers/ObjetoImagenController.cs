using AzureServicesWeb.Models;
using AzureServicesWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AzureServicesWeb.Controllers
{
    public class ObjetoImagenController : Controller
    {
        public async Task<IActionResult> Index()
        { 
            ObjetoImagenServices service = new ObjetoImagenServices();
            List<ObjetoImagen> objima = (await service.GetAllDataAsync()) as List<ObjetoImagen>;
            return View(objima);
        }
    }
}
