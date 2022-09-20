using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyWebApp.Models
{
    public class CreditCard
    {
        public int id { get; set; }
        public string Number { get; set; }
        public string CCV { get; set; }
        
        public string UserId { get; set; }
        [ValidateNever]
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
                