using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyWebApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderState { get; set; }
        public DateTime AddedDateTime { get; set; }
        public DateTime RequiredDateTime { get; set; }
        public double TotalPrice { get; set; }        
        public int ProductId { get; set; }
        [ValidateNever]
        public Product Product { get; set; }
        public string UserId { get; set; }
        [ValidateNever]
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
