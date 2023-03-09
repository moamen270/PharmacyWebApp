using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DSCWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public byte[]? ProfilePicture { get; set; }
    }
}