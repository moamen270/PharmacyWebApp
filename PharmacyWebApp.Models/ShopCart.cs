﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyWebApp.Models
{
    public class ShopCart
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        [ValidateNever]
        public Product Product { get; set; }
        public string UserId { get; set; }
        [ValidateNever]
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

    }
}
