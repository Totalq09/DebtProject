using Microsoft.EntityFrameworkCore.Migrations;

namespace DebtProjectMVC.Migrations
{
    public partial class LinkCreditorToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Borrower_PESEL",
                table: "Borrower");

            migrationBuilder.AddColumn<int>(
                name: "CreditorId",
                table: "Borrower",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Borrower_PESEL",
                table: "Borrower",
                column: "PESEL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Borrower_PESEL",
                table: "Borrower");

            migrationBuilder.DropColumn(
                name: "CreditorId",
                table: "Borrower");

            migrationBuilder.CreateIndex(
                name: "IX_Borrower_PESEL",
                table: "Borrower",
                column: "PESEL",
                unique: true,
                filter: "[PESEL] IS NOT NULL");
        }
    }
}
