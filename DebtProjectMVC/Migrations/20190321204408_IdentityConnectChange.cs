using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DebtProjectMVC.Migrations
{
    public partial class IdentityConnectChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Creditor",
                nullable: true);    

            migrationBuilder.CreateIndex(
                name: "IX_Creditor_UserId",
                table: "Creditor",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Creditor_AspNetUsers_UserId",
                table: "Creditor",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creditor_AspNetUsers_UserId",
                table: "Creditor");

            migrationBuilder.DropIndex(
                name: "IX_Creditor_UserId",
                table: "Creditor");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Creditor");
        }
    }
}
