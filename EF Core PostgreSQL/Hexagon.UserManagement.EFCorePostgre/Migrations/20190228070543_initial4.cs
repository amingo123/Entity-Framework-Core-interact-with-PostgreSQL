using Microsoft.EntityFrameworkCore.Migrations;

namespace Hexagon.UserManagement.EFCorePostgre.Migrations
{
    public partial class initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminUsers_Realms_RealmId",
                table: "AdminUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_RealmSolution_Realms_RlmId",
                table: "RealmSolution");

            migrationBuilder.DropForeignKey(
                name: "FK_RealmSolution_Solutions_SlnId",
                table: "RealmSolution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RealmSolution",
                table: "RealmSolution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Solutions",
                table: "Solutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Realms",
                table: "Realms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdminUsers",
                table: "AdminUsers");

            migrationBuilder.RenameTable(
                name: "RealmSolution",
                newName: "realmsolution");

            migrationBuilder.RenameTable(
                name: "Solutions",
                newName: "solution");

            migrationBuilder.RenameTable(
                name: "Realms",
                newName: "realm");

            migrationBuilder.RenameTable(
                name: "AdminUsers",
                newName: "adminuser");

            migrationBuilder.RenameColumn(
                name: "SlnId",
                table: "realmsolution",
                newName: "slnid");

            migrationBuilder.RenameColumn(
                name: "RlmId",
                table: "realmsolution",
                newName: "rlmid");

            migrationBuilder.RenameIndex(
                name: "IX_RealmSolution_SlnId",
                table: "realmsolution",
                newName: "ix_realmsolution_slnid");

            migrationBuilder.RenameColumn(
                name: "SolutionName",
                table: "solution",
                newName: "solutionname");

            migrationBuilder.RenameColumn(
                name: "SlnId",
                table: "solution",
                newName: "slnid");

            migrationBuilder.RenameColumn(
                name: "RealmName",
                table: "realm",
                newName: "realmname");

            migrationBuilder.RenameColumn(
                name: "RlmId",
                table: "realm",
                newName: "rlmid");

            migrationBuilder.RenameColumn(
                name: "TokenExpireTime",
                table: "adminuser",
                newName: "tokenexpiretime");

            migrationBuilder.RenameColumn(
                name: "RealmId",
                table: "adminuser",
                newName: "realmid");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "adminuser",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "adminuser",
                newName: "gender");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "adminuser",
                newName: "age");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "adminuser",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_AdminUsers_RealmId",
                table: "adminuser",
                newName: "ix_adminuser_realmid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_realmsolution",
                table: "realmsolution",
                columns: new[] { "rlmid", "slnid" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_solution",
                table: "solution",
                column: "slnid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_realm",
                table: "realm",
                column: "rlmid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_adminuser",
                table: "adminuser",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_adminuser_realms_realmid",
                table: "adminuser",
                column: "realmid",
                principalTable: "realm",
                principalColumn: "rlmid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_realmsolution_realm_rlmid",
                table: "realmsolution",
                column: "rlmid",
                principalTable: "realm",
                principalColumn: "rlmid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_realmsolution_solutions_slnid",
                table: "realmsolution",
                column: "slnid",
                principalTable: "solution",
                principalColumn: "slnid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_adminuser_realms_realmid",
                table: "adminuser");

            migrationBuilder.DropForeignKey(
                name: "fk_realmsolution_realm_rlmid",
                table: "realmsolution");

            migrationBuilder.DropForeignKey(
                name: "fk_realmsolution_solutions_slnid",
                table: "realmsolution");

            migrationBuilder.DropPrimaryKey(
                name: "pk_realmsolution",
                table: "realmsolution");

            migrationBuilder.DropPrimaryKey(
                name: "pk_solution",
                table: "solution");

            migrationBuilder.DropPrimaryKey(
                name: "pk_realm",
                table: "realm");

            migrationBuilder.DropPrimaryKey(
                name: "pk_adminuser",
                table: "adminuser");

            migrationBuilder.RenameTable(
                name: "realmsolution",
                newName: "RealmSolution");

            migrationBuilder.RenameTable(
                name: "solution",
                newName: "Solutions");

            migrationBuilder.RenameTable(
                name: "realm",
                newName: "Realms");

            migrationBuilder.RenameTable(
                name: "adminuser",
                newName: "AdminUsers");

            migrationBuilder.RenameColumn(
                name: "slnid",
                table: "RealmSolution",
                newName: "SlnId");

            migrationBuilder.RenameColumn(
                name: "rlmid",
                table: "RealmSolution",
                newName: "RlmId");

            migrationBuilder.RenameIndex(
                name: "ix_realmsolution_slnid",
                table: "RealmSolution",
                newName: "IX_RealmSolution_SlnId");

            migrationBuilder.RenameColumn(
                name: "solutionname",
                table: "Solutions",
                newName: "SolutionName");

            migrationBuilder.RenameColumn(
                name: "slnid",
                table: "Solutions",
                newName: "SlnId");

            migrationBuilder.RenameColumn(
                name: "realmname",
                table: "Realms",
                newName: "RealmName");

            migrationBuilder.RenameColumn(
                name: "rlmid",
                table: "Realms",
                newName: "RlmId");

            migrationBuilder.RenameColumn(
                name: "tokenexpiretime",
                table: "AdminUsers",
                newName: "TokenExpireTime");

            migrationBuilder.RenameColumn(
                name: "realmid",
                table: "AdminUsers",
                newName: "RealmId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "AdminUsers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "gender",
                table: "AdminUsers",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "age",
                table: "AdminUsers",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AdminUsers",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ix_adminuser_realmid",
                table: "AdminUsers",
                newName: "IX_AdminUsers_RealmId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RealmSolution",
                table: "RealmSolution",
                columns: new[] { "RlmId", "SlnId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Solutions",
                table: "Solutions",
                column: "SlnId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Realms",
                table: "Realms",
                column: "RlmId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_RealmSolution_Realms_RlmId",
                table: "RealmSolution",
                column: "RlmId",
                principalTable: "Realms",
                principalColumn: "RlmId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RealmSolution_Solutions_SlnId",
                table: "RealmSolution",
                column: "SlnId",
                principalTable: "Solutions",
                principalColumn: "SlnId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
