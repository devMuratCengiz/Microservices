using Microservice.Shared.Dtos;
using Microservices.Services.Catalog.Dtos.CategoryDtos;

namespace Microservices.Services.Catalog.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CreateCategoryDto createCategoryDto);
        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}
