using Microsoft.EntityFrameworkCore.Migrations;

namespace Hexagon.UserManagement.EFCorePostgre.Migrations
{
    public partial class city : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    cityname = table.Column<string>(nullable: true),
                    discriminator = table.Column<string>(nullable: false),
                    other = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cities", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "city");
        }
    }
}
