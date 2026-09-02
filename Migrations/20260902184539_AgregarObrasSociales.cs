using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AtencionesApp.Migrations
{
    /// <inheritdoc />
    public partial class AgregarObrasSociales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ObraSocialId",
                table: "Pacientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ObrasSociales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObrasSociales", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "ObrasSociales",
                columns: new[] { "Id", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "(PAMI) INSTITUTO NACIONAL DE SERVICIOS SOCIALES PARA JUBILADOS Y PENSIONADOS" },
                    { 2, false, "(DOSEP) O.S.P. SAN LUIS" },
                    { 3, false, "(OSFATUN) OBRA SOCIAL DE LA FEDERACION ARGENTINA DEL TRABAJADOR DE LAS UNIVERSIDADES NACIONALES" },
                    { 4, false, "Programa Sumar San Luis" },
                    { 5, false, "(OSECAC) OBRA SOCIAL DE LOS EMPLEADOS DE COMERCIO Y ACTIVIDADES CIVILES" },
                    { 6, false, "(OSPM) OBRA SOCIAL DEL PERSONAL MOSAISTA" },
                    { 7, false, "(OSFATLYF) OBRA SOCIAL DE LA FEDERACION ARGENTINA DE TRABAJADORES DE LUZ Y FUERZA" },
                    { 8, false, "O.S.P. BUENOS AIRES (IOMA)" },
                    { 9, false, "O.S.P. SANTA CRUZ" },
                    { 10, false, "(OSCE) OBRA SOCIAL DE CERAMISTAS" },
                    { 11, false, "O.S.P. SAN JUAN" },
                    { 12, false, "OBRA SOCIAL CONDUCTORES DE TRANSPORTE COLECTIVO DE PASAJEROS" },
                    { 13, false, "O.S.P. MENDOZA" },
                    { 14, false, "OBRA SOCIAL DE TRABAJADORES SOCIOS DE LA ASOCIACION MUTUAL DEL PERSONAL JERARQUICO DE BANCOS OFICIALES NACIONALES-JERARQUICOS SALUD-" },
                    { 15, false, "(OSMATA) OBRA SOCIAL DEL SINDICATO DE MECANICOS Y AFINES DEL TRANSPORTE AUTOMOTOR" },
                    { 16, false, "(OSVVRA) OBRA SOCIAL DE VIAJANTES VENDEDORES DE LA REPUBLICA ARGENTINA. (ANDAR)" },
                    { 17, false, "(OSPEDYC) OBRA SOCIAL DEL PERSONAL DE ENTIDADES DEPORTIVAS Y CIVILES" },
                    { 18, false, "OBRA SOCIAL DE GUINCHEROS Y MAQUINISTAS DE GRUAS MOVILES" },
                    { 19, false, "(OSCOEMA) OBRA SOCIAL DE LA CONFEDERACION DE OBREROS Y EMPLEADOS MUNICIPALES ARGENTINA ( OSCOEMA )" },
                    { 20, false, "(O.S.PERS.A.A.M.S.) OBRA SOCIAL DEL PERSONAL ASOCIADO A ASOCIACION MUTUAL SANCOR" },
                    { 21, false, "(OSPIA) OBRA SOCIAL DEL PERSONAL DE LA INDUSTRIA DE LA ALIMENTACION" },
                    { 22, false, "O.S.P. CATAMARCA (OSEP)" },
                    { 23, false, "(OSMEDICA) OBRA SOCIAL DE LOS MEDICOS DE LA CIUDAD DE BUENOS AIRES" },
                    { 24, false, "O.S.P. RIO NEGRO (IPROSS)" },
                    { 25, false, "(OSDE) OBRA SOCIAL DE EJECUTIVOS Y DEL PERSONAL DE DIRECCION DE EMPRESAS" },
                    { 26, false, "(OSIM) OBRA SOCIAL DEL PERSONAL DE DIRECCION DE LA INDUSTRIA METALURGICAY DEMAS ACTIVIDADES EMPRESARIAS" },
                    { 27, false, "(OSUTHGRA) OBRA SOCIAL DE LA UNION DE TRABAJADORES DEL TURISMO, HOTELEROS Y GASTRONOMICOS DE LA REPUBLICA ARGENTINA" },
                    { 28, false, "(ASE) OBRA SOCIAL DEL PERSONAL DE DIRECCION ACCION SOCIAL DE EMPRESARIOS (A:S:E.)" },
                    { 29, false, "(DOSPU) Dirección de Obra Social para el Personal Universitario" },
                    { 30, false, "(OSPCN) OBRA SOCIAL UNION PERSONAL DE LA UNION DEL PERSONAL CIVIL DE LA NACION" },
                    { 31, false, "(OSPECON) OBRA SOCIAL DEL PERSONAL DE LA CONSTRUCCION" },
                    { 32, false, "(OSPAT) OBRA SOCIAL DEL PERSONAL DE LA ACTIVIDAD DEL TURF" },
                    { 33, false, "(OSME) OBRA SOCIAL PARA EL PERSONAL DEL MINISTERIO DE ECONOMIA Y DE OBRAS Y SERVICIOS PUBLICOS" },
                    { 34, false, "(OSTES) OBRA SOCIAL DE TRABAJADORES DE ESTACIONES DE SERVICIO" },
                    { 35, false, "(OSPRERA) OBRA SOCIAL DEL PERSONAL RURAL Y ESTIBADORES DE LA REPUBLICA ARGENTINA" },
                    { 36, false, "ASOCIACIÓN MUTUAL MÉDICA VILLA MARÍA" },
                    { 37, false, "GALENO ARGENTINA S.A." },
                    { 38, false, "MEDIFÉ ASOCIACIÓN CIVIL" },
                    { 39, false, "(OSPAP) OBRA SOCIAL DEL PERSONAL DE LA ACTIVIDAD PERFUMISTA" },
                    { 40, false, "(OSSEG) OBRA SOCIAL DE LA ACTIVIDAD DE SEGUROS, REASEGUROS, CAPITALIZACION Y AHORRO Y PRESTAMO PARA LA VIVIENDA" },
                    { 41, false, "(OSPACA) OBRA SOCIAL DEL PERSONAL DEL AUTOMOVIL CLUB ARGENTINO" },
                    { 42, false, "(OSTPBA) OBRA SOCIAL DE TRABAJADORES DE PRENSA DE BUENOS AIRES" },
                    { 43, false, "(OSPLAD) OBRA SOCIAL PARA LA ACTIVIDAD DOCENTE" },
                    { 44, false, "(OSMMEDT) OBRA SOCIAL DE MANDOS MEDIOS DE TELECOMUNICACIONES EN LA REPUBLICA ARGENTINA Y MERCOSUR" },
                    { 45, false, "(OSPESGYPE) OBRA SOCIAL DEL PERSONAL DE ESTACIONES DE SERVICIO, GARAGES, PLAYAS DE ESTACIONAMIENTO Y LAVADEROS AUTOMATICOS" },
                    { 46, false, "OBRA SOCIAL DE CONDUCTORES CAMIONEROS Y PERSONAL DEL TRANSPORTE AUTOMOTOR DE CARGAS" },
                    { 47, false, "(OSMISS) OBRA SOCIAL DE MINISTROS, SECRETARIOS Y SUBSECRETARIOS" },
                    { 48, false, "(OSPEP) OBRA SOCIAL DEL PERSONAL DE LA ENSEÑANZA PRIVADA" },
                    { 49, false, "(OSPIP) OBRA SOCIAL DEL PERSONAL DE LA INDUSTRIA DEL PLASTICO" },
                    { 50, false, "(IOSFA) INSTITUTO DE OBRA SOCIAL DE LAS FUERZAS ARMADAS" },
                    { 51, false, "(FEDECAMARAS) OBRA SOCIAL DE LA FEDERACION DE CAMARAS Y CENTROS COMERCIALES ZONALES DE LA REPUBLICA ARGENTINA (FEDECAMARAS)," },
                    { 52, false, "(OSPIDA) OBRA SOCIAL DEL PERSONAL DE IMPRENTA, DIARIOS Y AFINES" },
                    { 53, false, "(OSPROTURA) OBRA SOCIAL PROFESIONALES DEL TURF DE LA REPUBLICA ARGENTINA" },
                    { 54, false, "(OSDOP) OBRA SOCIAL DE DOCENTES PARTICULARES" },
                    { 55, false, "(OSALARA) OBRA SOCIAL DE AGENTES DE LOTERIAS Y AFINES DE LA REPUBLICA ARGENTINA" },
                    { 56, false, "(OSPACP) OBRA SOCIAL DEL PERSONAL AUXILIAR DE CASAS PARTICULARES" },
                    { 57, false, "O.S.P. SANTA FE (IAPOSS)" },
                    { 58, false, "OBRA SOCIAL DELPERSONAL DE LA INDUSTRIA GRAFICA DE LA PROVINCIA DE CORDOBA" },
                    { 59, false, "(OSDEPYM) OBRA SOCIAL DE EMPRESARIOS, PROFESIONALES Y MONOTRIBUTISTAS" },
                    { 60, false, "(OSPATCA) OBRA SOCIAL DEL PERSONAL ADMINISTRATIVO Y TECNICO DE LA CONSTRUCCION Y AFINES" },
                    { 61, false, "(OSFE) OBRA SOCIAL FERROVIARIA" },
                    { 62, false, "(UPFPARA) OBRA SOCIAL DEL PERSONAL DE FABRICAS DE PINTURA" },
                    { 63, false, "(OSPIT) OBRA SOCIAL DEL PERSONAL DE LA INDUSTRIA TEXTIL" },
                    { 64, false, "(O.S.A.M.) OBRA SOCIAL DE LA ACTIVIDAD MINERA" },
                    { 65, false, "SWISS MEDICAL S.A." },
                    { 66, false, "O.S.P. CIUDAD AUT. DE BUENOS AIRES OBSBA" },
                    { 67, false, "(OSUOMRA) OBRA SOCIAL DE LA UNION OBRERA METALURGICA DE LA REPUBLICA ARGENTINA" },
                    { 68, false, "(PFA) SUPERINTENDENCIA DEL BIENESTAR POLICIA FEDERAL ARGENTINA" },
                    { 69, false, "PREVENCIÓN SALUD S.A." },
                    { 70, false, "O.S.P. LA RIOJA" },
                    { 71, false, "OBRA SOCIAL DE LA CAMARA DE EMPRESARIOS DE AGENCIAS DE REMISES DE ARGENTINA" },
                    { 72, false, "ASOCIACION MUTUAL SANCOR" },
                    { 73, false, "ASOCIACION MUTUAL DEL PERSONAL JERARQUICO DE BANCOS OFICIALES NACIONALES" },
                    { 74, false, "OBRA SOCIAL PROGRAMAS MEDICOS SOCIEDAD ARGENTINA DE CONSULTORIA MUTUAL" },
                    { 75, false, "ROI SA" },
                    { 76, false, "(OSSIMRA) OBRA SOCIAL DE LOS SUPERVISORES DE LA INDUSTRIA METALMECANICA DE LA REPUBLICA ARGENTINA" },
                    { 77, false, "OBRA SOCIAL DEL PERSONAL DE LA FEDERACION DE SINDICATOS DE LA INDUSTRIA QUIMICA Y PETROQUIMICA" },
                    { 78, false, "CIRCULO MUTUAL DE SUBOFICIALES RETIRADOS DE LA POLICIA FEDERAL ARGENTINA" },
                    { 79, false, "(OSEIV) OBRA SOCIAL DE EMPLEADOS DE LA INDUSTRIA DEL VIDRIO" },
                    { 80, false, "(OSPEDICI) OBRA SOCIAL DEL PERSONAL DE DISTRIBUIDORAS CINEMATOGRAFICAS DE LA R.A." },
                    { 81, false, "(OSPF) OBRA SOCIAL DEL PERSONAL DE FARMACIA" },
                    { 82, false, "(OSITAC) OBRA SOCIAL DE LA INDUSTRIA DEL TRANSPORTEAUTOMOTOR DE CORDOBA" },
                    { 83, false, "(OSETRA) OBRA SOCIAL DE EMPLEADOS DEL TABACO DE LA REPUBLICA ARGENTINA" },
                    { 84, false, "(OSPIV) OBRA SOCIAL DEL PERSONAL DE LA INDUSTRIA DEL VESTIDO" },
                    { 85, false, "(OSPS MERCEDES) OBRA SOCIAL DEL PERSONAL SUPERIOR MERCEDES BENZ ARGENTINA" },
                    { 86, false, "(OSSACRA) OBRA SOCIAL DE LA ASOCIACION CIVIL PROSINDICATO DE AMAS DE CASA DE LA REPUBLICA ARGENTINA" },
                    { 87, false, "(OSEMM) OBRA SOCIAL DE EMPLEADOS DE LA MARINA MERCANTE" },
                    { 88, false, "NOBIS SA" },
                    { 89, false, "AVALIAN SALUD Y BIENESTAR COOPERATIVA LIMITADA EX ACA SALUD COOPERATIVA DE PRESTACION DE SERVICIOS MEDICO ASISTENCIALES LTDA" },
                    { 90, false, "(OSCEP) OBRA SOCIAL DE CAPATACES ESTIBADORES PORTUARIOS" },
                    { 91, false, "O.S.P. CORDOBA (APROSS)" },
                    { 92, false, "ASOCIACION SOCORROS MUTUOS FUERZAS ARMADAS" },
                    { 93, false, "(OSMITA) OBRA SOCIAL MUTUALIDAD INDUSTRIAL TEXTIL ARGENTINA" },
                    { 94, false, "(OSPTV) OBRA SOCIAL DEL PERSONAL DE TELEVISION" },
                    { 95, false, "(OSPSIP) OBRA SOCIAL DEL PERSONAL DE SEGURIDAD COMERCIAL, INDUSTRIAL E INVESTIGACIONES PRIVADAS" },
                    { 96, false, "(O.S.A.M.O.C) OBRA SOCIAL ASOCIACION MUTUAL DE LOS OBREROS CATOLICOS PADRE FEDERICO GROTE" },
                    { 97, false, "MEDICINA PREPAGA HOMINIS S.A." },
                    { 98, false, "OMINT S.A. DE SERVICIOS" },
                    { 99, false, "OBRA SOCIAL DE LA FEDERACION GREMIAL DEL PERSONAL DE LA INDUSTRIA DE LA CARNE Y SUS DERIVADOS" },
                    { 100, false, "(OSPE) OBRA SOCIAL DE PETROLEROS" },
                    { 101, false, "(OSUTI) OBRA SOCIAL DE LA UNION DE TRABAJADORES DEL INSTITUTO NACIONAL DE SERVICIOS SOCIALES PARA JUBILADOS Y PENSIONADOS DE LA REPUBLICA ARGENTINA" },
                    { 102, false, "OBRA SOCIAL DE PATRONES DE CABOTAJE DE RIOS Y PUERTOS" },
                    { 103, false, "BRAMED S.R.L." },
                    { 104, false, "O.S.P. TUCUMAN (IPSST)" },
                    { 105, false, "SCIS" },
                    { 106, false, "(OSPEP) OBRA SOCIAL DEL PERSONAL DE PANADERIAS" },
                    { 107, false, "(OSFFENTOS) OBRA SOCIAL FEDERAL DE LA FEDERACION NACIONAL DE TRABAJADORES DE OBRAS SANITARIAS" },
                    { 108, false, "(OSBA) OBRA SOCIAL SERVICIOS SOCIALES BANCARIOS" },
                    { 109, false, "OBRA SOCIAL DEL PERSONAL JERARQUICO DEL TRANSPORTE AUTOMOTOR DE PASAJEROS DE CORDOBA Y AFINES" },
                    { 110, false, "(OSPADEP) OBRA SOCIAL DEL PERSONAL DE AERONAVEGACION DE ENTES PRIVADOS" },
                    { 111, false, "OBRA SOCIAL DE EMPLEADOS Y PERSONAL JERARQUICODE LA ACTIVIDAD DELNEUMATICO ARGENTINO DE NEUMATICOS GOOD YEAR SRL" },
                    { 112, false, "OBRA SOCIAL DE TRABAJADORES VIALES Y AFINES DE LA REPUBLICA ARGENTINA" },
                    { 113, false, "(OSDIC) OBRA SOCIAL DEL PERSONAL DIRECTIVO DE LA INDUSTRIA DE LA CONSTRUCCION" },
                    { 114, false, "(OSPAGA) OBRA SOCIAL DEL PERSONAL DE AGUAS GASEOSAS Y AFINES" },
                    { 115, false, "(OSPESA) OBRA SOCIAL DEL PERSONAL DE SOCIEDADES DE AUTORES Y AFINES" },
                    { 116, false, "(OSPOCE) OBRA SOCIAL DEL PERSONAL DEL ORGANISMO DE CONTROL EXTERNO" },
                    { 117, false, "(OSPIF) OBRA SOCIAL DEL PERSONAL DE LA INDUSTRIA DEL FOSFORO, ENCENDIDO Y AFINES" },
                    { 118, false, "(OSCHOCA) OBRA SOCIAL DE CHOFERES DE CAMIONES" },
                    { 119, false, "(OSADEF) OBRA SOCIAL DE LAS ASOCIACIONES DE EMPLEADOS DE FARMACIA" },
                    { 120, false, "(OSPERYH) OBRA SOCIAL DEL PERSONAL DE EDIFICIOS DE RENTA Y HORIZONTAL DE LA CIUDAD AUTONOMA DE BUENOS AIRESY GRAN BUENOS AIRES" },
                    { 121, false, "OBRA SOCIAL ACEROS PARANA" },
                    { 122, false, "(OSFYB) OBRA SOCIAL DE FARMACEUTICOS Y BIOQUIMICOS" },
                    { 123, false, "OBRA SOCIAL PARA EL PERSONAL DE EMPRESAS DE LIMPIEZA, SERVICIOS Y MAESTRANZA DE MENDOZA" },
                    { 124, false, "(OSPPRA) OBRA SOCIAL DEL PERSONAL DE PRENSA DE LA REPUBLICA ARGENTINA" },
                    { 125, false, "(OSPES) OBRA SOCIAL PARA EL PERSONAL DE ESTACIONES DE SERVICIO, GARAGES, PLAYAS DE ESTACIONAMIENTO, LAVADEROS AUTOMATICOS Y GOMERIAS DE LA REPUBLICA ARGENTINA" },
                    { 126, false, "(OSPM) OBRA SOCIAL DEL PERSONAL DE MAESTRANZA" },
                    { 127, false, "GALENO CONSULTING GROUP S. A." },
                    { 128, false, "MEDICUS SOCIEDAD ANONIMA DE ASISTENCIA MEDICA Y CIENTIFICA" },
                    { 129, false, "(OSPSA) OBRA SOCIAL DEL PERSONAL DE LA SANIDAD ARGENTINA" },
                    { 130, false, "(OSPA) OBRA SOCIAL DEL PERSONAL AERONAUTICO" },
                    { 131, false, "MUTUAL MEDICA RIO CUARTO" }
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBkMRcWOVFYYfaYYBp2/Fva9YJTM1+ALFHPkkIWOfLlu7IyyMIn21Zl0ascImuXWCg==");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_ObraSocialId",
                table: "Pacientes",
                column: "ObraSocialId");

            migrationBuilder.CreateIndex(
                name: "IX_ObrasSociales_Nombre",
                table: "ObrasSociales",
                column: "Nombre",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_ObrasSociales_ObraSocialId",
                table: "Pacientes",
                column: "ObraSocialId",
                principalTable: "ObrasSociales",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_ObrasSociales_ObraSocialId",
                table: "Pacientes");

            migrationBuilder.DropTable(
                name: "ObrasSociales");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_ObraSocialId",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "ObraSocialId",
                table: "Pacientes");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMduexmbmaQzeVReOtghpa66H8TllOfaJNeLLfYJpmD6KS6mY8oBiJN1FwgFstXc5w==");
        }
    }
}
