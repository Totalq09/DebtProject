using Microsoft.EntityFrameworkCore.Migrations;

namespace DebtProjectMVC.Migrations
{
    public partial class LinkCreditorToUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Borrower_CreditorId",
                table: "Borrower",
                column: "CreditorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Borrower_Creditor_CreditorId",
                table: "Borrower",
                column: "CreditorId",
                principalTable: "Creditor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrower_Creditor_CreditorId",
                table: "Borrower");

            migrationBuilder.DropIndex(
                name: "IX_Borrower_CreditorId",
                table: "Borrower");
        }
    }
}
