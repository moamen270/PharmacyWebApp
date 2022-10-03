using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyWebApp.Models
{
    public class ShopCart
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]
        public int ProductId { get; set; }

        [ValidateNever]
        public Product Product { get; set; }

        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
        public int Count { get; set; }

        public string UserId { get; set; }

        [ValidateNever]
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public double Price { get; set; }
    }
}