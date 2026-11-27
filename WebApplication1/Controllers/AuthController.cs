using Microsoft.AspNetCore.Mvc;
using GestorIngresosGastos.Models;
using GestorIngresosGastos.Services;

namespace GestorIngresosGastos.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string password)
        {
            var usuario = await _authService.ValidarCredencialesAsync(correo, password);
            if (usuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos.";
                return View();
            }

            HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(Usuario nuevoUsuario)
        {
            var registrado = await _authService.RegistrarUsuarioAsync(nuevoUsuario);
            if (!registrado)
            {
                ViewBag.Error = "Ese correo electrónico ya está registrado.";
                return View(nuevoUsuario);
            }

            TempData["MensajeExito"] = "Cuenta creada exitosamente. Ya puedes iniciar sesión.";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
