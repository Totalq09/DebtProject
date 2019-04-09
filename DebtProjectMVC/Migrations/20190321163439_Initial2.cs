using Microsoft.EntityFrameworkCore.Migrations;

namespace DebtProjectMVC.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Debt",
                nullable: false,
                defaultValue: "Open",
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Debt",
                nullable: false,
                oldClrType: typeof(string),
                oldDefaultValue: "Open");
        }
    }
}
