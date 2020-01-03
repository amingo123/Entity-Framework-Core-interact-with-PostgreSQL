using Microsoft.EntityFrameworkCore.Migrations;

namespace Hexagon.UserManagement.EFCorePostgre.Migrations
{
    public partial class city3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "fk_capital_cities_id",
                table: "capital",
                column: "id",
                principalTable: "city",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_capital_cities_id",
                table: "capital");
        }
    }
}
