using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyWebApp.Models.ViewModels
{
    public class RoleFormVM
    {
        [Required,StringLength(256)]

        public string Name { get; set; }
    }
}
