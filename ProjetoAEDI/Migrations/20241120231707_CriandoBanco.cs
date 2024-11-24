using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjetoAEDI.Migrations
{
    /// <inheritdoc />
    public partial class CriandoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adotantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Contato = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Preferencias = table.Column<string>(type: "text", nullable: false),
                    HistoricoVisitas = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adotantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gatinhos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Idade = table.Column<int>(type: "integer", nullable: false),
                    Cor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Temperamento = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    RequisitosEspeciais = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gatinhos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Adocoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GatinhoId = table.Column<int>(type: "integer", nullable: false),
                    AdotanteId = table.Column<int>(type: "integer", nullable: false),
                    DataAdocao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adocoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adocoes_Adotantes_AdotanteId",
                        column: x => x.AdotanteId,
                        principalTable: "Adotantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adocoes_Gatinhos_GatinhoId",
                        column: x => x.GatinhoId,
                        principalTable: "Gatinhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adocoes_AdotanteId",
                table: "Adocoes",
                column: "AdotanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Adocoes_GatinhoId",
                table: "Adocoes",
                column: "GatinhoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adocoes");

            migrationBuilder.DropTable(
                name: "Adotantes");

            migrationBuilder.DropTable(
                name: "Gatinhos");
        }
    }
}
