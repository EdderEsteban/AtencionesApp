using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtencionesApp.Migrations
{
    /// <inheritdoc />
    public partial class MigrarObraSocialDePaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // La columna ObraSocialId ya existe (la creó EF por convención al
            // declarar la colección de navegación en ObraSocial). Acá solo se
            // convierten los datos y recién después se elimina la columna vieja.
            // El orden importa: cada paso toca únicamente los pacientes que los
            // anteriores no resolvieron.

            // Paso A — coincidencia por sigla. Los datos cargados a mano suelen
            // tener la sigla sola ("PAMI") mientras el padrón la trae como
            // prefijo entre paréntesis ("(PAMI) INSTITUTO NACIONAL DE...").
            migrationBuilder.Sql(@"
                UPDATE Pacientes p
                JOIN ObrasSociales o
                  ON UPPER(o.Nombre) LIKE CONCAT('(', UPPER(TRIM(p.ObraSocial)), ')%')
                SET p.ObraSocialId = o.Id
                WHERE p.ObraSocialId IS NULL
                  AND p.ObraSocial IS NOT NULL AND TRIM(p.ObraSocial) <> '';
            ");

            // Paso B — coincidencia por nombre completo normalizado.
            migrationBuilder.Sql(@"
                UPDATE Pacientes p
                JOIN ObrasSociales o ON UPPER(TRIM(o.Nombre)) = UPPER(TRIM(p.ObraSocial))
                SET p.ObraSocialId = o.Id
                WHERE p.ObraSocialId IS NULL
                  AND p.ObraSocial IS NOT NULL AND TRIM(p.ObraSocial) <> '';
            ");

            // Paso C — lo que no coincidió con nada se da de alta, para no
            // perder lo ya registrado. El GROUP BY evita insertar dos veces
            // variantes que solo difieren en mayúsculas o espacios.
            migrationBuilder.Sql(@"
                INSERT INTO ObrasSociales (Nombre, IsDeleted)
                SELECT MIN(TRIM(p.ObraSocial)), 0
                FROM Pacientes p
                WHERE p.ObraSocialId IS NULL
                  AND p.ObraSocial IS NOT NULL AND TRIM(p.ObraSocial) <> ''
                GROUP BY UPPER(TRIM(p.ObraSocial));
            ");

            // Paso D — enlaza los recién dados de alta.
            migrationBuilder.Sql(@"
                UPDATE Pacientes p
                JOIN ObrasSociales o ON UPPER(TRIM(o.Nombre)) = UPPER(TRIM(p.ObraSocial))
                SET p.ObraSocialId = o.Id
                WHERE p.ObraSocialId IS NULL
                  AND p.ObraSocial IS NOT NULL AND TRIM(p.ObraSocial) <> '';
            ");

            migrationBuilder.DropColumn(
                name: "ObraSocial",
                table: "Pacientes");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEIMT/6Yh19Xgm7OpMNQLydTmQw5WfKEx59Lgd2RRgg3i+ot14yAgDjpyD2gib/gkRA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ObraSocial",
                table: "Pacientes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            // Restituye el texto a partir de la clave foránea, para que revertir
            // esta migración no pierda la obra social de ningún paciente.
            migrationBuilder.Sql(@"
                UPDATE Pacientes p
                JOIN ObrasSociales o ON o.Id = p.ObraSocialId
                SET p.ObraSocial = o.Nombre
                WHERE p.ObraSocialId IS NOT NULL;
            ");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBkMRcWOVFYYfaYYBp2/Fva9YJTM1+ALFHPkkIWOfLlu7IyyMIn21Zl0ascImuXWCg==");
        }
    }
}
