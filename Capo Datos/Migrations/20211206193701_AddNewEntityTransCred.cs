using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Capo_Datos.Migrations
{
    public partial class AddNewEntityTransCred : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstadosCredito_CuentasAhorro_CuentaOrigenId",
                table: "EstadosCredito");

            migrationBuilder.DropForeignKey(
                name: "FK_Transferencias_CuentasAhorro_Id_CuentaDestino",
                table: "Transferencias");

            migrationBuilder.DropForeignKey(
                name: "FK_Transferencias_CuentasAhorro_Id_CuentaOrigen",
                table: "Transferencias");

            migrationBuilder.DropIndex(
                name: "IX_EstadosCredito_CuentaOrigenId",
                table: "EstadosCredito");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transferencias",
                table: "Transferencias");

            migrationBuilder.DropColumn(
                name: "CuentaOrigenId",
                table: "EstadosCredito");

            migrationBuilder.RenameTable(
                name: "Transferencias",
                newName: "TransferenciasCuenta");

            migrationBuilder.RenameIndex(
                name: "IX_Transferencias_Id_CuentaOrigen",
                table: "TransferenciasCuenta",
                newName: "IX_TransferenciasCuenta_Id_CuentaOrigen");

            migrationBuilder.RenameIndex(
                name: "IX_Transferencias_Id_CuentaDestino",
                table: "TransferenciasCuenta",
                newName: "IX_TransferenciasCuenta_Id_CuentaDestino");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransferenciasCuenta",
                table: "TransferenciasCuenta",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TransferenciasCredito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_CreditoOrigen = table.Column<int>(type: "int", nullable: false),
                    Id_CuentaDestino = table.Column<int>(type: "int", nullable: false),
                    MontoActual = table.Column<double>(type: "float", nullable: false),
                    FechaRealizada = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferenciasCredito", x => x.Id);

                    table.ForeignKey(
                      name: "FK_TransferenciasCredito_TarjetasCredito_Id_CreditoOrigen",
                      column: x => x.Id_CreditoOrigen,
                      principalTable: "TarjetasCredito",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.NoAction,
                      onUpdate: ReferentialAction.NoAction);

                    table.ForeignKey(
                        name: "FK_TransferenciasCredito_CuentasAhorro_Id_CuentaDestino",
                        column: x => x.Id_CuentaDestino,
                        principalTable: "CuentasAhorro",
                        principalColumn: "Id",
                         onDelete: ReferentialAction.NoAction,
                        onUpdate: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstadosCredito_Id_CuentaOrigen",
                table: "EstadosCredito",
                column: "Id_CuentaOrigen");

            migrationBuilder.CreateIndex(
                name: "IX_TransferenciasCredito_Id_CreditoOrigen",
                table: "TransferenciasCredito",
                column: "Id_CreditoOrigen");

            migrationBuilder.CreateIndex(
                name: "IX_TransferenciasCredito_Id_CuentaDestino",
                table: "TransferenciasCredito",
                column: "Id_CuentaDestino");

            migrationBuilder.AddForeignKey(
                name: "FK_EstadosCredito_CuentasAhorro_Id_CuentaOrigen",
                table: "EstadosCredito",
                column: "Id_CuentaOrigen",
                principalTable: "CuentasAhorro",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction,
                onUpdate: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferenciasCuenta_CuentasAhorro_Id_CuentaDestino",
                table: "TransferenciasCuenta",
                column: "Id_CuentaDestino",
                principalTable: "CuentasAhorro",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction,
                onUpdate: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferenciasCuenta_CuentasAhorro_Id_CuentaOrigen",
                table: "TransferenciasCuenta",
                column: "Id_CuentaOrigen",
                principalTable: "CuentasAhorro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade,
                onUpdate: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstadosCredito_CuentasAhorro_Id_CuentaOrigen",
                table: "EstadosCredito");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferenciasCuenta_CuentasAhorro_Id_CuentaDestino",
                table: "TransferenciasCuenta");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferenciasCuenta_CuentasAhorro_Id_CuentaOrigen",
                table: "TransferenciasCuenta");

            migrationBuilder.DropTable(
                name: "TransferenciasCredito");

            migrationBuilder.DropIndex(
                name: "IX_EstadosCredito_Id_CuentaOrigen",
                table: "EstadosCredito");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransferenciasCuenta",
                table: "TransferenciasCuenta");

            migrationBuilder.RenameTable(
                name: "TransferenciasCuenta",
                newName: "Transferencias");

            migrationBuilder.RenameIndex(
                name: "IX_TransferenciasCuenta_Id_CuentaOrigen",
                table: "Transferencias",
                newName: "IX_Transferencias_Id_CuentaOrigen");

            migrationBuilder.RenameIndex(
                name: "IX_TransferenciasCuenta_Id_CuentaDestino",
                table: "Transferencias",
                newName: "IX_Transferencias_Id_CuentaDestino");

            migrationBuilder.AddColumn<int>(
                name: "CuentaOrigenId",
                table: "EstadosCredito",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transferencias",
                table: "Transferencias",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Transferencias_CuentasAhorro_Id_CuentaDestino",
                table: "Transferencias",
                column: "Id_CuentaDestino",
                principalTable: "CuentasAhorro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transferencias_CuentasAhorro_Id_CuentaOrigen",
                table: "Transferencias",
                column: "Id_CuentaOrigen",
                principalTable: "CuentasAhorro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
