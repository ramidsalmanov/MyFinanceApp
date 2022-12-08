using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFinance.Persistence.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BudgetItems_BudgetId",
                table: "BudgetItems");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetItems_BudgetId_CategoryId",
                table: "BudgetItems",
                columns: new[] { "BudgetId", "CategoryId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BudgetItems_BudgetId_CategoryId",
                table: "BudgetItems");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetItems_BudgetId",
                table: "BudgetItems",
                column: "BudgetId");
        }
    }
}
