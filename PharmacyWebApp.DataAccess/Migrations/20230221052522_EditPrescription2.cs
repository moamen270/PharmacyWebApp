using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyWebApp.Migrations
{
    public partial class EditPrescription2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DoseRate",
                table: "PrescriptionDetail",
                newName: "Dosage");

            migrationBuilder.RenameColumn(
                name: "BeforeAfter",
                table: "PrescriptionDetail",
                newName: "BeforeAfterMeal");

            migrationBuilder.AddColumn<bool>(
                name: "dispensing",
                table: "Prescription",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dispensing",
                table: "Prescription");

            migrationBuilder.RenameColumn(
                name: "Dosage",
                table: "PrescriptionDetail",
                newName: "DoseRate");

            migrationBuilder.RenameColumn(
                name: "BeforeAfterMeal",
                table: "PrescriptionDetail",
                newName: "BeforeAfter");
        }
    }
}
