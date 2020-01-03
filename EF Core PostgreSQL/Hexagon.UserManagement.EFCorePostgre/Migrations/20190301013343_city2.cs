using Microsoft.EntityFrameworkCore.Migrations;

namespace Hexagon.UserManagement.EFCorePostgre.Migrations
{
    public partial class city2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_cities",
                table: "city");

            migrationBuilder.DropColumn(
                name: "other",
                table: "city");

            migrationBuilder.DropColumn(
                name: "discriminator",
                table: "city");

            migrationBuilder.AddPrimaryKey(
                name: "pk_city",
                table: "city",
                column: "id");

            migrationBuilder.CreateTable(
                name: "capital",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    other = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_capital", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "capital");

            migrationBuilder.DropPrimaryKey(
                name: "pk_city",
                table: "city");

            migrationBuilder.AddColumn<string>(
                name: "other",
                table: "city",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "discriminator",
                table: "city",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cities",
                table: "city",
                column: "id");
        }
    }
}
