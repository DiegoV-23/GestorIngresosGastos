using Microsoft.EntityFrameworkCore;
using GestorIngresosGastos.Models;

namespace GestorIngresosGastos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Gasto> Gastos { get; set; }

        public DbSet<Ingreso> Ingresos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Nombre = "Administrador", Correo = "admin@correo.com", Password = "1234" }
            );

            modelBuilder.Entity<Gasto>().HasData(
                new Gasto { Id = 1, Descripcion = "Internet y Luz", Monto = 3800m, TipoGasto = "Fijo", Categoria = "Servicios", Fecha = new DateTime(2026, 11, 27), UsuarioId = 1}
            );

            modelBuilder.Entity<Ingreso>().HasData(
               new Ingreso { Id = 1, Descripcion = "Salario Mensual", Monto = 35000m, TipoIngreso = "Activo", Fuente = "Empleo Principal", Fecha = new DateTime(2026, 11, 27), UsuarioId = 1 }
           );
        }
    }
}
