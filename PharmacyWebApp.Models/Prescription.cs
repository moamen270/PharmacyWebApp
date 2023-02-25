using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyWebApp.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public DateTime CreationDateTime { get; set; } = DateTime.Now;
        public bool dispensing { get; set; } = false;

        [ForeignKey("Patient")]
        public string PatientId { get; set; }

        public ApplicationUser Patient { get; set; }

        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }

        public ApplicationUser Doctor { get; set; }
    }
}