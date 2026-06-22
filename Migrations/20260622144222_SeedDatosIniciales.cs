using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AtencionesApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatosIniciales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Instituciones",
                columns: new[] { "Id", "CodigoSISA", "Dependencia", "IsDeleted", "Nombre", "SitioWeb", "Tipologia" },
                values: new object[,]
                {
                    { 1, "10740072180010", "Provincial", false, "HOSPITAL CANDELARIA", null, "ESCIG" },
                    { 2, "10740072180014", "Provincial", false, "HOSPITAL JUAN KLIPSONS DE LUJAN", null, "ESCIG" },
                    { 3, "10740072180020", "Provincial", false, "HOSPITAL QUINES", null, "ESCIG" },
                    { 4, "10740072180021", "Provincial", false, "HOSPITAL SAN FRANCISCO", null, "ESCIG" },
                    { 5, "10740142180026", "Provincial", false, "HOSPITAL VILLA GENERAL ROCA", null, "ESCIG" },
                    { 6, "10740212180016", "Provincial", false, "HOSPITAL LA TOMA", null, "ESCIG" },
                    { 7, "10740212181000", "Provincial", false, "HOSPITAL DR. RENE FAVALORO (EL TRAPICHE)", null, "ESCIG" },
                    { 8, "10740282180011", "Provincial", false, "HOSPITAL REGIONAL CONCARAN DR. JOSE VELASCO", null, "ESCIG" },
                    { 9, "10740282180018", "Provincial", false, "HOSPITAL NASCHEL", null, "ESCIG" },
                    { 10, "10740282180024", "Provincial", false, "HOSPITAL TILISARAO", null, "ESCIG" },
                    { 11, "10740352180015", "Provincial", false, "HOSPITAL JUSTO DARACT", null, "ESCIG" },
                    { 12, "10740352180027", "Provincial", false, "HOSPITAL JUAN DOMINGO PERON", null, "ESCIG" },
                    { 13, "10740352181045", "Provincial", false, "HOSPITAL VICEINTENDENTA VERONICA BAILONE", "www.hospitalbailone.dosep.sanluis.gob.ar", "ESCIG" },
                    { 14, "10740352380028", "Privado", false, "SANATORIO DE LA MERCED S.R.L.", null, "ESCIG" },
                    { 15, "10740352380999", "Privado", false, "HOSPITAL PRIVADO DE LA VILLA", null, "ESCIG" },
                    { 16, "10740422180007", "Provincial", false, "HOSPITAL ANCHORENA", null, "ESCIG" },
                    { 17, "10740422180008", "Provincial", false, "HOSPITAL ARIZONA", null, "ESCIG" },
                    { 18, "10740422180009", "Provincial", false, "HOSPITAL BUENA ESPERANZA", null, "ESCIG" },
                    { 19, "10740422180013", "Provincial", false, "HOSPITAL MONI HAYMOFF DE FORTUNA", null, "ESCIG" },
                    { 20, "10740422180025", "Provincial", false, "HOSPITAL UNION", null, "ESCIG" },
                    { 21, "10740422180948", "Provincial", false, "HOSPITAL PAULINA BUSSETTI DE MISA - NUEVA GALIA", null, "ESCIG" },
                    { 22, "10740492180012", "Provincial", false, "HOSPITAL DE SANTA ROSA", null, "ESCIG" },
                    { 23, "10740492180017", "Provincial", false, "HOSPITAL MERLO", null, "ESCIG" },
                    { 24, "10740492180907", "Provincial", false, "HOSPITAL MADRE CATALINA RODRIGUEZ", null, "ESCIG" },
                    { 25, "10740562180022", "Provincial", false, "HOSPITAL PEDIATRICO", null, "ESCIEP" },
                    { 26, "10740562180279", "Provincial", false, "HOSPITAL DR.JUAN GREGORIO VIVAS  JUANA KOSLAY", null, "ESCIG" },
                    { 27, "10740562181040", "Provincial", false, "HOSPITAL CENTRAL RAMON CARRILLO", null, "ESCIG" },
                    { 28, "10740562380004", "Privado", false, "CLINICA PRIVADA ITALIA S.R.L", null, "ESCIG" },
                    { 29, "10740562380029", "Privado", false, "SANATORIO RAMOS MEJIA S.R.L", null, "ESCIG" },
                    { 30, "10740562380030", "Privado", false, "SANATORIO Y CLINICA RIVADAVIA SACAYS", null, "ESCIG" },
                    { 31, "10740632180023", "Provincial", false, "HOSPITAL SAN MARTIN", null, "ESCIG" },
                    { 32, "12740352181097", "Provincial", false, "MATERNIDAD PROVINCIAL CARLOS ALBERTO LUCO", null, "ESCIEM" },
                    { 33, "12740352380032", "Privado", false, "CLINICA DEL NIÑO", null, "ESCIEM" },
                    { 34, "12740562180352", "Provincial", false, "MATERNIDAD PROVINCIAL DOCTORA TERESITA BAIGORRIA", null, "ESCIEM" },
                    { 35, "12740562380258", "Privado", false, "CLINICA Y MATERNIDAD CERHU S.R.L.", null, "ESCIEM" },
                    { 36, "13740562180482", "Provincial", false, "HOSPITAL  DE SALUD MENTAL", null, "ESCIESM" },
                    { 37, "14740282380809", "Privado", false, "SANATORIO LOS PASOS S.A.", null, "ESCIE" },
                    { 38, "14740352380037", "Privado", false, "CLINICA DEL ACONCAGUA", null, "ESCIE" },
                    { 39, "14740352380038", "Privado", false, "CLINICA S.T.I.A.", null, "ESCIE" },
                    { 40, "14740352380040", "Privado", false, "INSTITUTO CARDIOVASCULAR VILLA MERCEDES", null, "ESCIE" },
                    { 41, "14740562180947", "Provincial", false, "CENTRO ONCOLOGICO INTEGRAL DE SAN LUIS", null, "ESCIE" },
                    { 42, "14740562381029", "Privado", false, "ESPAÑA INTERNACION EN REHABILITACION", null, "ESCIE" },
                    { 43, "14740562381126", "Privado", false, "INTEGRA SERVICIOS MEDICOS", null, "ESCIE" },
                    { 44, "14740563081006", "Otros", false, "CENTRO DE REHABILITACION COMUNIDAD TERAPEUTICA LEVANTATE Y ANDA", null, "ESCIE" },
                    { 45, "15740282381022", "Privado", false, "HOGAR DE ANCIANOS SAN JOAQUIN SANTA ANA", null, "ESCIRES" },
                    { 46, "15740352381026", "Privado", false, "RESIDENCIA PARA ADULTOS MAYORES SAN GABRIEL", null, "ESCIRES" },
                    { 47, "15740352381043", "Privado", false, "RESIDENCIA PARA ADULTOS MAYORES SANTA TERESITA", null, "ESCIRES" },
                    { 48, "15740562181044", "Provincial", false, "CENTRO INTEGRAL  DEL ADULTO MAYOR SAN VICENTE DE PAUL", null, "ESCIRES" },
                    { 49, "15740562380533", "Privado", false, "RESIDENCIA LOS JAZMINES", null, "ESCIRES" },
                    { 50, "15740562380662", "Privado", false, "RESIDENCIA DE ADULTOS MAYORES LAS MAGNOLIAS", null, "ESCIRES" },
                    { 51, "15740562380731", "Privado", false, "RESIDENCIA HOGAR SAN JORGE", null, "ESCIRES" },
                    { 52, "15740562381020", "Privado", false, "RESIDENCIA PARA MAYORES AYUN", null, "ESCIRES" },
                    { 53, "15740562381021", "Privado", false, "RESIDENCIA PARA ADULTOS MAYORES RENACER II", null, "ESCIRES" },
                    { 54, "15740562381023", "Privado", false, "RESIDENCIA PARA ADULTOS MAYORES", null, "ESCIRES" },
                    { 55, "15740562381024", "Privado", false, "HOGAR DE ANCIANOS SAN ANTONIO", null, "ESCIRES" },
                    { 56, "15740562381025", "Privado", false, "RESIDENCIA DE ADULTOS MAYORES LOS OLIVOS", null, "ESCIRES" },
                    { 57, "15740562381027", "Privado", false, "RESIDENCIA DE ADULTOS MAYORES AMANECERES", null, "ESCIRES" },
                    { 58, "15740562381028", "Privado", false, "RESIDENCIA PARA ADULTOS MAYORES LOS ALMENDROS", null, "ESCIRES" },
                    { 59, "15740562381041", "Privado", false, "MIS AMORES I", null, "ESCIRES" },
                    { 60, "15740562381042", "Privado", false, "RESIDENCIA PARA ADULTOS MAYORES HOGAR SAN BENITO", null, "ESCIRES" },
                    { 61, "17740352381105", "Privado", false, "RESIDENCIA PARA ADULTOS MAYORES SAN AGUSTIN", null, "ESCIRES" },
                    { 62, "50740072180053", "Provincial", false, "CAPS BALDE ESCUDERO", null, "ESSIDT" },
                    { 63, "50740072180077", "Provincial", false, "CAPS EL ALGARROBAL", null, "ESSIDT" },
                    { 64, "50740072180079", "Provincial", false, "CAPS EL CADILLO", null, "ESSIDT" },
                    { 65, "50740072180080", "Provincial", false, "CAPS EL CALDEN", null, "ESSIDT" },
                    { 66, "50740072180085", "Provincial", false, "CAPS EL RETAMO", null, "ESSIDT" },
                    { 67, "50740072180098", "Provincial", false, "CAPS LA BOTIJA", null, "ESSIDT" },
                    { 68, "50740072180101", "Provincial", false, "CAPS LA MAJADA", null, "ESSIDT" },
                    { 69, "50740072180113", "Provincial", false, "CAPS LAS PALOMAS", null, "ESSIDT" },
                    { 70, "50740072180115", "Provincial", false, "CAPS LEANDRO N. ALEM CAPS 21", null, "ESSIDT" },
                    { 71, "50740072180149", "Provincial", false, "CAPS RIO JUAN GOMEZ", null, "ESSIDT" },
                    { 72, "50740072180155", "Provincial", false, "CAPS SAN MIGUEL", null, "ESSIDT" },
                    { 73, "50740072180159", "Provincial", false, "CAPS SANTA ROSA DEL CANTANTAL", null, "ESSIDT" },
                    { 74, "50740072180260", "Provincial", false, "CAPS LA AVENENCIA", null, "ESSIDT" },
                    { 75, "50740072180266", "Provincial", false, "CAPS LA REPRESITA", null, "ESSIDT" },
                    { 76, "50740072181050", "Provincial", false, "CAPS HUARPES", null, "ESSIDT" },
                    { 77, "50740072181051", "Provincial", false, "CAPS BANDA ESTE", null, "ESSIDT" },
                    { 78, "50740072380683", "Privado", false, "CONSULTORIO MEDICO DR. DI FRANCO ALBERTO", null, "ESSIDT" },
                    { 79, "50740072380745", "Privado", false, "CONSULTORIOS MEDICOS CENTRO DE LA VISION", null, "ESSIDT" },
                    { 80, "50740072380748", "Privado", false, "CONSULTORIO MEDICO CANELO", null, "ESSIDT" },
                    { 81, "50740072380749", "Privado", false, "CONSULTORIO MEDICO Y DE ODONTOLOGIA ORTIZ", null, "ESSIDT" },
                    { 82, "50740142180047", "Provincial", false, "CAPS ARBOL SOLO", null, "ESSIDT" },
                    { 83, "50740142180061", "Provincial", false, "CAPS CABEZA DE VACA", null, "ESSIDT" },
                    { 84, "50740142180092", "Provincial", false, "CAPS GENERAL ROCA ( LOS MANANTIALES)", null, "ESSIDT" },
                    { 85, "50740142180118", "Provincial", false, "CAPS Nº 46 LOS CHAÑARES", null, "ESSIDT" },
                    { 86, "50740142180128", "Provincial", false, "CAPS Nº 36 - LA CALERA", null, "ESSIDT" },
                    { 87, "50740142180142", "Provincial", false, "CAPS POZO DEL TALA", null, "ESSIDT" },
                    { 88, "50740142180152", "Provincial", false, "CAPS SAN ANTONIO (DEPARTAMENTO BELGRANO)", null, "ESSIDT" },
                    { 89, "50740142180160", "Provincial", false, "CAPS TORO NEGRO", null, "ESSIDT" },
                    { 90, "50740142180165", "Provincial", false, "CAPS VILLA DE LA QUEBRADA", null, "ESSIDT" },
                    { 91, "50740142180178", "Provincial", false, "HOSPITAL  NOGOLI", null, "ESSIDT" },
                    { 92, "50740142181053", "Provincial", false, "CS REPRESA DEL CARMEN", null, "ESSIDT" },
                    { 93, "50740142181054", "Provincial", false, "CS SAN PEDRO", null, "ESSIDT" },
                    { 94, "50740212180064", "Provincial", false, "CAPS CAÑADA HONDA DE GUZMAN", null, "ESSIDT" },
                    { 95, "50740212180065", "Provincial", false, "CAPS CAROLINA", null, "ESSIDT" },
                    { 96, "50740212180067", "Provincial", false, "CAPS CERROS LARGOS", null, "ESSIDT" },
                    { 97, "50740212180078", "Provincial", false, "CAPS EL ARENAL", null, "ESSIDT" },
                    { 98, "50740212180082", "Provincial", false, "CAPS EL DURAZNO DR. RAMON CARRILLO", null, "ESSIDT" },
                    { 99, "50740212180091", "Provincial", false, "CAPS FRAGA", null, "ESSIDT" },
                    { 100, "50740212180100", "Provincial", false, "CAPS LA FLORIDA", null, "ESSIDT" },
                    { 101, "50740212180104", "Provincial", false, "CAPS LA TOTORA", null, "ESSIDT" },
                    { 102, "50740212180150", "Provincial", false, "CAPS N ° 77 SALADILLO", null, "ESSIDT" },
                    { 103, "50740212180161", "Provincial", false, "CAPS  EL TRAPICHE", null, "ESSIDT" },
                    { 104, "50740212180276", "Provincial", false, "CAPS ESTANCIA GRANDE", null, "ESSIDT" },
                    { 105, "50740212380751", "Privado", false, "CONSULTORIO MEDICO AVANZINI", null, "ESSIDT" },
                    { 106, "50740212380752", "Privado", false, "CONSULTORIO ODONTOLOGICO OD. DARDO PIZARRO", null, "ESSIDT" },
                    { 107, "50740212380804", "Privado", false, "CONSULTORIO MEDICO DR. NORA ALLENDE", null, "ESSIDT" },
                    { 108, "50740212380806", "Privado", false, "CONSULTORIO DR ROCHA", null, "ESSIDT" },
                    { 109, "50740212380808", "Privado", false, "CENTRO MEDICO CLINICA LA TOMA", null, "ESSIDT" },
                    { 110, "50740282180070", "Provincial", false, "CAPS CORTADERAS", null, "ESSIDT" },
                    { 111, "50740282180099", "Provincial", false, "CAPS LA ESQUINA", null, "ESSIDT" },
                    { 112, "50740282180108", "Provincial", false, "CAPS LAGUNA", null, "ESSIDT" },
                    { 113, "50740282180137", "Provincial", false, "CAPS PAPAGAYOS", null, "ESSIDT" },
                    { 114, "50740282180146", "Provincial", false, "CAPS RENCA", null, "ESSIDT" },
                    { 115, "50740282180153", "Provincial", false, "CAPS SAN FELIPE", null, "ESSIDT" },
                    { 116, "50740282180156", "Provincial", false, "CAPS SAN PABLO", null, "ESSIDT" },
                    { 117, "50740282180167", "Provincial", false, "CAPS VILLA DEL CARMEN", null, "ESSIDT" },
                    { 118, "50740282180168", "Provincial", false, "CAPS VILLA LARCA", null, "ESSIDT" },
                    { 119, "50740282180270", "Provincial", false, "CAPS LOS LOBOS", null, "ESSIDT" },
                    { 120, "50740282380753", "Privado", false, "CENTRO MEDICO Y DE REHABILITACION NASCHEL", null, "ESSIDT" },
                    { 121, "50740282380754", "Privado", false, "CENTRO INTEGRAL DE SALUD Y EDUCACION CISE", null, "ESSIDT" },
                    { 122, "50740282380810", "Privado", false, "CONSULTORIO MEDICO DR. PONCE", null, "ESSIDT" },
                    { 123, "50740282380811", "Privado", false, "CONSULTORIO ODONTOLOGICO PABLO ROSSETTI", null, "ESSIDT" },
                    { 124, "50740282380812", "Privado", false, "CENTRO MEDICO UNISOL SALUD", null, "ESSIDT" },
                    { 125, "50740282380813", "Privado", false, "CONSULTORIO ODONTOLOGICO ODONTOLOGIA MSC", null, "ESSIDT" },
                    { 126, "50740282380814", "Privado", false, "CONSULTORIO ODONTOLOGICO PABLO ROSETTI", null, "ESSIDT" },
                    { 127, "50740282380816", "Privado", false, "CONSULTORIO ODONTOLOGICO RAUL ALBERTO BERTOME", null, "ESSIDT" },
                    { 128, "50740282380953", "Privado", false, "PASTORE, ALICIA ROSA", null, "ESSIDT" },
                    { 129, "50740282380964", "Privado", false, "CONSULTORIO ODONTOLOGICO MARISA LOPEZ RUIZ", null, "ESSIDT" },
                    { 130, "50740352180044", "Provincial", false, "CAPS ALBERT SABIN", null, "ESSIDT" },
                    { 131, "50740352180056", "Provincial", false, "CAPS BARRIO BELGRANO", null, "ESSIDT" },
                    { 132, "50740352180074", "Provincial", false, "CAPS DR. HUGO ESPINOSA", null, "ESSIDT" },
                    { 133, "50740352180076", "Provincial", false, "CAPS DR. TALLAFERRO - BARRIO EL CRIOLLO", null, "ESSIDT" },
                    { 134, "50740352180083", "Provincial", false, "CAPS SAN JOSE DEL MORRO", null, "ESSIDT" },
                    { 135, "50740352180094", "Provincial", false, "CAPS JUAN JORBA", null, "ESSIDT" },
                    { 136, "50740352180095", "Provincial", false, "CAPS JUAN LLERENA", null, "ESSIDT" },
                    { 137, "50740352180103", "Provincial", false, "CAPS LA PUNILLA", null, "ESSIDT" },
                    { 138, "50740352180112", "Provincial", false, "CAPS LAS MIRANDAS", null, "ESSIDT" },
                    { 139, "50740352180114", "Provincial", false, "CAPS LAVAISSE", null, "ESSIDT" },
                    { 140, "50740352180116", "Provincial", false, "CAPS LLORENTE RUIZ", null, "ESSIDT" },
                    { 141, "50740352180147", "Provincial", false, "CAPS RENE FAVALORO (EX PIMPOLLO)", null, "ESSIDT" },
                    { 142, "50740352180151", "Provincial", false, "CAPS SAN ANTONIO", null, "ESSIDT" },
                    { 143, "50740352180162", "Provincial", false, "CAPS ATE II", null, "ESSIDT" },
                    { 144, "50740352180164", "Provincial", false, "CAPS VILLA CELESTINA", null, "ESSIDT" },
                    { 145, "50740352180171", "Provincial", false, "CAPS 12 DE OCTUBRE", null, "ESSIDT" },
                    { 146, "50740352180228", "Provincial", false, "HOSPITAL DE DIA JUSTO SUAREZ ROCHA", null, "ESSIDT" },
                    { 147, "50740352180232", "Provincial", false, "HOSPITAL DR. BRAULIO MOYANO", null, "ESSIDT" },
                    { 148, "50740352180275", "Provincial", false, "HOSPITAL DE REFERENCIA EVA PERON", null, "ESSIDT" },
                    { 149, "50740352180280", "Provincial", false, "CAPS Nº 1 JUAN DOMINGO PERON", null, "ESSIDT" },
                    { 150, "50740352180548", "Provincial", false, "CAPS SAN JOSE", null, "ESSIDT" },
                    { 151, "50740352180549", "Provincial", false, "POSTA CIUDAD JARDIN", null, "ESSIDT" },
                    { 152, "50740352181073", "Provincial", false, "CAPS MANUEL BELGRANO", null, "ESSIDT" },
                    { 153, "50740352380184", "Privado", false, "CENTRO INTEGRAL ODONTOLOGICO MERCEDES SRL (CIOM. SRL)", null, "ESSIDT" },
                    { 154, "50740352380215", "Privado", false, "CENTRO PRIVADO DE ESPEC. MEDICAS", null, "ESSIDT" },
                    { 155, "50740352380219", "Privado", false, "CLINICA PRIVADA DEL OJO", null, "ESSIDT" },
                    { 156, "50740352380277", "Privado", false, "CENTRO MEDICO DE OBESIDAD Y CLINICA MEDICA", null, "ESSIDT" },
                    { 157, "50740352380282", "Privado", false, "CENTRO MUTUAL JUSTO DARACT", null, "ESSIDT" },
                    { 158, "50740352380343", "Privado", false, "CPO ESPECIALIDADES MEDICAS", null, "ESSIDT" },
                    { 159, "50740352380765", "Privado", false, "CENTRO MEDICO FENIX", null, "ESSIDT" },
                    { 160, "50740352380769", "Privado", false, "CENTRO INTEGRAL VITAL CORPS", null, "ESSIDT" },
                    { 161, "50740352380779", "Privado", false, "CENTRO DE SALUD DENTAL", null, "ESSIDT" },
                    { 162, "50740352380781", "Privado", false, "CENTRO FONOAUDIOLOGIA INTEGRAL", null, "ESSIDT" },
                    { 163, "50740352380782", "Privado", false, "CONSULTORIOS MEDICOS VILLA MERCEDES", null, "ESSIDT" },
                    { 164, "50740352380783", "Privado", false, "OFTALMO-MEDICOS S.R.L", null, "ESSIDT" },
                    { 165, "50740352380785", "Privado", false, "CENTRO MEDICO RESPIRAR", null, "ESSIDT" },
                    { 166, "50740352380786", "Privado", false, "ODONTOLOGIA INTEGRAL", null, "ESSIDT" },
                    { 167, "50740352380788", "Privado", false, "CENTRO ODONTOLOGICO Y ESPECIALIDADES INTEGRADAS", null, "ESSIDT" },
                    { 168, "50740352380789", "Privado", false, "CENTRO MEDICO Y KINESIOLOGICO GIRAUDO", null, "ESSIDT" },
                    { 169, "50740352380790", "Privado", false, "CENTRO MEDICO SMATA SALUD", null, "ESSIDT" },
                    { 170, "50740352380791", "Privado", false, "CENTRO MEDICO OSSIMRA", null, "ESSIDT" },
                    { 171, "50740352380832", "Privado", false, "INSTITUTO DE LA VISION SALUD I.VI.SA", null, "ESSIDT" },
                    { 172, "50740352380893", "Privado", false, "KINEFIQ", null, "ESSIDT" },
                    { 173, "50740352380894", "Privado", false, "CENTRO SALUD DENTAL", null, "ESSIDT" },
                    { 174, "50740352380951", "Privado", false, "MARTIN CARLOS PEREZ", null, "ESSIDT" },
                    { 175, "50740352380962", "Privado", false, "CONSULTORIO MEDICO OSPRERA VM", null, "ESSIDT" },
                    { 176, "50740352380976", "Privado", false, "CENTRO DE ESPECIALIDADES KINESICAS SANA", null, "ESSIDT" },
                    { 177, "50740352380977", "Privado", false, "CONSULTORIO ODONTOLOGICO CHIERICHETTI", null, "ESSIDT" },
                    { 178, "50740352380979", "Privado", false, "CONSULTORIOS EXTERNOS SANATORIO LA MERCED", null, "ESSIDT" },
                    { 179, "50740352381015", "Privado", false, "CENTRO MEDICO MEDILAB", null, "ESSIDT" },
                    { 180, "50740352581118", "Universitario público", false, "CENTRO INTEGRAL DE SALUD DR. FERNANDO ROMERO", null, "ESSIDT" },
                    { 181, "50740353080925", "Otros", false, "CENTRO MEDICO PROVIDA", null, "ESSIDT" },
                    { 182, "50740422180049", "Provincial", false, "CAPS BAGUAL", null, "ESSIDT" },
                    { 183, "50740422180059", "Provincial", false, "CAPS BATAVIA", null, "ESSIDT" },
                    { 184, "50740422180090", "Provincial", false, "CAPS FORTIN EL PATRIA", null, "ESSIDT" },
                    { 185, "50740422180105", "Provincial", false, "CAPS LA VERDE", null, "ESSIDT" },
                    { 186, "50740422180120", "Provincial", false, "CAPS LOS OVEROS", null, "ESSIDT" },
                    { 187, "50740422180124", "Provincial", false, "CAPS MARTIN DE LOYOLA", null, "ESSIDT" },
                    { 188, "50740422180132", "Provincial", false, "CAPS NAVIA", null, "ESSIDT" },
                    { 189, "50740422180133", "Provincial", false, "CAPS NUEVA GALIA", null, "ESSIDT" },
                    { 190, "50740422181052", "Provincial", false, "CENTRO DE SALUD NAHUEL MAPA", null, "ESSIDT" },
                    { 191, "50740492180051", "Provincial", false, "CAPS BAJO VELIZ", null, "ESSIDT" },
                    { 192, "50740492180066", "Provincial", false, "CAPS CERRO DE ORO", null, "ESSIDT" },
                    { 193, "50740492180087", "Provincial", false, "CAPS EL TALITA", null, "ESSIDT" },
                    { 194, "50740492180107", "Provincial", false, "CAPS LAFINUR", null, "ESSIDT" },
                    { 195, "50740492180117", "Provincial", false, "CAPS LOS CAJONES", null, "ESSIDT" },
                    { 196, "50740492180119", "Provincial", false, "CAPS LOS MOLLES", null, "ESSIDT" },
                    { 197, "50740492180134", "Provincial", false, "CAPS Nº 53 - CARPINTERIA", null, "ESSIDT" },
                    { 198, "50740492180140", "Provincial", false, "CAPS PIEDRA BLANCA", null, "ESSIDT" },
                    { 199, "50740492180145", "Provincial", false, "CAPS PUNTA DEL AGUA", null, "ESSIDT" },
                    { 200, "50740492280272", "Municipal", false, "CIC VILLA DE MERLO", null, "ESSIDT" },
                    { 201, "50740492280673", "Municipal", false, "CAPS BARRIO ROSEDAL", null, "ESSIDT" },
                    { 202, "50740492281039", "Municipal", false, "CIC BARRANCAS COLORADAS", null, "ESSIDT" },
                    { 203, "50740492380334", "Privado", false, "FLAVIA PATERNI", null, "ESSIDT" },
                    { 204, "50740492380339", "Privado", false, "CENTRO MEDICO OLIVERA Y ASOCIADOS", null, "ESSIDT" },
                    { 205, "50740492380759", "Privado", false, "CONSULTORIO ODONTOLOGICO SERGIANI", null, "ESSIDT" },
                    { 206, "50740492380761", "Privado", false, "CONSULTORIO ODONTOLOGICO DENTIS", null, "ESSIDT" },
                    { 207, "50740492380766", "Privado", false, "CONSULTORIO ODONTOLOGICO SALUD BUCAL", null, "ESSIDT" },
                    { 208, "50740492380768", "Privado", false, "CONSULTORIOS ODONTOLOGICOS CIRO", null, "ESSIDT" },
                    { 209, "50740492380772", "Privado", false, "CONSULTORIO ODONTOLOGICO ZAVALA", null, "ESSIDT" },
                    { 210, "50740492380773", "Privado", false, "CONSULTORIO ODONTOLOGICO DOLORINI", null, "ESSIDT" },
                    { 211, "50740492380817", "Privado", false, "CONSULTORIO ODONTOLOGICO GONZALES", null, "ESSIDT" },
                    { 212, "50740492381056", "Privado", false, "FUNDACION ACONCAGUA", null, "ESSIDT" },
                    { 213, "50740492381079", "Privado", false, "CENTRO DE PEDIATRIA DR. FURQUE", null, "ESSIDT" },
                    { 214, "50740562180045", "Provincial", false, "CAPS ALTO PELADO", null, "ESSIDT" },
                    { 215, "50740562180046", "Provincial", false, "CAPS ALTO PENCOSO", null, "ESSIDT" },
                    { 216, "50740562180052", "Provincial", false, "CAPS BALDE", null, "ESSIDT" },
                    { 217, "50740562180055", "Provincial", false, "CAPS A.M.E.P.", null, "ESSIDT" },
                    { 218, "50740562180057", "Provincial", false, "CAPS  ESTRELLA DEL SUR", null, "ESSIDT" },
                    { 219, "50740562180058", "Provincial", false, "CAPS BARRIO EVA PERON", null, "ESSIDT" },
                    { 220, "50740562180060", "Provincial", false, "CAPS BEAZLEY", null, "ESSIDT" },
                    { 221, "50740562180069", "Provincial", false, "CAPS CHOSMES", null, "ESSIDT" },
                    { 222, "50740562180071", "Provincial", false, "CAPS DESAGUADERO", null, "ESSIDT" },
                    { 223, "50740562180073", "Provincial", false, "CAPS Nº 5 DR. HANNA M. ABDALLAH", null, "ESSIDT" },
                    { 224, "50740562180081", "Provincial", false, "CAPS EL CHORRILLO", null, "ESSIDT" },
                    { 225, "50740562180089", "Provincial", false, "CAPS EL VOLCAN", null, "ESSIDT" },
                    { 226, "50740562180093", "Provincial", false, "CAPS JARILLA", null, "ESSIDT" },
                    { 227, "50740562180097", "Provincial", false, "CAPS JULIO BONA Nº 12", null, "ESSIDT" },
                    { 228, "50740562180110", "Provincial", false, "CAPS LAS BARRANCAS", null, "ESSIDT" },
                    { 229, "50740562180121", "Provincial", false, "CAPS BARRIO AMPYA MAESTROS PUNTANOS", null, "ESSIDT" },
                    { 230, "50740562180122", "Provincial", false, "CAPS MALVINAS ARGENTINAS", null, "ESSIDT" },
                    { 231, "50740562180125", "Provincial", false, "CAPS N°14 MONSEÑOR TIBILETTI", null, "ESSIDT" },
                    { 232, "50740562180127", "Provincial", false, "CAPS Nº 10 - BARRIO SAN MARTIN", null, "ESSIDT" },
                    { 233, "50740562180130", "Provincial", false, "CAPS Nº 80 - POTRERO DE LOS FUNES", null, "ESSIDT" },
                    { 234, "50740562180135", "Provincial", false, "CAPS Nº7 - BARRIO LAS AMERICAS", null, "ESSIDT" },
                    { 235, "50740562180143", "Provincial", false, "CAPS PUEBLO NUEVO", null, "ESSIDT" },
                    { 236, "50740562180154", "Provincial", false, "CAPS N° 9 SAN JERONIMO", null, "ESSIDT" },
                    { 237, "50740562180169", "Provincial", false, "CAPS ZANJITAS", null, "ESSIDT" },
                    { 238, "50740562180170", "Provincial", false, "CAPS N° BARRIO 1° DE MAYO", null, "ESSIDT" },
                    { 239, "50740562180227", "Provincial", false, "HOSPITAL CERRO DE LA CRUZ", null, "ESCIG" },
                    { 240, "50740562180230", "Provincial", false, "HOSPITAL DEL OESTE DR. ATILIO LUCHINI", null, "ESCIG" },
                    { 241, "50740562180231", "Provincial", false, "HOSPITAL DEL SUR", null, "ESCIG" },
                    { 242, "50740562180233", "Provincial", false, "HOSPITAL MARIA J. BECKER DE LA PUNTA", null, "ESCIG" },
                    { 243, "50740562180439", "Provincial", false, "CAPS SUYUQUE", null, "ESSIDT" },
                    { 244, "50740562180637", "Provincial", false, "CENTRO DE REFERENCIA PROVINCIAL DE REHABILITACION", null, "ESSIDT" },
                    { 245, "50740562180638", "Provincial", false, "CENTRO DE ADOLESCENCIA DE SAN LUIS", null, "ESSIDT" },
                    { 246, "50740562180639", "Provincial", false, "CAPS HOSPITAL DEL NORTE", null, "ESSIDT" },
                    { 247, "50740562181059", "Provincial", false, "CENTRO DE ENFERMEDADES RESPIRATORIAS", null, "ESSIDT" },
                    { 248, "50740562181096", "Provincial", false, "HOSPITAL MARTHA  ABDALLAH IGLESIAS", null, "ESSIDT" },
                    { 249, "50740562181117", "Provincial", false, "CAPS SERRANIAS PUNTANAS", null, "ESSIDT" },
                    { 250, "50740562280848", "Municipal", false, "CAPS EVA PERON 3RA ROTONDA", null, "ESSIDT" },
                    { 251, "50740562280849", "Municipal", false, "CAPS ALBERTO PUCHMULLER", null, "ESSIDT" },
                    { 252, "50740562280850", "Municipal", false, "CAPS NESTOR KIRCHNER", null, "ESSIDT" },
                    { 253, "50740562380186", "Privado", false, "CENTRO MEDICO BIOQUIMICO 9 DE JULIO", null, "ESSIDT" },
                    { 254, "50740562380187", "Privado", false, "CENTRO MEDICO CASEROS", null, "ESSIDT" },
                    { 255, "50740562380198", "Privado", false, "CENTRO MEDICO MEDICI", null, "ESSIDT" },
                    { 256, "50740562380200", "Privado", false, "CENTRO MEDICO PEDIATRICA SAN LUIS", null, "ESSIDT" },
                    { 257, "50740562380203", "Privado", false, "CENTRO MEDICO PRIVADO (CEMEPRI) S.R.L.", null, "ESSIDT" },
                    { 258, "50740562380204", "Privado", false, "CENTRO MEDICO SANTA LUCIA", null, "ESSIDT" },
                    { 259, "50740562380206", "Privado", false, "CENTRO MEDICO S.T.I.A (SINDICATO DE TRABAJADORES DE INDUSTRIAS)", null, "ESSIDT" },
                    { 260, "50740562380212", "Privado", false, "CENTRO OFTALMOLOGICO - JUNIN 1185", null, "ESSIDT" },
                    { 261, "50740562380213", "Privado", false, "CENTRO PRIVADO DE CIRUGIA PLASTICA", null, "ESSIDT" },
                    { 262, "50740562380216", "Privado", false, "CENTRO UROLOGICO DIAGNOSTICO Y TRATAMIENTO", null, "ESSIDT" },
                    { 263, "50740562380220", "Privado", false, "CONSTRUIR SALUD", null, "ESSIDT" },
                    { 264, "50740562380226", "Privado", false, "FAMISALUD", null, "ESSIDT" },
                    { 265, "50740562380234", "Privado", false, "INSTITUTO DE SALUD FEMENINA (IFEM)", null, "ESSIDT" },
                    { 266, "50740562380335", "Privado", false, "DR. CRESPO CARLOS ALFREDO - ODONTOSALUD", null, "ESSIDT" },
                    { 267, "50740562380338", "Privado", false, "CENTRO MEDICO RIVADAVIA", null, "ESSIDT" },
                    { 268, "50740562380555", "Privado", false, "CENTRO MEDICO DERMATOLOGICO DRA. FARRERO", null, "ESSIDT" },
                    { 269, "50740562380636", "Privado", false, "CENTRO MEDICO MEDICENTER", null, "ESSIDT" },
                    { 270, "50740562380644", "Privado", false, "CONSULTORIO MEDICO ARCOR", null, "ESSIDT" },
                    { 271, "50740562380646", "Privado", false, "CONSULTORIO MEDICO FIGUEROA", null, "ESSIDT" },
                    { 272, "50740562380667", "Privado", false, "CENTRO MEDICO DEL TRABAJO SAN LUIS", null, "ESSIDT" },
                    { 273, "50740562380682", "Privado", false, "CONSULTORIOS MEDICOS CIANN", null, "ESSIDT" },
                    { 274, "50740562380685", "Privado", false, "CENTRO MEDICO BUEN PASTOR", null, "ESSIDT" },
                    { 275, "50740562380687", "Privado", false, "CONSULTORIO DE PEDIATRIA Y ADOLESCENCIA ABDALLAH", null, "ESSIDT" },
                    { 276, "50740562380688", "Privado", false, "CONSULTORIO MEDICO", null, "ESSIDT" },
                    { 277, "50740562380689", "Privado", false, "CONSULTORIO MEDICO VALLEJOS", null, "ESSIDT" },
                    { 278, "50740562380690", "Privado", false, "CONSULTORIO EXTERNO", null, "ESSIDT" },
                    { 279, "50740562380691", "Privado", false, "CONSULTORIO ODONTOLOGICO VICTOR LUJAN", null, "ESSIDT" },
                    { 280, "50740562380692", "Privado", false, "CONSULTORIO MEDICO ABAYAY", null, "ESSIDT" },
                    { 281, "50740562380693", "Privado", false, "CONSULTORIO MEDICO DRA. MALDONADO", null, "ESSIDT" },
                    { 282, "50740562380694", "Privado", false, "CONSULTORIO MEDICO DRA. SASIAIN", null, "ESSIDT" },
                    { 283, "50740562380695", "Privado", false, "CONSULTORIO MEDICO DR. NOFAL", null, "ESSIDT" },
                    { 284, "50740562380696", "Privado", false, "CONSULTORIOS MEDICOS ZOPPI", null, "ESSIDT" },
                    { 285, "50740562380697", "Privado", false, "CONSULTORIOS ODONTOLOGICOS DOMINGUEZ", null, "ESSIDT" },
                    { 286, "50740562380698", "Privado", false, "CONSULTORIO MEDICO WAL MART", null, "ESSIDT" },
                    { 287, "50740562380699", "Privado", false, "CONSULTORIO MEDICO SAN BENITO", null, "ESSIDT" },
                    { 288, "50740562380700", "Privado", false, "CONSULTORIO MEDICO DR. JEREZ", null, "ESSIDT" },
                    { 289, "50740562380701", "Privado", false, "CONSULTORIO MEDICO PORTAL MUJER", null, "ESSIDT" },
                    { 290, "50740562380702", "Privado", false, "CONSULTORIO MEDICO DR. FIGUEROA", null, "ESSIDT" },
                    { 291, "50740562380703", "Privado", false, "CONSULTORIOS MEDICOS DR. LATINI", null, "ESSIDT" },
                    { 292, "50740562380704", "Privado", false, "CONSULTORIO MEDICO BUENA SALUD", null, "ESSIDT" },
                    { 293, "50740562380707", "Privado", false, "CONSULTORIO MEDICO DRA. CATALINA ROWE", null, "ESSIDT" },
                    { 294, "50740562380708", "Privado", false, "CONSULTORIO MEDICO MEDICAL HAIR", null, "ESSIDT" },
                    { 295, "50740562380709", "Privado", false, "CONSULTORIO MEDICO Y LABORATORIO DE ANALISIS CLINICOS BIOFARM", null, "ESSIDT" },
                    { 296, "50740562380710", "Privado", false, "CONSULTORIO MEDICO DR. ESPOSITO EDUARDO", null, "ESSIDT" },
                    { 297, "50740562380711", "Privado", false, "CONSULTORIO ODONTOLOGICO DENTAL JUANA KOSLAY", null, "ESSIDT" },
                    { 298, "50740562380712", "Privado", false, "CONSULTORIO MEDICO DR. DARIO CAMARGO", null, "ESSIDT" },
                    { 299, "50740562380714", "Privado", false, "CONSULTORIO MEDICO DR. CATALINI", null, "ESSIDT" },
                    { 300, "50740562380715", "Privado", false, "CONSULTORIO MEDICO DR. CALBACHO", null, "ESSIDT" },
                    { 301, "50740562380716", "Privado", false, "CONSULTORIO MEDICO DRA. SUSANA MARONE", null, "ESSIDT" },
                    { 302, "50740562380717", "Privado", false, "CONSULTORIO MEDICO ATEMPO", null, "ESSIDT" },
                    { 303, "50740562380718", "Privado", false, "CONSULTORIOS OSPRERA", null, "ESSIDT" },
                    { 304, "50740562380719", "Privado", false, "CENTRO MEDICO INDOOR GARDENS", null, "ESSIDT" },
                    { 305, "50740562380720", "Privado", false, "CONSULTORIO ODONTOLOGICO INTEGRAL C.Y.R.O", null, "ESSIDT" },
                    { 306, "50740562380721", "Privado", false, "CONSULTORIO ODONTOLOGICO OD. SILVIA E. MUNDET", null, "ESSIDT" },
                    { 307, "50740562380722", "Privado", false, "CONSULTORIO ODONTOLOGICO DRA. ROWE ELIZABETH MARIA", null, "ESSIDT" },
                    { 308, "50740562380723", "Privado", false, "CONSULTORIO ODONTOLOGICO PENIN", null, "ESSIDT" },
                    { 309, "50740562380724", "Privado", false, "CONSULTORIOS ODONTOLOGICOS SPAZIO DENTAL", null, "ESSIDT" },
                    { 310, "50740562380725", "Privado", false, "CONSULTORIO ODONTOLOGICO OD, GRACIELA MURCIA", null, "ESSIDT" },
                    { 311, "50740562380726", "Privado", false, "CONSULTORIO ONTOLOGICO BOUCHARD", null, "ESSIDT" },
                    { 312, "50740562380727", "Privado", false, "CONSULTORIO ODONTOLOGICO DE REHABILITACION ORAL", null, "ESSIDT" },
                    { 313, "50740562380729", "Privado", false, "CONSULTORIO ODONTOLOGICO DRA. WEINSTOCK LEONTINA", null, "ESSIDT" },
                    { 314, "50740562380732", "Privado", false, "CONSULTORIO ODONTOLOGICO SILVIA SUSANA PANINI", null, "ESSIDT" },
                    { 315, "50740562380734", "Privado", false, "CONSULTORIO ODONTOLOGICO OD, AMALIA GIMENEZ DE DOMENICONI", null, "ESSIDT" },
                    { 316, "50740562380738", "Privado", false, "CONSULTORIOS ODONT, CLINICA DENTAL RUTA", null, "ESSIDT" },
                    { 317, "50740562380739", "Privado", false, "CONSULTORIO ODONTOLOGICO OD, RAMON GOMEZ", null, "ESSIDT" },
                    { 318, "50740562380740", "Privado", false, "CONSULTORIO ODONTOLOGICO SAN JOSE", null, "ESSIDT" },
                    { 319, "50740562380741", "Privado", false, "CONSULTORIO ODONTOLOGICO (SMATA)", null, "ESSIDT" },
                    { 320, "50740562380742", "Privado", false, "CONSULTORIO ODONTOLOGICO MATTEVI", null, "ESSIDT" },
                    { 321, "50740562380743", "Privado", false, "CONSULTORIO ODONTOLOGICO EMMA INES BRUERA", null, "ESSIDT" },
                    { 322, "50740562380793", "Privado", false, "CENTRO MEDICO COLON Y EME", null, "ESSIDT" },
                    { 323, "50740562380796", "Privado", false, "CENTRO MEDICO COLON Y EME ESTUDIOS EMPRESARIALES.", null, "ESSIDT" },
                    { 324, "50740562380797", "Privado", false, "CENTRO ODONTOLOGICO MASTROTTA", null, "ESSIDT" },
                    { 325, "50740562380798", "Privado", false, "DENTAL PUNTANA", null, "ESSIDT" },
                    { 326, "50740562380799", "Privado", false, "CONSULTORIO ODONTOLOGICO MARIA VALERIA CORTINEZ", null, "ESSIDT" },
                    { 327, "50740562380800", "Privado", false, "ARDEL SERVICIOS ODONTOLOGICOS INTEGRALES", null, "ESSIDT" },
                    { 328, "50740562380801", "Privado", false, "LA ODONTOLOGIA CONSULTORIO ODONTOLOGICO", null, "ESSIDT" },
                    { 329, "50740562380802", "Privado", false, "CONSULTORIO ODONTOLOGICO VIVIANA MONICA NASISIS", null, "ESSIDT" },
                    { 330, "50740562380803", "Privado", false, "CONSULTORIO ODONTOLOGICO OD. ARROYO EUGENIA", null, "ESSIDT" },
                    { 331, "50740562380818", "Privado", false, "CONSULTORIO ODONTOLOGICO INTEGRAL", null, "ESSIDT" },
                    { 332, "50740562380820", "Privado", false, "CONSULTORIOS PLAZA, MEDICINA DE FAMILIA Y DEPORTOLOGIA", null, "ESSIDT" },
                    { 333, "50740562380826", "Privado", false, "CENTRO MEDICO FEMED", null, "ESSIDT" },
                    { 334, "50740562380835", "Privado", false, "GIMNASIO Y BOX DE REHABILITACION KINESIOLOGICA Y FISIOTERAPIA", null, "ESSIDT" },
                    { 335, "50740562380836", "Privado", false, "FISIOMED", null, "ESSIDT" },
                    { 336, "50740562380837", "Privado", false, "VIDA PLENA VP", null, "ESSIDT" },
                    { 337, "50740562380838", "Privado", false, "CONSULTORIO DE KINESIOLOGIA Y ESTETICA CORPORAL HOLISTICA", null, "ESSIDT" },
                    { 338, "50740562380843", "Privado", false, "CONSULTORIO DE KINESIOLOGIA Y ODONTOLOGIA", null, "ESSIDT" },
                    { 339, "50740562380908", "Privado", false, "ETIKOS", null, "ESSIDT" },
                    { 340, "50740562380912", "Privado", false, "CENTRO DE REHABILITACION NEURO-FISIO-KINESIO  TEMPUS", null, "ESSIDT" },
                    { 341, "50740562380952", "Privado", false, "BRINGAS CLAUDIO GABRIEL", null, "ESSIDT" },
                    { 342, "50740562380957", "Privado", false, "LABORATORIO DE MECANICA DENTAL CONCI", null, "ESSIDT" },
                    { 343, "50740562380961", "Privado", false, "KINESPORT", null, "ESSIDT" },
                    { 344, "50740562380965", "Privado", false, "KINESIO PLUS", null, "ESSIDT" },
                    { 345, "50740562380966", "Privado", false, "CENTRO DE REHABILITACION SAN BENITO", null, "ESSIDT" },
                    { 346, "50740562380967", "Privado", false, "COEM", null, "ESSIDT" },
                    { 347, "50740562380975", "Privado", false, "CONSULTORIOS EMPRESART S.R.L", null, "ESSIDT" },
                    { 348, "50740562380978", "Privado", false, "ADN SAN LUIS", null, "ESCL" },
                    { 349, "50740562381007", "Privado", false, "IMED", null, "ESSIDT" },
                    { 350, "50740562381010", "Privado", false, "CENTRO MEDICO SAN CAMILO", null, "ESSIDT" },
                    { 351, "50740562381011", "Privado", false, "CIMAH CENTRO INTEGRAL AVANZADO DE HERIDAS", null, "ESSIDT" },
                    { 352, "50740562381012", "Privado", false, "CIEP", null, "ESSIDT" },
                    { 353, "50740562381013", "Privado", false, "CENTRO MEDICO LOS ALAMOS", null, "ESSIDT" },
                    { 354, "50740562381017", "Privado", false, "NOVA PRAXIS", null, "ESSIDT" },
                    { 355, "50740562381018", "Privado", false, "CENTRO DE NEUROLOGIA Y REHABILITACION CENYR", null, "ESSIDT" },
                    { 356, "50740562381019", "Privado", false, "CENTRO MEDICO LAVALLE", null, "ESSIDT" },
                    { 357, "50740562381047", "Privado", false, "CENTRO MEDICO RIVADAVIA ONTIVEROS", null, "ESSIDT" },
                    { 358, "50740562381048", "Privado", false, "CENOR CENTRO DE TERAPIA RADIANTE", null, "ESSIDT" },
                    { 359, "50740562381124", "Privado", false, "CENTRO MEDICO INNOVA", null, "ESSIDT" },
                    { 360, "50740562780223", "Seguridad Social", false, "CONSULTORIOS EXTERNOS DEL SINDICATO DE CHOFERES DE CAMIONES", null, "ESSIDT" },
                    { 361, "50740562781046", "Seguridad Social", false, "OBRA SOCIAL DE CONDUCTORES CAMIONEROS Y PERSONAL DEL TRANSPORTE AUTOMOTOR DE CARGAS DE SAN LUIS", null, "ESSIDT" },
                    { 362, "50740562781099", "Seguridad Social", false, "OSFATUN MEDICINA PARA LA MUJER", null, "ESSIDT" },
                    { 363, "50740563080998", "Otros", false, "CENTRO MEDICO BIOMED", null, "ESSIDT" },
                    { 364, "50740632180106", "Provincial", false, "CAPS LA VERTIENTE", null, "ESSIDT" },
                    { 365, "50740632180109", "Provincial", false, "CAPS LAS AGUADAS", null, "ESSIDT" },
                    { 366, "50740632180111", "Provincial", false, "CAPS LAS CHACRAS", null, "ESSIDT" },
                    { 367, "50740632180129", "Provincial", false, "CAPS Nº 63 - LAS LAGUNAS", null, "ESSIDT" },
                    { 368, "50740632180139", "Provincial", false, "CAPS Nº 72 PASO GRANDE", null, "ESSIDT" },
                    { 369, "50740632180141", "Provincial", false, "CAPS POTRERILLO", null, "ESSIDT" },
                    { 370, "50740632180166", "Provincial", false, "CAPS VILLA DE PRAGA", null, "ESSIDT" },
                    { 371, "50740632180268", "Provincial", false, "CAPS QUEBRADA DE SAN VICENTE", null, "ESSIDT" },
                    { 372, "51740212380807", "Privado", false, "LABORATORIO DE ANALISIS CLINICOS OMAR PALMERO", null, "ESSID" },
                    { 373, "51740282381030", "Privado", false, "LABORATORIO DE ANALISIS CLINICOS LIA BARROSO", null, "ESSID" },
                    { 374, "51740352380733", "Privado", false, "LABORATORIO DE ANALISIS CLINICOS ANA MARIA PEREZ", null, "ESSID" },
                    { 375, "51740352380736", "Privado", false, "LABORATORIO DE ANALISIS CLINICOS ODETTI Y SERVICIO DE CAMARA GAMMA RISSI", null, "ESSID" },
                    { 376, "51740352381008", "Privado", false, "LABORATORIO DE ANALISIS CLINICOS BERTOMEU", null, "ESSID" },
                    { 377, "51740352381049", "Privado", false, "SIGNA IMAGENES MEDICAS VILLA MERCEDES", null, "ESSID" },
                    { 378, "51740422180640", "Provincial", false, "CAPS RANQUEL RUKA TREMOY", null, "ESSIDT" },
                    { 379, "51740492380713", "Privado", false, "CONSULTORIO MEDICO DUJE", null, "ESSID" },
                    { 380, "51740492381014", "Privado", false, "CENTRO DE CITOLOGIA PRIVADO CCP", null, "ESSID" },
                    { 381, "51740562180337", "Provincial", false, "LABORATORIO DE GENETICA FORENSE - ANALISIS DE ADN", "www.labgeneticaforense.com.ar", "ESSID" },
                    { 382, "51740562180621", "Provincial", false, "LABORATORIO DE DIAGNOSTICO GENITO MAMARIO", null, "ESSID" },
                    { 383, "51740562180653", "Provincial", false, "PROGRAMA LABORATORIO DE SALUD PUBLICA DR. DALMIRO PEREZ LABORDA", null, "ESSID" },
                    { 384, "51740562181032", "Provincial", false, "LABORATORIO DE EPIDEMIOLOGIA", null, "ESSID" },
                    { 385, "51740562380243", "Privado", false, "CONSULTORIOS DOSPU", null, "ESSIDT" },
                    { 386, "51740562380429", "Privado", false, "CENTRO DE DIAGNOSTICO POR IMAGENES PROIMAGEN S.R.L.", "www.diagnosticovalentini.com", "ESSID" },
                    { 387, "51740562380441", "Privado", false, "DIAGNOSTICO PRINGLES", null, "ESSID" },
                    { 388, "51740562380475", "Privado", false, "LABORATORIO DE ANALISIS CLINICOS LIC. ANA MARIA MELONI DE TONN", null, "ESSID" },
                    { 389, "51740562380476", "Privado", false, "BIOLABORATORIO", null, "ESSID" },
                    { 390, "51740562380484", "Privado", false, "LABORATORIO DE ANALISIS CLINICOS LICENCIADA GRACIELA BEATRIZ RODRIGUEZ", null, "ESSID" },
                    { 391, "51740562380642", "Privado", false, "LABORATORIO DE ANALISIS CLINICOS CASEROS", null, "ESSID" },
                    { 392, "51740562380648", "Privado", false, "CENTRO DE DIAGNOSTICO GP DIAGNOSTICO SAN LUIS", null, "ESSID" },
                    { 393, "51740562380658", "Privado", false, "SIGNA PLUS DIAGNOSTICO POR IMAGENES RMI", null, "ESSID" },
                    { 394, "51740562380664", "Privado", false, "LABORATORIO DE ANALISIS CLINICOS CENTORBI", null, "ESSID" },
                    { 395, "51740562380665", "Privado", false, "LABORATORIO PORTELA", null, "ESSID" },
                    { 396, "51740562380666", "Privado", false, "INBIO", null, "ESSID" },
                    { 397, "51740562380670", "Privado", false, "LABORATORIO DE ANALISIS CLINICOS BIANCO", null, "ESSID" },
                    { 398, "51740562380760", "Privado", false, "CAMARA GAMMA SPECT SAN LUIS", null, "ESSID" },
                    { 399, "51740562380762", "Privado", false, "CONSULTORIO RADIOLOGICO DR. MIGUEL ANGEL GARRO", null, "ESSID" },
                    { 400, "51740562380763", "Privado", false, "DIAGNOS SALUD S.R.L", null, "ESSID" },
                    { 401, "51740562380764", "Privado", false, "SIGNA IMAGENES MEDICAS SAN LUIS", null, "ESSID" },
                    { 402, "51740562380839", "Privado", false, "CENTRO DE REHABILITACION PROFESOR SUAREZ", null, "ESSID" },
                    { 403, "51740562381005", "Privado", false, "LABORATORIO DE ANALISIS CLINICOS ORELLANO ELORZA", null, "ESSID" },
                    { 404, "51740562381016", "Privado", false, "LABORATORIO DE ANALISIS CLINICOS LIC. PORTELA", null, "ESSID" },
                    { 405, "51740562381071", "Privado", false, "LABORATORIO BIOMEDICINA BIOMAV", null, "ESSID" },
                    { 406, "52740352380251", "Privado", false, "CENTRO PRIVADO DE NEFROLOGIA Y HEMODIALISIS SRL", null, "ESSIT" },
                    { 407, "52740352380278", "Privado", false, "CONSULTORIO ODONTOLOGICO", null, "ESCL" },
                    { 408, "52740352380344", "Privado", false, "CENTRO INTERDISCIPLINARIO DE REHABILITACION", null, "ESSIT" },
                    { 409, "52740352380626", "Privado", false, "REHABILITAR TERAPIA NEUROLOGICA", null, "ESSIT" },
                    { 410, "52740352380656", "Privado", false, "CENTRO EDUCATIVO TERAPEUTICO TIERRA DE SOL", null, "ESSIT" },
                    { 411, "52740352380780", "Privado", false, "CLINICA DE CIRUGIA PLASTICA S.R.L", null, "ESSIT" },
                    { 412, "52740352380794", "Privado", false, "CENTRO EDUCATIVO TERAPEUTICO CON INTEGRACION ESCOLAR TRIADA", null, "ESSIT" },
                    { 413, "52740352380795", "Privado", false, "CENTRO DE DIA NEWEN S.A.", null, "ESSIT" },
                    { 414, "52740562180287", "Provincial", false, "CAPS Nº 4 BRIARDO LLORENTE RUIZ", null, "ESSIDT" },
                    { 415, "52740562181115", "Provincial", false, "CPAA   CENTRO DE PREVENCION Y ASISTENCIA DE LAS ADICCIONES", null, "ESSIT" },
                    { 416, "52740562380250", "Privado", false, "CENTRO PRIVADO DE NEFROLOGIA SAN LUIS RENAL", null, "ESSIT" },
                    { 417, "52740562380284", "Privado", false, "CENTRO DE SALUD ESTUDIANTIL UNIVERSITARIA", null, "ESSIDT" },
                    { 418, "52740562380557", "Privado", false, "HACER BIEN CENTRO DE REHABILITACION", null, "ESSIT" },
                    { 419, "52740562380583", "Privado", false, "INSTITUTO DE REHABILITACION INTEGRAL", null, "ESSIT" },
                    { 420, "52740562380641", "Privado", false, "CENTRO DE DIA LUZ DE LUNA", null, "ESSIT" },
                    { 421, "52740562380643", "Privado", false, "CENTRO DE ESTIMULACION TEMPRANA Y CENTRO DE REHABILITACION RUKALAF", null, "ESSIT" },
                    { 422, "52740562380647", "Privado", false, "CENTRO DE REHABILITACION CASABLU", null, "ESSIT" },
                    { 423, "52740562380651", "Privado", false, "CENTRO DE DIALISIS NEFROREAL", null, "ESSIT" },
                    { 424, "52740562380654", "Privado", false, "CENTRO DE ATENCION TEMPRANA Y REHABILITACION INTEGRAL CEATRI", null, "ESSIT" },
                    { 425, "52740562380659", "Privado", false, "CENTRO DE ESTIMULACION TEMPRANA Y NEUROREHABILITACION  CEYNE", null, "ESSIT" },
                    { 426, "52740562380684", "Privado", false, "CONSULTORIOS Y BOXES KINESIOLOGICOS CON GIMNASIO DE REHABILITACION NASCHAR", null, "ESSIT" },
                    { 427, "52740562380686", "Privado", false, "CONSULTORIO DE KINESIOLOGIA Y FISIOTERAPIA VIDA PLENA", null, "ESSIT" },
                    { 428, "52740562380705", "Privado", false, "CONSULTORIO DE NUTRICION Y PSICOLOGIA IL MODO SANI", null, "ESSIT" },
                    { 429, "52740562380755", "Privado", false, "CONSULTORIO Y BOXES KINESIOLOGICOS CON GIMNASIO DE REHABILITACION  FEDERICO CORTEZ", null, "ESSIT" },
                    { 430, "52740562380770", "Privado", false, "CONSULTORIO LICENCIADA MARIA DANIELA VELEZ", null, "ESSIT" },
                    { 431, "52740562380774", "Privado", false, "CONSULTORIO DE KINESIOLOGIA Y FISIOTERAPIA GOEN LIC. LETICIA ELIANA ENRIZ", null, "ESSIT" },
                    { 432, "52740562380775", "Privado", false, "CEREIN CONSULTORIO DE REHABILITACION INTEGRAL", null, "ESSIT" },
                    { 433, "52740562380776", "Privado", false, "CONSULTORIO DE KINESIOLOGIA Y FISIOTERAPIA PULSO", null, "ESSIT" },
                    { 434, "52740562380778", "Privado", false, "CONSULTORIO DE KINESIOLOGIA Y FISIOTERAPIA VANNUCCI", null, "ESSIT" },
                    { 435, "52740562380840", "Privado", false, "CONSULTORIO DE KINESIOLOGIA Y FISIOTERAPIA LICENCIADA MARIA ALEJANDRA GODOY", null, "ESSIT" },
                    { 436, "52740562380841", "Privado", false, "CONSULTORIO KINESIOLOGICO LICENCIADO FABIO ZAVALA", null, "ESSIT" },
                    { 437, "52740562380842", "Privado", false, "KINEXIS", null, "ESSIT" },
                    { 438, "52740562380845", "Privado", false, "CONSULTORIO DE KINESIOLOGIA Y FISIOTERAPIA LICENCIADO FERNANDO BARRERA", null, "ESSIT" },
                    { 439, "52740562380846", "Privado", false, "REHABILITACION INTEGRAL RIVADAVIA", null, "ESSIT" },
                    { 440, "52740562380847", "Privado", false, "ZAHA", null, "ESSIT" },
                    { 441, "52740562380884", "Privado", false, "CENTROS TERAPEUTICOS PARA SALUD", null, "ESSIT" },
                    { 442, "52740562380950", "Privado", false, "DI GENARO JUAN CARLOS", null, "ESSIT" },
                    { 443, "52740562381057", "Privado", false, "CENTRO NEFROLOGICO DU-RENS", null, "ESSIT" },
                    { 444, "52740562381070", "Privado", false, "CENTRO DE DIA LA ESPERANZA", null, "ESSIT" },
                    { 445, "53740072380746", "Privado", false, "SAN JORGE SERVICIOS DE SALUD", null, "ESCL" },
                    { 446, "53740072380852", "Privado", false, "INTEGRA SERVICIOS MEDICOS S.A. QUINES", null, "ESCL" },
                    { 447, "53740212380553", "Privado", false, "INTEGRA SERVICIOS MEDICOS LA TOMA", null, "ESCL" },
                    { 448, "53740212380878", "Privado", false, "OPTICA CARLA DE BIASIO SOLUCIONES OPTICAS", null, "ESCL" },
                    { 449, "53740212380936", "Privado", false, "CENTRO MEDICO CLINICA REGIONAL MEDICA CECILIA", null, "ESCL" },
                    { 450, "53740212380939", "Privado", false, "CONSULTORIO MEDICO DEL CENTRO MESA COORDINADORA DE JUBILADOS Y PENSIONADOS NACIONALES FILIAL LA TOMA", null, "ESCL" },
                    { 451, "53740282380881", "Privado", false, "OPTICA TILISARAO", null, "ESCL" },
                    { 452, "53740352180865", "Provincial", false, "SEMPRO VILLA MERCEDES", null, "ESCL" },
                    { 453, "53740352180890", "Provincial", false, "HOSPITAL LA PEDRERA", null, "ESCL" },
                    { 454, "53740352380603", "Privado", false, "GRANDIET VILLA MERCEDES", null, "ESCL" },
                    { 455, "53740352380889", "Privado", false, "LABORATORIO DE ANALISIS CLINICOS LIC. PEDRO ANTONIO YUVERO", null, "ESCL" },
                    { 456, "53740352380906", "Privado", false, "BANCO PRIVADO DE SANGRE VILLA MERCEDES", null, "ESCL" },
                    { 457, "53740352380910", "Privado", false, "LABORATORIO DE ANALISIS CLINICOS PAGANO", null, "ESCL" },
                    { 458, "53740352380931", "Privado", false, "CENTRO MEDICO SALUDDAR", null, "ESCL" },
                    { 459, "53740352380932", "Privado", false, "CENTRO MEDICO BIOS", null, "ESCL" },
                    { 460, "53740352380943", "Privado", false, "CONSULTORIOS INTERDISCIPLINARIOS TESAI", null, "ESCL" },
                    { 461, "53740352380944", "Privado", false, "CENTRO DE ESTIMULACION COGNITIVA INTEGRAL ACCION", null, "ESCL" },
                    { 462, "53740352380945", "Privado", false, "CONSULTORIO MEDICO Y SALA DE ENFERMERIA COMPLEJO ARCOR", null, "ESCL" },
                    { 463, "53740352380954", "Privado", false, "C.I.O.M CENTRO INTEGRAL ODONTOLOGICO MERCEDES", null, "ESCL" },
                    { 464, "53740352380959", "Privado", false, "CENTRO DE VACUNACION E INYECTATORIO INFANTIL GRACIELA", null, "ESCL" },
                    { 465, "53740352380960", "Privado", false, "CENTRO RECREATIVO Y RESIDENCIA PARA ADULTOS MAYORES EN LA QUINTA DE GRACIELA", null, "ESCL" },
                    { 466, "53740352380963", "Privado", false, "CONSULTORIO ODONTOLOGICO PABLO AGUSTIN PERCARA", null, "ESCL" },
                    { 467, "53740352380970", "Privado", false, "CONSULTORIO ODONTOLOGICO CONTRERAS DENTAL", null, "ESCL" },
                    { 468, "53740352380972", "Privado", false, "CENTRO MEDICO PRINGLES", null, "ESCL" },
                    { 469, "53740352380973", "Privado", false, "FONOAUDIOLOGIA INTEGRAL", null, "ESCL" },
                    { 470, "53740352381002", "Privado", false, "OPTICA CENTRO OPTICO LUCERO", null, "ESCL" },
                    { 471, "53740352381003", "Privado", false, "OPTICA VISION", null, "ESCL" },
                    { 472, "53740352381004", "Privado", false, "OPTICA LUTZ FERRANDO", null, "ESCL" },
                    { 473, "53740352381116", "Privado", false, "UMI URGENCIA MEDICA INTEGRAL", null, "ESCL" },
                    { 474, "53740353080916", "Otros", false, "CENTRO MEDICO NEXUS S.R.L.", null, "ESCL" },
                    { 475, "53740353080918", "Otros", false, "CENTRO MEDICO FAMILIA Y SALUD", null, "ESCL" },
                    { 476, "53740492180866", "Provincial", false, "SEMPRO MERLO", null, "ESCL" },
                    { 477, "53740492380256", "Privado", false, "SALUD INTEGRAL S.A.", null, "ESSIDT" },
                    { 478, "53740492380556", "Privado", false, "INTEGRA SERVICIOS MEDICOS - VILLA DE MERLO", null, "ESCL" },
                    { 479, "53740492380875", "Privado", false, "OPTICA CENTRO OPTICO BUENA VISION", null, "ESCL" },
                    { 480, "53740492380876", "Privado", false, "OPTICA EL POETA II", null, "ESCL" },
                    { 481, "53740492380877", "Privado", false, "OPTICA EL POETA", null, "ESCL" },
                    { 482, "53740492380879", "Privado", false, "OPTICA LA OPTICA", null, "ESCL" },
                    { 483, "53740492380880", "Privado", false, "OPTICA OPTIMA VISION", null, "ESCL" },
                    { 484, "53740492380882", "Privado", false, "OPTICA MAXIMA VISION", null, "ESCL" },
                    { 485, "53740492380930", "Privado", false, "CENTRO DE SALUD GENESIS WELLNESS", null, "ESCL" },
                    { 486, "53740493080917", "Otros", false, "CONSULTORIOS SANAR", null, "ESCL" },
                    { 487, "53740493080927", "Otros", false, "CONSULTORIO MEDICO CENTRO VIDAS", null, "ESCL" },
                    { 488, "53740562180463", "Provincial", false, "BANCO CENTRAL DE SANGRE DE LA PROVINCIA DE SAN LUIS", null, "ESCL" },
                    { 489, "53740562180540", "Provincial", false, "CAMION SANITARIO GOBIERNO DE SAN LUIS INTERNO 3-363", null, "ESCL" },
                    { 490, "53740562180600", "Provincial", false, "MOVIL Nº T-002 ODONTOLOGICO MINISTERIO DE SALUD GOBIERNO DE LA PROVINCIA DE SAN LUIS", null, "ESCL" },
                    { 491, "53740562180601", "Provincial", false, "MOVIL Nº T-001 GINECOLOGICO MINISTERIO DE SALUD GOBIERNO DE SAN LUIS", null, "ESCL" },
                    { 492, "53740562180602", "Provincial", false, "MOVIL Nº T-003 OFTALMOLOGIA, CL MEDICA GOBIERNO DE LA PROVINCIA DE SAN LUIS", null, "ESCL" },
                    { 493, "53740562180864", "Provincial", false, "SEMPRO SAN LUIS", null, "ESCL" },
                    { 494, "53740562181104", "Provincial", false, "SERVICIO CONTROL DE VECTORES CHAGAS", null, "ESCL" },
                    { 495, "53740562280825", "Municipal", false, "UNIDAD DE TRASLADO MUNICIPALIDAD DE LA CIUDAD DE LA PUNTA", null, "ESCL" },
                    { 496, "53740562380645", "Privado", false, "LABORATORIO MECANICA DENTAL CURAY SAA", null, "ESCL" },
                    { 497, "53740562380757", "Privado", false, "SERVICIOS MEDICOS INTEGRALES", null, "ESCL" },
                    { 498, "53740562380822", "Privado", false, "SLOTS MACHINES S.A.", null, "ESCL" },
                    { 499, "53740562380844", "Privado", false, "INYECTATORIO BELGRANO", null, "ESCL" },
                    { 500, "53740562380855", "Privado", false, "NOVA SALUD", null, "ESCL" },
                    { 501, "53740562380856", "Privado", false, "VITTAL ASIST ATENCION MEDICA PRIVADA", null, "ESCL" },
                    { 502, "53740562380869", "Privado", false, "OPTICA LUTZ FERRANDO", null, "ESCL" },
                    { 503, "53740562380870", "Privado", false, "OPTICA HORUS", null, "ESCL" },
                    { 504, "53740562380871", "Privado", false, "OPTICA S.E.C.", null, "ESCL" },
                    { 505, "53740562380872", "Privado", false, "OPTICA VISION COLOR SRL", null, "ESCL" },
                    { 506, "53740562380873", "Privado", false, "OPTICA GENESIS", null, "ESCL" },
                    { 507, "53740562380874", "Privado", false, "OPTICA LA PUNTA", null, "ESCL" },
                    { 508, "53740562380896", "Privado", false, "LABORATORIO DE CITOLOGIA VIDA DIAGNOSTICO PARA LA MUJER", null, "ESCL" },
                    { 509, "53740562380902", "Privado", false, "LABORATORIO DE ANALISIS CLINICO", null, "ESCL" },
                    { 510, "53740562380903", "Privado", false, "LABORATORIO DE ANALISIS CLINICO CENTRAL", null, "ESCL" },
                    { 511, "53740562380904", "Privado", false, "LABORATORIO DE ANALISIS CLINICO IBAC S.R.L.", null, "ESCL" },
                    { 512, "53740562380933", "Privado", false, "ALZAR", null, "ESCL" },
                    { 513, "53740562380934", "Privado", false, "INSTITUTO DERMATOLOGICO DE ESTETICA Y LUMINOTERAPIA IDEL", null, "ESCL" },
                    { 514, "53740562380938", "Privado", false, "CONSULTORIOS MEDICOS PLAZA", null, "ESCL" },
                    { 515, "53740562380940", "Privado", false, "CENTRO DE DIAGNOSTICO DE ENFERMEDADES DE LA MUJER CEDIEM", null, "ESCL" },
                    { 516, "53740562380941", "Privado", false, "CENTRO MEDICO TRINIDAD", null, "ESCL" },
                    { 517, "53740562380942", "Privado", false, "POTRERO SALUD", null, "ESCL" },
                    { 518, "53740562380946", "Privado", false, "FAMI SALUD CENTRO MEDICO", null, "ESCL" },
                    { 519, "53740562380969", "Privado", false, "MIS AMORES", null, "ESCL" },
                    { 520, "53740562380971", "Privado", false, "CENTRO PRIVADO DE OJOS", null, "ESCL" },
                    { 521, "53740562380974", "Privado", false, "CENTRO EDUCATIVO TERAPEUTICO AILEN", null, "ESCL" },
                    { 522, "53740562381123", "Privado", false, "FARMACITY SUC. 404", null, "ESCL" },
                    { 523, "53740562381125", "Privado", false, "FARMACITY SUC. 373", null, "ESCL" },
                    { 524, "53740563080895", "Otros", false, "LABORATORIO ARMANDO PEREYRA SANCHEZ", null, "ESCL" },
                    { 525, "53740563080919", "Otros", false, "CENTRO MEDICO LOMBARDI", null, "ESCL" },
                    { 526, "53740563080920", "Otros", false, "CENTRO MEDICO PRIVADO", null, "ESCL" },
                    { 527, "53740563080921", "Otros", false, "CENTRO DE CIRUGIA PLASTICA", null, "ESCL" },
                    { 528, "53740563080922", "Otros", false, "BELGRANO INTEGRAL", null, "ESCL" },
                    { 529, "53740563080923", "Otros", false, "CENTRO TERAPEUTICO PARA SALUD", null, "ESCL" },
                    { 530, "53740563080924", "Otros", false, "CENTRO MEDICO PRIVADO RIVADAVIA", null, "ESCL" },
                    { 531, "53740563080928", "Otros", false, "CONSULTORIOS DE LA ASOCIACION MUTUAL DE PROTECCION FAMILIAR", null, "ESCL" },
                    { 532, "53740563080929", "Otros", false, "CENTRO MEDICO ANDES SALUD", null, "ESCL" }
                });

            migrationBuilder.InsertData(
                table: "TiposPrestacionEnfermeria",
                columns: new[] { "Id", "Grupo", "IsDeleted", "NombrePrestacion" },
                values: new object[,]
                {
                    { 1, "Parenteral", false, "Venoclisis" },
                    { 2, "Parenteral", false, "V.Endovenosa" },
                    { 3, "Parenteral", false, "V.Intramuscular" },
                    { 4, "Parenteral", false, "V.Subcutánea" },
                    { 5, "Enteral", false, "Via Oral" },
                    { 6, "Enteral", false, "V.Sublingual" },
                    { 7, "Mucosas y Piel", false, "Adm.O2" },
                    { 8, "Mucosas y Piel", false, "Adm.Puff" },
                    { 9, "Mucosas y Piel", false, "Nebulizaciones" },
                    { 10, "Mucosas y Piel", false, "Adm.Ocular" },
                    { 11, "Mucosas y Piel", false, "Adm.Cutanea" },
                    { 12, "Mucosas y Piel", false, "Adm.Rectal" },
                    { 13, "Curaciones", false, "Curacion Simple" },
                    { 14, "Curaciones", false, "Curacion Compleja" },
                    { 15, "Curaciones", false, "Curacion de Escara" },
                    { 16, "Curaciones", false, "Asistencia a Sutura" },
                    { 17, "Curaciones", false, "Extraccion de Puntos" },
                    { 18, "Vendajes", false, "Vendaje Simple" },
                    { 19, "Vendajes", false, "Vendaje Elastico" },
                    { 20, "Vendajes", false, "Ferulas" },
                    { 21, "Lavajes", false, "Lavado de Oido" },
                    { 22, "Lavajes", false, "Lavado de Ocular" },
                    { 23, "Sondaje", false, "SNG" },
                    { 24, "Sondaje", false, "SV" },
                    { 25, "Metodos Fisicos", false, "Metodos Fisicos" },
                    { 26, "Rehidratacion oral", false, "Rehidratacion oral" },
                    { 27, "Antropometria", false, "Antropometria" },
                    { 28, "Antropometria", false, "Percentilos" },
                    { 29, "Enema", false, "Enema" },
                    { 30, "Controles", false, "F.Respiratoria" },
                    { 31, "Controles", false, "C.Temperatura" },
                    { 32, "Controles", false, "C.Glucemias" },
                    { 33, "Controles", false, "TA" },
                    { 34, "ECG", false, "ECG" },
                    { 35, "Internacion", false, "TAL" },
                    { 36, "Internacion", false, "Traslados y Derivaciones" },
                    { 37, "Internacion", false, "Retiro vcl" },
                    { 38, "Internacion", false, "Control de goteo" },
                    { 39, "Internacion", false, "Cambio de suero" },
                    { 40, "Internacion", false, "Control de Permeab.Del cateter" },
                    { 41, "Internacion", false, "Higiene y Confort" },
                    { 42, "Internacion", false, "Arreglo de la Unidad" },
                    { 43, "Internacion", false, "Revision de Ambulancia" },
                    { 44, "Internacion", false, "Diag de Enfermeria" },
                    { 45, "Internacion", false, "Pase de Guardia" },
                    { 46, "Internacion", false, "Confeccion de Report" },
                    { 47, "Triagge", false, "Triagge" },
                    { 48, "Otras Actividades", false, "Otras Actividades" },
                    { 49, "Salidas de Ambulancia", false, "Ambulancia Propia" },
                    { 50, "Salidas de Ambulancia", false, "Sempro" },
                    { 51, "Salidas de Ambulancia", false, "Consultorio" },
                    { 52, "Salidas de Ambulancia", false, "Guardia" },
                    { 53, "Salidas de Ambulancia", false, "Ramon Carrillo" },
                    { 54, "Salidas de Ambulancia", false, "Maternidad" },
                    { 55, "Salidas de Ambulancia", false, "Pediatrico" },
                    { 56, "Salidas de Ambulancia", false, "Privado" },
                    { 57, "Salidas de Ambulancia", false, "Salida auxilio" },
                    { 58, "Salidas de Ambulancia", false, "Salud Mental" },
                    { 59, "Salidas de Ambulancia", false, "Salida Programada" }
                });

            migrationBuilder.InsertData(
                table: "TiposPrestacionOdontologia",
                columns: new[] { "Id", "IsDeleted", "NombrePrestacion" },
                values: new object[,]
                {
                    { 1, false, "Motivacion Orientación dx y terap." },
                    { 2, false, "Tto Farmacológico" },
                    { 3, false, "Enseñanza THO-Revelado de placa" },
                    { 4, false, "Topicación c/flúor" },
                    { 5, false, "Sellado fosas y fisuras" },
                    { 6, false, "Remineralización" },
                    { 7, false, "Inactivación de caries" },
                    { 8, false, "Tartrectomía" },
                    { 9, false, "Obturación provisoria" },
                    { 10, false, "Obturación definitiva" },
                    { 11, false, "Apertura/instrumentación de conducto" },
                    { 12, false, "Obturación unirradicular" },
                    { 13, false, "Obturación multirradicular" },
                    { 14, false, "Tto pulpar temporario" },
                    { 15, false, "Exodoncia temporario" },
                    { 16, false, "Exodoncia simple" },
                    { 17, false, "Exodoncia compleja" },
                    { 18, false, "Trat complicación post quirúrgica" },
                    { 19, false, "Rx. Intraoral" },
                    { 20, false, "Desgaste tejido dentario" },
                    { 21, false, "Paso intermedio prótesis" },
                    { 22, false, "Instalación de prótesis" },
                    { 23, false, "Inst placa de relaja/mucop" },
                    { 24, false, "Compostura / Agregado" },
                    { 25, false, "Alivio de prótesis" },
                    { 26, false, "Sol estudio complementario" },
                    { 27, false, "Derivación/Interconsulta" },
                    { 28, false, "Alta Básica" },
                    { 29, false, "Consultorio Estomatología" },
                    { 30, false, "Biopsia" },
                    { 31, false, "Consultorio Red Cirugía" },
                    { 32, false, "Otro" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 293);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 295);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 296);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 299);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 311);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 316);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 318);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 319);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 320);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 321);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 322);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 323);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 324);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 327);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 330);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 331);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 332);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 333);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 334);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 335);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 336);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 337);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 338);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 339);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 340);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 341);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 342);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 343);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 344);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 345);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 346);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 347);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 348);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 349);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 350);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 351);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 352);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 353);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 354);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 355);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 356);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 357);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 358);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 359);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 360);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 361);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 362);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 363);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 364);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 365);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 366);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 367);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 368);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 369);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 370);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 371);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 372);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 373);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 374);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 375);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 376);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 377);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 378);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 379);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 380);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 381);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 382);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 383);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 384);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 385);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 386);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 387);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 388);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 389);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 390);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 391);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 392);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 393);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 394);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 395);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 396);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 397);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 398);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 399);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 400);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 402);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 403);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 404);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 405);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 406);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 407);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 408);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 409);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 410);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 411);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 412);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 413);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 414);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 415);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 416);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 417);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 418);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 419);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 420);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 421);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 422);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 423);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 424);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 425);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 426);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 427);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 428);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 429);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 430);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 431);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 432);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 433);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 434);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 435);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 436);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 437);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 438);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 439);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 440);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 441);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 442);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 443);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 444);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 445);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 446);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 447);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 448);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 449);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 450);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 451);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 452);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 453);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 454);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 455);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 456);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 457);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 458);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 459);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 460);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 461);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 462);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 463);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 464);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 465);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 466);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 467);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 468);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 469);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 470);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 471);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 472);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 473);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 474);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 475);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 476);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 477);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 478);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 479);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 480);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 481);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 482);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 483);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 484);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 485);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 486);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 487);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 488);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 489);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 490);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 491);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 492);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 493);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 494);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 495);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 496);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 497);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 498);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 499);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 500);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 501);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 502);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 503);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 504);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 505);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 506);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 507);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 508);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 509);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 510);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 511);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 512);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 513);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 514);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 515);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 516);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 517);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 518);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 519);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 520);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 521);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 522);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 523);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 524);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 525);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 526);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 527);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 528);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 529);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 530);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 531);

            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "Id",
                keyValue: 532);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionEnfermeria",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "TiposPrestacionOdontologia",
                keyColumn: "Id",
                keyValue: 32);
        }
    }
}
