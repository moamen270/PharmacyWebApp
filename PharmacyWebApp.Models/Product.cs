using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PharmacyWebApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "New Product";
        public string Description { get; set; } = "New Product Description";

        [Range(1, 1000)]
        public double ListPrice { get; set; } = 1;

        [Range(1, 1000)]
        public double Price { get; set; } = 1;

        public byte[]? ProductPicture { get; set; }
        public int? BrandId { get; set; }

        [ValidateNever]
        public Brand? Brand { get; set; }

        public int? CategoryId { get; set; }

        [ValidateNever]
        public Category? Category { get; set; }

        public IEnumerable<Review>? Reviews { get; set; }
    }
}