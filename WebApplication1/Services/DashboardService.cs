using Microsoft.EntityFrameworkCore;
using GestorIngresosGastos.Data;
using GestorIngresosGastos.Models;

namespace GestorIngresosGastos.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardViewModel> ObtenerResumenFinancieroAsync(int usuarioId)
        {
            var gastos = await _context.Gastos
                .Where(g => g.UsuarioId == usuarioId)
                .ToListAsync();

            var ingresos = await _context.Ingresos
                .Where(i => i.UsuarioId == usuarioId)
                .ToListAsync();

            var movimientos = new List<MovimientoResumen>();

            movimientos.AddRange(ingresos.Select(i => new MovimientoResumen
            {
                Descripcion = i.Descripcion,
                Tipo = "Ingreso",
                Clasificacion = i.TipoIngreso,
                Monto = i.Monto,
                Fecha = i.Fecha
            }));

            movimientos.AddRange(gastos.Select(g => new MovimientoResumen
            {
                Descripcion = g.Descripcion,
                Tipo = "Gasto",
                Clasificacion = g.TipoGasto,
                Monto = g.Monto,
                Fecha = g.Fecha
            }));

            return new DashboardViewModel
            {
                IngresosActivos = ingresos.Where(i => i.TipoIngreso == "Activo").Sum(i => i.Monto),
                IngresosPasivos = ingresos.Where(i => i.TipoIngreso == "Pasivo").Sum(i => i.Monto),
                TotalIngresos = ingresos.Sum(i => i.Monto),

                GastosFijos = gastos.Where(g => g.TipoGasto == "Fijo").Sum(g => g.Monto),
                GastosVariables = gastos.Where(g => g.TipoGasto == "Variable").Sum(g => g.Monto),
                TotalGastos = gastos.Sum(g => g.Monto),

                UltimosMovimientos = movimientos.OrderByDescending(m => m.Fecha).Take(10).ToList()
            };
        }
    }
}
