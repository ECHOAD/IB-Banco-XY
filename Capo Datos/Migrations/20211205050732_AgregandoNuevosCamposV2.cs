using Microsoft.EntityFrameworkCore.Migrations;

namespace Capo_Datos.Migrations
{
    public partial class AgregandoNuevosCamposV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstadosPrestamo_CuentasAhorro_Id_prestamo",
                table: "EstadosPrestamo");

            migrationBuilder.AddColumn<int>(
                name: "Id_CuentaOrigen",
                table: "EstadosPrestamo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EstadosPrestamo_Id_CuentaOrigen",
                table: "EstadosPrestamo",
                column: "Id_CuentaOrigen");

            migrationBuilder.AddForeignKey(
                name: "FK_EstadosPrestamo_CuentasAhorro_Id_CuentaOrigen",
                table: "EstadosPrestamo",
                column: "Id_CuentaOrigen",
                principalTable: "CuentasAhorro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EstadosPrestamo_Prestamos_Id_prestamo",
                table: "EstadosPrestamo",
                column: "Id_prestamo",
                principalTable: "Prestamos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstadosPrestamo_CuentasAhorro_Id_CuentaOrigen",
                table: "EstadosPrestamo");

            migrationBuilder.DropForeignKey(
                name: "FK_EstadosPrestamo_Prestamos_Id_prestamo",
                table: "EstadosPrestamo");

            migrationBuilder.DropIndex(
                name: "IX_EstadosPrestamo_Id_CuentaOrigen",
                table: "EstadosPrestamo");

            migrationBuilder.DropColumn(
                name: "Id_CuentaOrigen",
                table: "EstadosPrestamo");

            migrationBuilder.AddForeignKey(
                name: "FK_EstadosPrestamo_CuentasAhorro_Id_prestamo",
                table: "EstadosPrestamo",
                column: "Id_prestamo",
                principalTable: "CuentasAhorro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
