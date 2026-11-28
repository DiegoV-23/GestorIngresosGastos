namespace GestorIngresosGastos.Models
{
    public class Ingreso
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string TipoIngreso { get; set; } = string.Empty; // "Activo" o "Pasivo"
        public string Fuente { get; set; } = string.Empty; // Ej. Salario, Alquiler, Dividendos
        public DateTime Fecha { get; set; } = DateTime.Now;

        // Relación privada con el Usuario
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}

