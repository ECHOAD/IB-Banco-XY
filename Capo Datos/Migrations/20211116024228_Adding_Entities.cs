using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Capo_Datos.Migrations
{
    public partial class Adding_Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadosCredito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_TarjetaCredito = table.Column<int>(type: "int", nullable: false),
                    Pagado = table.Column<double>(type: "float", nullable: false),
                    Disponible = table.Column<double>(type: "float", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosCredito", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadosPrestamo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_prestamo = table.Column<int>(type: "int", nullable: false),
                    Monto_pagado = table.Column<double>(type: "float", nullable: false),
                    Monto_restante = table.Column<double>(type: "float", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosPrestamo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstadosPrestamo_CuentasAhorro_Id_prestamo",
                        column: x => x.Id_prestamo,
                        principalTable: "CuentasAhorro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prestamos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo_Prestamo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance_Apertura = table.Column<double>(type: "float", nullable: false),
                    Total_Pagado = table.Column<double>(type: "float", nullable: false),
                    Id_Usuario = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prestamos_AspNetUsers_Id_Usuario",
                        column: x => x.Id_Usuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TarjetasCredito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero_tarjetaCredito = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_usuario = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    Balance_disponible = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarjetasCredito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TarjetasCredito_AspNetUsers_Id_usuario",
                        column: x => x.Id_usuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstadosPrestamo_Id_prestamo",
                table: "EstadosPrestamo",
                column: "Id_prestamo");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_Id_Usuario",
                table: "Prestamos",
                column: "Id_Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_TarjetasCredito_Id_usuario",
                table: "TarjetasCredito",
                column: "Id_usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstadosCredito");

            migrationBuilder.DropTable(
                name: "EstadosPrestamo");

            migrationBuilder.DropTable(
                name: "Prestamos");

            migrationBuilder.DropTable(
                name: "TarjetasCredito");
        }
    }
}
