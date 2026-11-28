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

            if (ingreso.Monto <= 0)
            {
                ViewBag.Error = "El monto del ingreso debe ser mayor a RD$ 0.00.";
                return View(ingreso);
            }

            ingreso.UsuarioId = usuarioId.Value;
            await _ingresoService.CrearAsync(ingreso);
            TempData["MensajeExito"] = "Ingreso registrado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var usuarioId = ObtenerUsuarioId();
            if (usuarioId == null) return RedirectToAction("Login", "Auth");

            var ingreso = await _ingresoService.ObtenerPorIdAsync(id, usuarioId.Value);
            if (ingreso == null) return RedirectToAction(nameof(Index));

            return View(ingreso);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Ingreso ingreso)
        {
            var usuarioId = ObtenerUsuarioId();
            if (usuarioId == null) return RedirectToAction("Login", "Auth");

            if (ingreso.Monto <= 0)
            {
                ViewBag.Error = "El monto del ingreso debe ser mayor a RD$ 0.00.";
                return View(ingreso);
            }

            ingreso.UsuarioId = usuarioId.Value;
            await _ingresoService.ActualizarAsync(ingreso);
            TempData["MensajeExito"] = "Ingreso actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var usuarioId = ObtenerUsuarioId();
            if (usuarioId == null) return RedirectToAction("Login", "Auth");

            await _ingresoService.EliminarAsync(id, usuarioId.Value);
            TempData["MensajeExito"] = "Ingreso eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
