using Microsoft.EntityFrameworkCore;
using GestorIngresosGastos.Data;
using GestorIngresosGastos.Models;

namespace GestorIngresosGastos.Services
{
    public class IngresoService : IIngresoService
    {
        private readonly AppDbContext _context;

        public IngresoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ingreso>> ObtenerPorUsuarioAsync(int usuarioId)
        {
            return await _context.Ingresos
                .Where(i => i.UsuarioId == usuarioId)
                .OrderByDescending(i => i.Fecha)
                .ToListAsync();
        }

        public async Task<Ingreso?> ObtenerPorIdAsync(int id, int usuarioId)
        {
            return await _context.Ingresos
                .FirstOrDefaultAsync(i => i.Id == id && i.UsuarioId == usuarioId);
        }

        public async Task CrearAsync(Ingreso ingreso)
        {
            _context.Ingresos.Add(ingreso);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Ingreso ingreso)
        {
            _context.Ingresos.Update(ingreso);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id, int usuarioId)
        {
            var ingreso = await ObtenerPorIdAsync(id, usuarioId);
            if (ingreso != null)
            {
                _context.Ingresos.Remove(ingreso);
                await _context.SaveChangesAsync();
            }
        }
    }
}

