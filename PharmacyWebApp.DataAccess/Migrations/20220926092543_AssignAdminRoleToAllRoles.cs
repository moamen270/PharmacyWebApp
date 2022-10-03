using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyWebApp.Migrations
{
    public partial class AssignAdminRoleToAllRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[UserRole] (UserId,RoleId) SELECT '4d507749-d6e1-408f-bd38-db3ad6fe7199', Id FROM [dbo].[Roles]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[UserRole] WHERE UserId = '4d507749-d6e1-408f-bd38-db3ad6fe7199' ");
        }
    }
}