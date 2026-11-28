using Microsoft.EntityFrameworkCore;
using GestorIngresosGastos.Data;
using GestorIngresosGastos.Models;

namespace GestorIngresosGastos.Services
{
    public class GastoService : IGastoService
    {
        private readonly AppDbContext _context;

        public GastoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Gasto>> ObtenerPorUsuarioAsync(int usuarioId)
        {
            return await _context.Gastos
                .Where(g => g.UsuarioId == usuarioId)
                .OrderByDescending(g => g.Fecha)
                .ToListAsync();
        }

        public async Task<Gasto?> ObtenerPorIdAsync(int id, int usuarioId)
        {
            return await _context.Gastos
                .FirstOrDefaultAsync(g => g.Id == id && g.UsuarioId == usuarioId);
        }

        public async Task CrearAsync(Gasto gasto)
        {
            _context.Gastos.Add(gasto);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Gasto gasto)
        {
            _context.Gastos.Update(gasto);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id, int usuarioId)
        {
            var gasto = await ObtenerPorIdAsync(id, usuarioId);
            if (gasto != null)
            {
                _context.Gastos.Remove(gasto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
