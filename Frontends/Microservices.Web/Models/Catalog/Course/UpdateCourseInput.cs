﻿using Microservices.Web.Models.Catalog.Feature;
using System.ComponentModel.DataAnnotations;

namespace Microservices.Web.Models.Catalog.Course
{
    public class UpdateCourseInput
    {
        public string Id { get; set; }
        [Display(Name = "Kurs İsmi")]
        public string Name { get; set; }
        [Display(Name = "Kurs Fiyatı")]
        public decimal Price { get; set; }
        [Display(Name = "Kurs Açıklaması")]
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public FeatureViewModel Feature { get; set; }
        [Display(Name = "Kurs Kategori")]
        public string CategoryId { get; set; }
        [Display(Name = "Kurs Resmi")]
        public IFormFile PhotoFormFile { get; set; }
    }
}
