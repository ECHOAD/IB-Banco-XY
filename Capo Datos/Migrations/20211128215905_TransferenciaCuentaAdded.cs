using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Capo_Datos.Migrations
{
    public partial class TransferenciaCuentaAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transferencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_CuentaOrigen = table.Column<int>(type: "int", nullable: false),
                    Id_CuentaDestino = table.Column<int>(type: "int", nullable: false),
                    MontoActual = table.Column<double>(type: "float", nullable: false),
                    FechaRealizada = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transferencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transferencias_CuentasAhorro_Id_CuentaDestino",
                        column: x => x.Id_CuentaDestino,
                        principalTable: "CuentasAhorro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction,
                        onUpdate:ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Transferencias_CuentasAhorro_Id_CuentaOrigen",
                        column: x => x.Id_CuentaOrigen,
                        principalTable: "CuentasAhorro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction,
                        onUpdate: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_Id_CuentaDestino",
                table: "Transferencias",
                column: "Id_CuentaDestino");

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_Id_CuentaOrigen",
                table: "Transferencias",
                column: "Id_CuentaOrigen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transferencias");
        }
    }
}
