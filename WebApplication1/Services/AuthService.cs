using Microsoft.EntityFrameworkCore;
using GestorIngresosGastos.Data;
using GestorIngresosGastos.Models;

namespace GestorIngresosGastos.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ValidarCredencialesAsync(string correo, string password)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Password == password);
        }

        public async Task<bool> RegistrarUsuarioAsync(Usuario nuevoUsuario)
        {
            var existe = await _context.Usuarios.AnyAsync(u => u.Correo == nuevoUsuario.Correo);
            if (existe) return false;

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
