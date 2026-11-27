using GestorIngresosGastos.Models;

namespace GestorIngresosGastos.Services
{
    public interface IAuthService
    {
        Task<Usuario?> ValidarCredencialesAsync(string correo, string password);
        Task<bool> RegistrarUsuarioAsync(Usuario nuevoUsuario);
    }
}
