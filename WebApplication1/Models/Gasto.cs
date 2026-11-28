namespace GestorIngresosGastos.Models
{
    public class Gasto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string TipoGasto { get; set; } = string.Empty; // "Fijo" o "Variable"
        public string Categoria { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;

        // Relación privada con el Usuario que inició sesión
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
