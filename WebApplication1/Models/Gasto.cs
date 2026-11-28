using System.ComponentModel.DataAnnotations;

namespace GestorIngresosGastos.Models
{
    public class Gasto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; } = string.Empty;

        [Range(0.01, 999999999, ErrorMessage = "El monto debe ser mayor a 0")]
        public decimal Monto { get; set; }

        [Required]
        public string TipoGasto { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Categoría es obligatoria")]
        public string Categoria { get; set; } = string.Empty;

        public DateTime Fecha { get; set; } = DateTime.Now;

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
