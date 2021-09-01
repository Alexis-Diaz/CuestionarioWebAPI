using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CuestionarioWeb.EN.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reacciones",
                columns: table => new
                {
                    IdReaccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoReaccion = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reacciones", x => x.IdReaccion);
                });

            migrationBuilder.CreateTable(
                name: "RolUsuarios",
                columns: table => new
                {
                    IdRolUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoRolUsuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolUsuarios", x => x.IdRolUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdRolUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_RolUsuarios_IdRolUsuario",
                        column: x => x.IdRolUsuario,
                        principalTable: "RolUsuarios",
                        principalColumn: "IdRolUsuario",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Preguntas",
                columns: table => new
                {
                    IdPregunta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreguntaFormulada = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Estado = table.Column<byte>(type: "tinyint", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preguntas", x => x.IdPregunta);
                    table.ForeignKey(
                        name: "FK_Preguntas_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Respuestas",
                columns: table => new
                {
                    IdRespuesta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutoReferencia = table.Column<int>(type: "int", nullable: false),
                    RespuestaEmitida = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdPregunta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuestas", x => x.IdRespuesta);
                    table.ForeignKey(
                        name: "FK_Respuestas_Preguntas_IdPregunta",
                        column: x => x.IdPregunta,
                        principalTable: "Preguntas",
                        principalColumn: "IdPregunta",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Respuestas_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ReaccionUsuarioRespuestas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdReaccion = table.Column<int>(type: "int", nullable: false),
                    IdRespuesta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReaccionUsuarioRespuestas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReaccionUsuarioRespuestas_Reacciones_IdReaccion",
                        column: x => x.IdReaccion,
                        principalTable: "Reacciones",
                        principalColumn: "IdReaccion",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ReaccionUsuarioRespuestas_Respuestas_IdRespuesta",
                        column: x => x.IdRespuesta,
                        principalTable: "Respuestas",
                        principalColumn: "IdRespuesta",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ReaccionUsuarioRespuestas_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Preguntas_IdUsuario",
                table: "Preguntas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ReaccionUsuarioRespuestas_IdReaccion",
                table: "ReaccionUsuarioRespuestas",
                column: "IdReaccion");

            migrationBuilder.CreateIndex(
                name: "IX_ReaccionUsuarioRespuestas_IdRespuesta",
                table: "ReaccionUsuarioRespuestas",
                column: "IdRespuesta");

            migrationBuilder.CreateIndex(
                name: "IX_ReaccionUsuarioRespuestas_IdUsuario",
                table: "ReaccionUsuarioRespuestas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_IdPregunta",
                table: "Respuestas",
                column: "IdPregunta");

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_IdUsuario",
                table: "Respuestas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdRolUsuario",
                table: "Usuarios",
                column: "IdRolUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReaccionUsuarioRespuestas");

            migrationBuilder.DropTable(
                name: "Reacciones");

            migrationBuilder.DropTable(
                name: "Respuestas");

            migrationBuilder.DropTable(
                name: "Preguntas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "RolUsuarios");
        }
    }
}
