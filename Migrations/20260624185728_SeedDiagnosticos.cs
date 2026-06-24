using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AtencionesApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedDiagnosticos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Diagnosticos",
                columns: new[] { "Id", "Codigo", "Descripcion", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "K02.0", "Caries limitada al esmalte", false },
                    { 2, "K02.1", "Caries de la dentina", false },
                    { 3, "K02.2", "Caries del cemento", false },
                    { 4, "K02.3", "Caries dental detenida", false },
                    { 5, "K02.9", "Caries dental, no especificada", false },
                    { 6, "K04.0", "Pulpitis", false },
                    { 7, "K04.1", "Necrosis de la pulpa", false },
                    { 8, "K04.4", "Periodontitis apical aguda", false },
                    { 9, "K04.5", "Periodontitis apical crónica", false },
                    { 10, "K04.6", "Absceso periapical con fístula", false },
                    { 11, "K04.7", "Absceso periapical sin fístula", false },
                    { 12, "K05.0", "Gingivitis aguda", false },
                    { 13, "K05.1", "Gingivitis crónica", false },
                    { 14, "K05.2", "Periodontitis aguda", false },
                    { 15, "K05.3", "Periodontitis crónica", false },
                    { 16, "K05.4", "Periodontosis", false },
                    { 17, "K06.0", "Recesión gingival", false },
                    { 18, "K06.1", "Agrandamiento gingival", false },
                    { 19, "K08.1", "Pérdida de dientes por accidente o extracción", false },
                    { 20, "K08.3", "Raíz dental retenida", false },
                    { 21, "K01.0", "Dientes incluidos", false },
                    { 22, "K01.1", "Dientes impactados", false },
                    { 23, "K07.0", "Anomalías del tamaño de los maxilares", false },
                    { 24, "K07.3", "Anomalías de la posición del diente", false },
                    { 25, "K12.0", "Estomatitis aftosa recurrente", false },
                    { 26, "K12.1", "Otras formas de estomatitis", false },
                    { 27, "K13.0", "Enfermedades de los labios", false },
                    { 28, "K13.7", "Otras lesiones y las no especificadas de la mucosa bucal", false },
                    { 29, "Z01.2", "Examen odontológico de rutina", false },
                    { 30, "Z29.8", "Control preventivo y educación en salud bucal", false }
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFxza9sXVz/rkKpanhWsInLH5m21AnbzxV8guCVKavZ8CugC3qxE2ZWBAbQyibr2dg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Diagnosticos",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENzHiYc8JXunDUg5EAxM2IT/+QzSkm03k5pD90ksXwYxKz6Jym/IVzGcllyE6oDpxg==");
        }
    }
}
