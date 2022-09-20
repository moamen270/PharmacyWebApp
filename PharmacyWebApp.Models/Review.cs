using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyWebApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Range(0,5)]
        public int Rate { get; set; }
        public string? Comment { get; set; }
        public DateTime AddedDateTime { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        [ValidateNever]
        public Product Product { get; set; }
        public string UserId { get; set; }
        [ValidateNever]
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
