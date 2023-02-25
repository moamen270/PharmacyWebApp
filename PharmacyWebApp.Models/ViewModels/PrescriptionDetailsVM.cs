using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PharmacyWebApp.Models.ViewModels
{
    public class PrescriptionDetailsVM
    {
        public int Id { get; set; }
        public string Comment { get; set; } = "Empty";
        public int Dose { get; set; }
        public int Dosage { get; set; }
        public bool BeforeAfterMeal { get; set; }
        public int PrescriptionId { get; set; }

        [ValidateNever]
        public Prescription Prescription { get; set; }

        public int ProductId { get; set; }

        [ValidateNever]
        public Product Product { get; set; }
    }
}