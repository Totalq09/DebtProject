using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DebtProjectMVC.Migrations
{
    public partial class RemoveBorrower : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debt_Borrower_BorrowerId",
                table: "Debt");

            migrationBuilder.DropTable(
                name: "Borrower");

            migrationBuilder.DropIndex(
                name: "IX_Debt_BorrowerId",
                table: "Debt");

            migrationBuilder.DropColumn(
                name: "BorrowerId",
                table: "Debt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BorrowerId",
                table: "Debt",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Borrower",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreditorId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PESEL = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrower", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Borrower_Creditor_CreditorId",
                        column: x => x.CreditorId,
                        principalTable: "Creditor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Debt_BorrowerId",
                table: "Debt",
                column: "BorrowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrower_CreditorId",
                table: "Borrower",
                column: "CreditorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Borrower_PESEL",
                table: "Borrower",
                column: "PESEL");

            migrationBuilder.AddForeignKey(
                name: "FK_Debt_Borrower_BorrowerId",
                table: "Debt",
                column: "BorrowerId",
                principalTable: "Borrower",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
