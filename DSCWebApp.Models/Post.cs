using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DSCWebApp.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = "New Post Title";
        public string Description { get; set; } = "New Post Description";
        public byte[]? PostPicture { get; set; }
        public int CategoryId { get; set; }

        [ValidateNever]
        public Category Category { get; set; }
    }
}