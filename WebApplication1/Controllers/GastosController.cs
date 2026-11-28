using Microsoft.AspNetCore.Mvc;
using GestorIngresosGastos.Models;
using GestorIngresosGastos.Services;

namespace GestorIngresosGastos.Controllers
{
    public class GastosController : Controller
    {
        private readonly IGastoService _gastoService;

        public GastosController(IGastoService gastoService)
        {
            _gastoService = gastoService;
        }

        private int? ObtenerUsuarioId() => HttpContext.Session.GetInt32("UsuarioId");

        public async Task<IActionResult> Index()
        {
            var usuarioId = ObtenerUsuarioId();
            if (usuarioId == null) return RedirectToAction("Login", "Auth");

            var lista = await _gastoService.ObtenerPorUsuarioAsync(usuarioId.Value);
            return View(lista);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (ObtenerUsuarioId() == null) return RedirectToAction("Login", "Auth");
            return View(new Gasto { Fecha = DateTime.Now });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Gasto gasto)
        {
            var usuarioId = ObtenerUsuarioId();
            if (usuarioId == null) return RedirectToAction("Login", "Auth");

            if (gasto.Monto <= 0)
            {
                ViewBag.Error = "El monto del gasto debe ser mayor a RD$ 0.00.";
                return View(gasto);
            }

            gasto.UsuarioId = usuarioId.Value;
            await _gastoService.CrearAsync(gasto);
            TempData["MensajeExito"] = "Gasto registrado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var usuarioId = ObtenerUsuarioId();
            if (usuarioId == null) return RedirectToAction("Login", "Auth");

            var gasto = await _gastoService.ObtenerPorIdAsync(id, usuarioId.Value);
            if (gasto == null) return RedirectToAction(nameof(Index));

            return View(gasto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Gasto gasto)
        {
            var usuarioId = ObtenerUsuarioId();
            if (usuarioId == null) return RedirectToAction("Login", "Auth");

            if (gasto.Monto <= 0)
            {
                ViewBag.Error = "El monto del gasto debe ser mayor a RD$ 0.00.";
                return View(gasto);
            }

            gasto.UsuarioId = usuarioId.Value;
            await _gastoService.ActualizarAsync(gasto);
            TempData["MensajeExito"] = "Gasto actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var usuarioId = ObtenerUsuarioId();
            if (usuarioId == null) return RedirectToAction("Login", "Auth");

            await _gastoService.EliminarAsync(id, usuarioId.Value);
            TempData["MensajeExito"] = "Gasto eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
