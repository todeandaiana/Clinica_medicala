using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinica_medicala.Migrations
{
    /// <inheritdoc />
    public partial class Model1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicii_Specializari_SpecializareID",
                table: "Servicii");

            migrationBuilder.AlterColumn<int>(
                name: "SpecializareID",
                table: "Servicii",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicii_Specializari_SpecializareID",
                table: "Servicii",
                column: "SpecializareID",
                principalTable: "Specializari",
                principalColumn: "SpecializareID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicii_Specializari_SpecializareID",
                table: "Servicii");

            migrationBuilder.AlterColumn<int>(
                name: "SpecializareID",
                table: "Servicii",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicii_Specializari_SpecializareID",
                table: "Servicii",
                column: "SpecializareID",
                principalTable: "Specializari",
                principalColumn: "SpecializareID");
        }
    }
}
