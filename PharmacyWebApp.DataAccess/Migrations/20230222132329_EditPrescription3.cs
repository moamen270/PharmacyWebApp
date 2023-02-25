using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyWebApp.Migrations
{
    public partial class EditPrescription3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Users_PationtId",
                table: "Prescription");

            migrationBuilder.RenameColumn(
                name: "PationtId",
                table: "Prescription",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Prescription_PationtId",
                table: "Prescription",
                newName: "IX_Prescription_PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Users_PatientId",
                table: "Prescription",
                column: "PatientId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Users_PatientId",
                table: "Prescription");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Prescription",
                newName: "PationtId");

            migrationBuilder.RenameIndex(
                name: "IX_Prescription_PatientId",
                table: "Prescription",
                newName: "IX_Prescription_PationtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Users_PationtId",
                table: "Prescription",
                column: "PationtId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
