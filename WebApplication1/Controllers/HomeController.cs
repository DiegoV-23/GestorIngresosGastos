using Microsoft.AspNetCore.Mvc;
using GestorIngresosGastos.Services;

namespace GestorIngresosGastos.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public HomeController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var usuarioNombre = HttpContext.Session.GetString("UsuarioNombre");

            if (usuarioId == null || string.IsNullOrEmpty(usuarioNombre))
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.UsuarioNombre = usuarioNombre;
            var resumen = await _dashboardService.ObtenerResumenFinancieroAsync(usuarioId.Value);
            return View(resumen);
        }
    }
}
