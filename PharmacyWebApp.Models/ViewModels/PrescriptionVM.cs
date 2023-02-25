using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PharmacyWebApp.Models.ViewModels
{
    public class PrescriptionVM
    {
        public int Id { get; set; }
        public DateTime CreationDateTime { get; set; } = DateTime.Now;
        public bool dispensing { get; set; } = false;
        public string PatientId { get; set; }

        [ValidateNever]
        public ApplicationUser Patient { get; set; }

        public string DoctorId { get; set; }

        [ValidateNever]
        public ApplicationUser Doctor { get; set; }
        [ValidateNever]
        public IEnumerable<ApplicationUser> Patients { get; set; }
    }
}