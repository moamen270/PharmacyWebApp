using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PharmacyWebApp.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Range(1, 5)]
        [Required]
        public double Rate { get; set; }

        public string? Comment { get; set; }
        public DateTime AddedDateTime { get; set; } = DateTime.Now;
        public int ProductId { get; set; }

        [ValidateNever]
        public Product Product { get; set; }

        public string UserId { get; set; }

        [ValidateNever]
        public ApplicationUser User { get; set; }
    }
}