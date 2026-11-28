using GestorIngresosGastos.Models;

namespace GestorIngresosGastos.Services
{
    public class DashboardService : IDashboardService
    {
        public Task<DashboardViewModel> ObtenerResumenFinancieroAsync()
        {
            var movimientos = new List<MovimientoResumen>
            {
                new MovimientoResumen { Descripcion = "Salario Mensual", Tipo = "Ingreso", Clasificacion = "Activo", Monto = 35000m, Fecha = DateTime.Now.AddDays(-5) },
                new MovimientoResumen { Descripcion = "Renta Parqueo", Tipo = "Ingreso", Clasificacion = "Pasivo", Monto = 4500m, Fecha = DateTime.Now.AddDays(-3) },
                new MovimientoResumen { Descripcion = "Internet y Luz", Tipo = "Gasto", Clasificacion = "Fijo", Monto = 3800m, Fecha = DateTime.Now.AddDays(-2) },
                new MovimientoResumen { Descripcion = "Compra Supermercado", Tipo = "Gasto", Clasificacion = "Variable", Monto = 5200m, Fecha = DateTime.Now.AddDays(-1) }
            };

            // Cálculos usando LINQ
            var resumen = new DashboardViewModel
            {
                IngresosActivos = movimientos.Where(m => m.Tipo == "Ingreso" && m.Clasificacion == "Activo").Sum(m => m.Monto),
                IngresosPasivos = movimientos.Where(m => m.Tipo == "Ingreso" && m.Clasificacion == "Pasivo").Sum(m => m.Monto),
                TotalIngresos = movimientos.Where(m => m.Tipo == "Ingreso").Sum(m => m.Monto),

                GastosFijos = movimientos.Where(m => m.Tipo == "Gasto" && m.Clasificacion == "Fijo").Sum(m => m.Monto),
                GastosVariables = movimientos.Where(m => m.Tipo == "Gasto" && m.Clasificacion == "Variable").Sum(m => m.Monto),
                TotalGastos = movimientos.Where(m => m.Tipo == "Gasto").Sum(m => m.Monto),

                UltimosMovimientos = movimientos.OrderByDescending(m => m.Fecha).ToList()
            };

            return Task.FromResult(resumen);
        }
    }
}
