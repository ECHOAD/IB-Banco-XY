using Microsoft.EntityFrameworkCore.Migrations;

namespace Capo_Datos.Migrations
{
    public partial class AgregandoNuevosCampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "numero_tarjetaCredito",
                table: "TarjetasCredito",
                newName: "Numero_tarjetaCredito");

            migrationBuilder.AddColumn<int>(
                name: "CuentaOrigenId",
                table: "EstadosCredito",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id_CuentaOrigen",
                table: "EstadosCredito",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "EstadoCuentas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_EstadosCredito_CuentaOrigenId",
                table: "EstadosCredito",
                column: "CuentaOrigenId");

            migrationBuilder.AddForeignKey(
                name: "FK_EstadosCredito_CuentasAhorro_CuentaOrigenId",
                table: "EstadosCredito",
                column: "CuentaOrigenId",
                principalTable: "CuentasAhorro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstadosCredito_CuentasAhorro_CuentaOrigenId",
                table: "EstadosCredito");

            migrationBuilder.DropIndex(
                name: "IX_EstadosCredito_CuentaOrigenId",
                table: "EstadosCredito");

            migrationBuilder.DropColumn(
                name: "CuentaOrigenId",
                table: "EstadosCredito");

            migrationBuilder.DropColumn(
                name: "Id_CuentaOrigen",
                table: "EstadosCredito");

            migrationBuilder.RenameColumn(
                name: "Numero_tarjetaCredito",
                table: "TarjetasCredito",
                newName: "numero_tarjetaCredito");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "EstadoCuentas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
