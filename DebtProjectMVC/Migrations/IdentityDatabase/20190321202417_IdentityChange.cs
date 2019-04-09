using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DebtProjectMVC.Migrations.IdentityDatabase
{
    public partial class IdentityChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Creditor_CreditorId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CreditorId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreditorId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditorId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CreditorId",
                table: "AspNetUsers",
                column: "CreditorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Creditor_CreditorId",
                table: "AspNetUsers",
                column: "CreditorId",
                principalTable: "Creditor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
