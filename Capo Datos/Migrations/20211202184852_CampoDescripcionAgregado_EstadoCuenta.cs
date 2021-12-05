using Microsoft.EntityFrameworkCore.Migrations;

namespace Capo_Datos.Migrations
{
    public partial class CampoDescripcionAgregado_EstadoCuenta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "EstadoCuentas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "EstadoCuentas");
        }
    }
}
