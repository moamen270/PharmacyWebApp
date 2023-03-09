using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DSCWebApp.Models.ViewModels
{
    public class PostVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public byte[]? PostPicture { get; set; }

        public int CategoryId { get; set; }

        [ValidateNever]
        public Category Category { get; set; }

        [ValidateNever]
        public IEnumerable<Category> Categories { get; set; }
    }
}