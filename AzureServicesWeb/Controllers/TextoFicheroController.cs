using AzureServicesWeb.Models;
using AzureServicesWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureServicesWeb.Controllers
{
    public class TextoFicheroController : Controller
    {
        public async Task<IActionResult> Index()
        {
            TextoFicheroServices service = new TextoFicheroServices();
            List<TextoFichero> textfich = (await service.GetAllDataAsync()) as List<TextoFichero>;

            ViewData["num"] = textfich.Count();
            ViewData["MedPos"] = Math.Round(textfich.Average(x=>x.ProbPositivo),2);
            ViewData["MedNeut"] = Math.Round(textfich.Average(x => x.ProbNeutro), 2);
            ViewData["MedNeg"] = Math.Round(textfich.Average(x => x.ProbNegativo), 2);
            return View(textfich);
        }
    }
}
