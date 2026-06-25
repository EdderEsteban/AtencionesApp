using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtencionesApp.Migrations
{
    /// <inheritdoc />
    public partial class AddOdontogramaEstados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OdontogramaEstados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AtencionOdontologiaId = table.Column<int>(type: "int", nullable: false),
                    NumeroDiente = table.Column<int>(type: "int", nullable: false),
                    Superficie = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdontogramaEstados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OdontogramaEstados_AtencionesOdontologia_AtencionOdontologia~",
                        column: x => x.AtencionOdontologiaId,
                        principalTable: "AtencionesOdontologia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMduexmbmaQzeVReOtghpa66H8TllOfaJNeLLfYJpmD6KS6mY8oBiJN1FwgFstXc5w==");

            migrationBuilder.CreateIndex(
                name: "IX_OdontogramaEstados_AtencionOdontologiaId",
                table: "OdontogramaEstados",
                column: "AtencionOdontologiaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OdontogramaEstados");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFxza9sXVz/rkKpanhWsInLH5m21AnbzxV8guCVKavZ8CugC3qxE2ZWBAbQyibr2dg==");
        }
    }
}
