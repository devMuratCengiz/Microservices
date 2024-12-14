﻿using Microservices.Services.Catalog.Dtos.FeatureDtos;

namespace Microservices.Services.Catalog.Dtos.CourseDtos
{
    public class CreateCourseDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public FeatureDto Feature { get; set; }
        public string CategoryId { get; set; }
    }
}
