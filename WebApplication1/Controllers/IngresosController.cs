using Microsoft.AspNetCore.Mvc;
using GestorIngresosGastos.Models;
using GestorIngresosGastos.Services;

namespace GestorIngresosGastos.Controllers
{
    public class IngresosController : Controller
    {
        private readonly IIngresoService _ingresoService;

        public IngresosController(IIngresoService ingresoService)
        {
            _ingresoService = ingresoService;
        }

        private int? ObtenerUsuarioId() => HttpContext.Session.GetInt32("UsuarioId");

        public async Task<IActionResult> Index()
        {
            var usuarioId = ObtenerUsuarioId();
            if (usuarioId == null) return RedirectToAction("Login", "Auth");

            var lista = await _ingresoService.ObtenerPorUsuarioAsync(usuarioId.Value);
            return View(lista);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (ObtenerUsuarioId() == null) return RedirectToAction("Login", "Auth");
            return View(new Ingreso { Fecha = DateTime.Now });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ingreso ingreso)
        {
            var usuarioId = ObtenerUsuarioId();
            if (usuarioId == null) return RedirectToAction("Login", "Auth");

            ingreso.UsuarioId = usuarioId.Value;
            await _ingresoService.CrearAsync(ingreso);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var usuarioId = ObtenerUsuarioId();
            if (usuarioId == null) return RedirectToAction("Login", "Auth");

            var ingreso = await _ingresoService.ObtenerPorIdAsync(id, usuarioId.Value);
            if (ingreso == null) return NotFound();

            return View(ingreso);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Ingreso ingreso)
        {
            var usuarioId = ObtenerUsuarioId();
            if (usuarioId == null) return RedirectToAction("Login", "Auth");

            ingreso.UsuarioId = usuarioId.Value;
            await _ingresoService.ActualizarAsync(ingreso);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var usuarioId = ObtenerUsuarioId();
            if (usuarioId == null) return RedirectToAction("Login", "Auth");

            await _ingresoService.EliminarAsync(id, usuarioId.Value);
            return RedirectToAction(nameof(Index));
        }
    }
}
