using AutoMapper;
using Microservices.Services.Catalog.Dtos.CategoryDtos;
using Microservices.Services.Catalog.Dtos.CourseDtos;
using Microservices.Services.Catalog.Dtos.FeatureDtos;
using Microservices.Services.Catalog.Models;

namespace Microservices.Services.Catalog.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Course, CreateCourseDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, UpdateCourseDto>().ReverseMap();

            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();

            CreateMap<Feature, CreateFeatureDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();
            CreateMap<Feature, UpdateFeatureDto>().ReverseMap();
        }
    }
}
