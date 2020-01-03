using Microsoft.EntityFrameworkCore.Migrations;

namespace Hexagon.UserManagement.EFCorePostgre.Migrations
{
    public partial class initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Realms_RealmId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "AdminUsers");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RealmId",
                table: "AdminUsers",
                newName: "IX_AdminUsers_RealmId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdminUsers",
                table: "AdminUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminUsers_Realms_RealmId",
                table: "AdminUsers",
                column: "RealmId",
                principalTable: "Realms",
                principalColumn: "RlmId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminUsers_Realms_RealmId",
                table: "AdminUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdminUsers",
                table: "AdminUsers");

            migrationBuilder.RenameTable(
                name: "AdminUsers",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_AdminUsers_RealmId",
                table: "Users",
                newName: "IX_Users_RealmId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Realms_RealmId",
                table: "Users",
                column: "RealmId",
                principalTable: "Realms",
                principalColumn: "RlmId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
