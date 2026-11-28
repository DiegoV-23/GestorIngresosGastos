using GestorIngresosGastos.Models;

namespace GestorIngresosGastos.Services
{
    public interface IGastoService
    {
        Task<List<Gasto>> ObtenerPorUsuarioAsync(int usuarioId);
        Task<Gasto?> ObtenerPorIdAsync(int id, int usuarioId);
        Task CrearAsync(Gasto gasto);
        Task ActualizarAsync(Gasto gasto);
        Task EliminarAsync(int id, int usuarioId);
    }
}

