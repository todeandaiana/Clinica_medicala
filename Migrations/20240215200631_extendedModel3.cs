using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinica_medicala.Migrations
{
    /// <inheritdoc />
    public partial class extendedModel3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpecializareID",
                table: "Servicii",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Specializari",
                columns: table => new
                {
                    SpecializareID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializari", x => x.SpecializareID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servicii_SpecializareID",
                table: "Servicii",
                column: "SpecializareID");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicii_Specializari_SpecializareID",
                table: "Servicii",
                column: "SpecializareID",
                principalTable: "Specializari",
                principalColumn: "SpecializareID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicii_Specializari_SpecializareID",
                table: "Servicii");

            migrationBuilder.DropTable(
                name: "Specializari");

            migrationBuilder.DropIndex(
                name: "IX_Servicii_SpecializareID",
                table: "Servicii");

            migrationBuilder.DropColumn(
                name: "SpecializareID",
                table: "Servicii");
        }
    }
}
