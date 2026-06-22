using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtencionesApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsuarioAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Apellido", "Email", "IsDeleted", "Nombre", "PasswordHash", "RolId", "Telefono" },
                values: new object[] { 1, "Sistema", "admin@salud.com", false, "Admin", "AQAAAAIAAYagAAAAEClNBEXZTqK7e//pk8wfDrdzVh4orG3Py9+EPhs5bPwTQuX35RW0YFOtUK/HoMJSSg==", 1, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
