using AWSMvcConciertos.Models;
using AWSMvcConciertos.Services;
using Microsoft.AspNetCore.Mvc;

namespace AWSMvcConciertos.Controllers
{
    public class ConciertosController : Controller
    {
        private ServiceApiConciertos service;

        public ConciertosController(ServiceApiConciertos service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Evento> eventos = await this.service.GetEventosAsync();
            return View(eventos);
        }

        public async Task<IActionResult> FilterEventos()
        {
            List<CategoriaEvento> categorias = await this.service.GetCategoriasEventosAsync();
            ViewData["CATEGORIAS"] = categorias;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FilterEventos(int idcategoria)
        {
            List<Evento> eventos = await this.service.GetEventosCategoriaAsync(idcategoria);
            List<CategoriaEvento> categorias = await this.service.GetCategoriasEventosAsync();
            ViewData["CATEGORIAS"] = categorias;
            return View(eventos);
        }
    }
}
