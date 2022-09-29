﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PharmacyWebApp.Models.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        [Range(1, 1000)]
        public double ListPrice { get; set; } = 1;
        [Range(1, 1000)]
        public double Price { get; set; } = 1;
        public byte[]? ProductPicture { get; set; }
        [Range(0,5)]
        public int Rate { get; set; }
        public string? Comment { get; set; }
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        [ValidateNever]
         public IEnumerable<Brand> Brands { get; set; }
        [ValidateNever]
        public IEnumerable<Category> Categories { get; set; }
        [ValidateNever]
        public IEnumerable<Review> Reviews { get; set; }


    }

}
