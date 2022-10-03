using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyWebApp.Migrations
{
    public partial class AddAdminUser1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [City], [LastName], [PostalCode], [State], [StreetAddress], [FirstName], [ProfilePicture]) VALUES (N'4d507749-d6e1-408f-bd38-db3ad6fe7199', N'AdminMohamed@admin.com', N'ADMINMOHAMED@ADMIN.COM', N'AdminMohamed@admin.com', N'ADMINMOHAMED@ADMIN.COM', 0, N'AQAAAAEAACcQAAAAEJNLaum9h88JDE6lTg0jDvinduBQ+xgemb696r6t0KfA07ojNa9EUoHaV65LK0s7vg==', N'X7QMDNKD3WH7EA42Z6QNX33E7O3S3NFJ', N'58041c13-5d90-4c5c-a537-0acf3d7f15d3', N'01210675789', 0, 0, NULL, 1, 0, N'Alexandria', N'Hazem', N'25255255', N'Alexandria', N'Elflaky', N'Mohamed', NULL)\r\n");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[Users] WHERE Id = '4d507749-d6e1-408f-bd38-db3ad6fe7199' ");
        }
    }
}