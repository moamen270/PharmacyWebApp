using System.ComponentModel.DataAnnotations;

namespace PharmacyWebApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ParCode { get; set; }
        public string Description { get; set; }
        [Range(1,1000)]
        public double ListPrice { get; set; }
        [Range(1, 1000)]
        public double Price { get; set; }

    }
}
