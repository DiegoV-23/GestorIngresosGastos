using GestorIngresosGastos.Models;

namespace GestorIngresosGastos.Services
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> ObtenerResumenFinancieroAsync(int usuarioId);
    }
}
