using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinica_medicala.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pacienti",
                columns: table => new
                {
                    PacientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNasterii = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacienti", x => x.PacientID);
                });

            migrationBuilder.CreateTable(
                name: "Servicii",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titlu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pret = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicii", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Programari",
                columns: table => new
                {
                    ProgramareID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacientID = table.Column<int>(type: "int", nullable: false),
                    ServiciuID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programari", x => x.ProgramareID);
                    table.ForeignKey(
                        name: "FK_Programari_Pacienti_PacientID",
                        column: x => x.PacientID,
                        principalTable: "Pacienti",
                        principalColumn: "PacientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Programari_Servicii_ServiciuID",
                        column: x => x.ServiciuID,
                        principalTable: "Servicii",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Programari_PacientID",
                table: "Programari",
                column: "PacientID");

            migrationBuilder.CreateIndex(
                name: "IX_Programari_ServiciuID",
                table: "Programari",
                column: "ServiciuID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Programari");

            migrationBuilder.DropTable(
                name: "Pacienti");

            migrationBuilder.DropTable(
                name: "Servicii");
        }
    }
}
