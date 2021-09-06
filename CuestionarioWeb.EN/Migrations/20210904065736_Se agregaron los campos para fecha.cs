using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CuestionarioWeb.EN.Migrations
{
    public partial class Seagregaronloscamposparafecha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaDeRespuesta",
                table: "Respuestas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaDePregunta",
                table: "Preguntas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaDeRespuesta",
                table: "Respuestas");

            migrationBuilder.DropColumn(
                name: "FechaDePregunta",
                table: "Preguntas");
        }
    }
}
