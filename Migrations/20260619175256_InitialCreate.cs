using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtencionesApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Diagnosticos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnosticos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Instituciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodigoSISA = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dependencia = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipologia = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SitioWeb = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituciones", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Apellido = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DNI = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Sexo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Domicilio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefono = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ObraSocial = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PuedeCrear = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PuedeLeer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PuedeEditar = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PuedeEliminar = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TiposPrestacionEnfermeria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Grupo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NombrePrestacion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPrestacionEnfermeria", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TiposPrestacionOdontologia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombrePrestacion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPrestacionOdontologia", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Apellido = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefono = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AtencionesEnfermeria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    TipoAtencion = table.Column<int>(type: "int", nullable: false),
                    Embarazada = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SinObraSocial = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    InstitucionId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtencionesEnfermeria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AtencionesEnfermeria_Instituciones_InstitucionId",
                        column: x => x.InstitucionId,
                        principalTable: "Instituciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtencionesEnfermeria_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtencionesEnfermeria_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AtencionesOdontologia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    TipoConsulta = table.Column<int>(type: "int", nullable: false),
                    TipoTurno = table.Column<int>(type: "int", nullable: false),
                    Embarazada = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SinObraSocial = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    InstitucionId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    DiagnosticoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtencionesOdontologia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AtencionesOdontologia_Diagnosticos_DiagnosticoId",
                        column: x => x.DiagnosticoId,
                        principalTable: "Diagnosticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtencionesOdontologia_Instituciones_InstitucionId",
                        column: x => x.InstitucionId,
                        principalTable: "Instituciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtencionesOdontologia_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtencionesOdontologia_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InstitucionUsuario",
                columns: table => new
                {
                    InstitucionesId = table.Column<int>(type: "int", nullable: false),
                    UsuariosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitucionUsuario", x => new { x.InstitucionesId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_InstitucionUsuario_Instituciones_InstitucionesId",
                        column: x => x.InstitucionesId,
                        principalTable: "Instituciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstitucionUsuario_Usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PrestacionesEnfermeria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AtencionId = table.Column<int>(type: "int", nullable: false),
                    TipoPrestacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrestacionesEnfermeria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrestacionesEnfermeria_AtencionesEnfermeria_AtencionId",
                        column: x => x.AtencionId,
                        principalTable: "AtencionesEnfermeria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrestacionesEnfermeria_TiposPrestacionEnfermeria_TipoPrestac~",
                        column: x => x.TipoPrestacionId,
                        principalTable: "TiposPrestacionEnfermeria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PrestacionesOdontologia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AtencionId = table.Column<int>(type: "int", nullable: false),
                    TipoPrestacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrestacionesOdontologia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrestacionesOdontologia_AtencionesOdontologia_AtencionId",
                        column: x => x.AtencionId,
                        principalTable: "AtencionesOdontologia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrestacionesOdontologia_TiposPrestacionOdontologia_TipoPrest~",
                        column: x => x.TipoPrestacionId,
                        principalTable: "TiposPrestacionOdontologia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ValoracionesDentales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CariesPerm = table.Column<int>(type: "int", nullable: false),
                    PerdidosPerm = table.Column<int>(type: "int", nullable: false),
                    ObturadosPerm = table.Column<int>(type: "int", nullable: false),
                    CariesTemp = table.Column<int>(type: "int", nullable: false),
                    ExtraccionTemp = table.Column<int>(type: "int", nullable: false),
                    ObturadosTemp = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AtencionOdontologiaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValoracionesDentales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValoracionesDentales_AtencionesOdontologia_AtencionOdontolog~",
                        column: x => x.AtencionOdontologiaId,
                        principalTable: "AtencionesOdontologia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AtencionesEnfermeria_InstitucionId",
                table: "AtencionesEnfermeria",
                column: "InstitucionId");

            migrationBuilder.CreateIndex(
                name: "IX_AtencionesEnfermeria_PacienteId",
                table: "AtencionesEnfermeria",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AtencionesEnfermeria_UsuarioId",
                table: "AtencionesEnfermeria",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AtencionesOdontologia_DiagnosticoId",
                table: "AtencionesOdontologia",
                column: "DiagnosticoId");

            migrationBuilder.CreateIndex(
                name: "IX_AtencionesOdontologia_InstitucionId",
                table: "AtencionesOdontologia",
                column: "InstitucionId");

            migrationBuilder.CreateIndex(
                name: "IX_AtencionesOdontologia_PacienteId",
                table: "AtencionesOdontologia",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AtencionesOdontologia_UsuarioId",
                table: "AtencionesOdontologia",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_InstitucionUsuario_UsuariosId",
                table: "InstitucionUsuario",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_PrestacionesEnfermeria_AtencionId",
                table: "PrestacionesEnfermeria",
                column: "AtencionId");

            migrationBuilder.CreateIndex(
                name: "IX_PrestacionesEnfermeria_TipoPrestacionId",
                table: "PrestacionesEnfermeria",
                column: "TipoPrestacionId");

            migrationBuilder.CreateIndex(
                name: "IX_PrestacionesOdontologia_AtencionId",
                table: "PrestacionesOdontologia",
                column: "AtencionId");

            migrationBuilder.CreateIndex(
                name: "IX_PrestacionesOdontologia_TipoPrestacionId",
                table: "PrestacionesOdontologia",
                column: "TipoPrestacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_ValoracionesDentales_AtencionOdontologiaId",
                table: "ValoracionesDentales",
                column: "AtencionOdontologiaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstitucionUsuario");

            migrationBuilder.DropTable(
                name: "PrestacionesEnfermeria");

            migrationBuilder.DropTable(
                name: "PrestacionesOdontologia");

            migrationBuilder.DropTable(
                name: "ValoracionesDentales");

            migrationBuilder.DropTable(
                name: "AtencionesEnfermeria");

            migrationBuilder.DropTable(
                name: "TiposPrestacionEnfermeria");

            migrationBuilder.DropTable(
                name: "TiposPrestacionOdontologia");

            migrationBuilder.DropTable(
                name: "AtencionesOdontologia");

            migrationBuilder.DropTable(
                name: "Diagnosticos");

            migrationBuilder.DropTable(
                name: "Instituciones");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
