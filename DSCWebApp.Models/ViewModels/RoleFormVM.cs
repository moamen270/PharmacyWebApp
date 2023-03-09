using System.ComponentModel.DataAnnotations;

namespace DSCWebApp.Models.ViewModels
{
    public class RoleFormVM
    {
        [Required, StringLength(256)]
        public string Name { get; set; }
    }
}