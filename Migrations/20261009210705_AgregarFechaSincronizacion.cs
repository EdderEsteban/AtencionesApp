using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtencionesApp.Migrations
{
    /// <inheritdoc />
    public partial class AgregarFechaSincronizacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSincronizacion",
                table: "AtencionesOdontologia",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSincronizacion",
                table: "AtencionesEnfermeria",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaSincronizacion",
                table: "AtencionesOdontologia");

            migrationBuilder.DropColumn(
                name: "FechaSincronizacion",
                table: "AtencionesEnfermeria");
        }
    }
}
