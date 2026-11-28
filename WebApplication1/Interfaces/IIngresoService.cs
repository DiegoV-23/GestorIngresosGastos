using GestorIngresosGastos.Models;

namespace GestorIngresosGastos.Services
{
    public interface IIngresoService
    {
        Task<List<Ingreso>> ObtenerPorUsuarioAsync(int usuarioId);
        Task<Ingreso?> ObtenerPorIdAsync(int id, int usuarioId);
        Task CrearAsync(Ingreso ingreso);
        Task ActualizarAsync(Ingreso ingreso);
        Task EliminarAsync(int id, int usuarioId);
    }
}
