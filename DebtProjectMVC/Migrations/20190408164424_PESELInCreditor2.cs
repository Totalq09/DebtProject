using Microsoft.EntityFrameworkCore.Migrations;

namespace DebtProjectMVC.Migrations
{
    public partial class PESELInCreditor2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PESEL",
                table: "Creditor",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Creditor_PESEL",
                table: "Creditor",
                column: "PESEL",
                unique: true,
                filter: "[PESEL] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Creditor_PESEL",
                table: "Creditor");

            migrationBuilder.AlterColumn<string>(
                name: "PESEL",
                table: "Creditor",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
