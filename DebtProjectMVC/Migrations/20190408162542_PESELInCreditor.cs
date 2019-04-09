using Microsoft.EntityFrameworkCore.Migrations;

namespace DebtProjectMVC.Migrations
{
    public partial class PESELInCreditor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PESEL",
                table: "Creditor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PESEL",
                table: "Creditor");
        }
    }
}
