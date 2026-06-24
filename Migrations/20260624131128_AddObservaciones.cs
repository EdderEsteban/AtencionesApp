using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtencionesApp.Migrations
{
    /// <inheritdoc />
    public partial class AddObservaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "AtencionesOdontologia",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "AtencionesEnfermeria",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENzHiYc8JXunDUg5EAxM2IT/+QzSkm03k5pD90ksXwYxKz6Jym/IVzGcllyE6oDpxg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "AtencionesOdontologia");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "AtencionesEnfermeria");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEClNBEXZTqK7e//pk8wfDrdzVh4orG3Py9+EPhs5bPwTQuX35RW0YFOtUK/HoMJSSg==");
        }
    }
}
