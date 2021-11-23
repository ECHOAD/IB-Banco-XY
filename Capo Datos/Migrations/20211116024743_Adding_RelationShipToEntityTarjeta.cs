using Microsoft.EntityFrameworkCore.Migrations;

namespace Capo_Datos.Migrations
{
    public partial class Adding_RelationShipToEntityTarjeta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EstadosCredito_Id_TarjetaCredito",
                table: "EstadosCredito",
                column: "Id_TarjetaCredito");

            migrationBuilder.AddForeignKey(
                name: "FK_EstadosCredito_TarjetasCredito_Id_TarjetaCredito",
                table: "EstadosCredito",
                column: "Id_TarjetaCredito",
                principalTable: "TarjetasCredito",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstadosCredito_TarjetasCredito_Id_TarjetaCredito",
                table: "EstadosCredito");

            migrationBuilder.DropIndex(
                name: "IX_EstadosCredito_Id_TarjetaCredito",
                table: "EstadosCredito");
        }
    }
}
