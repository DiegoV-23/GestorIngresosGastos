using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorIngresosGastos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarIngresos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingresos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Monto = table.Column<decimal>(type: "TEXT", nullable: false),
                    TipoIngreso = table.Column<string>(type: "TEXT", nullable: false),
                    Fuente = table.Column<string>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingresos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingresos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Ingresos",
                columns: new[] { "Id", "Descripcion", "Fecha", "Fuente", "Monto", "TipoIngreso", "UsuarioId" },
                values: new object[] { 1, "Salario Mensual", new DateTime(2026, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Empleo Principal", 35000m, "Activo", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Ingresos_UsuarioId",
                table: "Ingresos",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingresos");
        }
    }
}
