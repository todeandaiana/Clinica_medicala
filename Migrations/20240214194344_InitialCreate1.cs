using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinica_medicala.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Servicii",
                newName: "ServiciuID");

            migrationBuilder.CreateTable(
                name: "Medici",
                columns: table => new
                {
                    MedicID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medici", x => x.MedicID);
                });

            migrationBuilder.CreateTable(
                name: "ServiciiPrestate",
                columns: table => new
                {
                    MedicID = table.Column<int>(type: "int", nullable: false),
                    ServiciuID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiciiPrestate", x => new { x.ServiciuID, x.MedicID });
                    table.ForeignKey(
                        name: "FK_ServiciiPrestate_Medici_MedicID",
                        column: x => x.MedicID,
                        principalTable: "Medici",
                        principalColumn: "MedicID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiciiPrestate_Servicii_ServiciuID",
                        column: x => x.ServiciuID,
                        principalTable: "Servicii",
                        principalColumn: "ServiciuID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiciiPrestate_MedicID",
                table: "ServiciiPrestate",
                column: "MedicID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiciiPrestate");

            migrationBuilder.DropTable(
                name: "Medici");

            migrationBuilder.RenameColumn(
                name: "ServiciuID",
                table: "Servicii",
                newName: "ID");
        }
    }
}
