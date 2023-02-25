using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyWebApp.Migrations
{
    public partial class EditPrescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Prescription");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "Prescription",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "Prescription");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Prescription",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
