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
            var usuario = HttpContext.Session.GetString("UsuarioNombre");
            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.UsuarioNombre = usuario;
            var resumen = await _dashboardService.ObtenerResumenFinancieroAsync();
            return View(resumen);
        }
    }
}
