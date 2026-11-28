namespace GestorIngresosGastos.Models
{
    public class DashboardViewModel
    {
        public decimal TotalIngresos { get; set; }
        public decimal IngresosActivos { get; set; }
        public decimal IngresosPasivos { get; set; }

        public decimal TotalGastos { get; set; }
        public decimal GastosFijos { get; set; }
        public decimal GastosVariables { get; set; }

        public decimal BalanceDisponible => TotalIngresos - TotalGastos;

        public List<MovimientoResumen> UltimosMovimientos { get; set; } = new();
    }

    public class MovimientoResumen
    {
        public string Descripcion { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty; 
        public string Clasificacion { get; set; } = string.Empty; 
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
