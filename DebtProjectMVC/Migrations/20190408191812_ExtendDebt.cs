using Microsoft.EntityFrameworkCore.Migrations;

namespace DebtProjectMVC.Migrations
{
    public partial class ExtendDebt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Debt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PESEL",
                table: "Debt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Debt",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Debt");

            migrationBuilder.DropColumn(
                name: "PESEL",
                table: "Debt");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Debt");
        }
    }
}
