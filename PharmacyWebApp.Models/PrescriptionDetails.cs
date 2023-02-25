using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyWebApp.Models
{
    public class PrescriptionDetails
    {
        public int Id { get; set; }
        public string Comment { get; set; } = "Empty";
        public int Dose { get; set; }
        public int Dosage { get; set; }
        public bool BeforeAfterMeal { get; set; }

        [ForeignKey("Prescription")]
        public int PrescriptionId { get; set; }

        public Prescription Prescription { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}