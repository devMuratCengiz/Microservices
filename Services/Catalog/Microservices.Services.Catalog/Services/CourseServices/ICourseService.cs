using Microservice.Shared.Dtos;
using Microservices.Services.Catalog.Dtos.CourseDtos;

namespace Microservices.Services.Catalog.Services.CourseServices
{
    public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<CourseDto>> GetByIdAsync(string id);
        Task<Response<List<CourseDto>>> GetAllCoursesByUserIdAsync(string userId);
        Task<Response<CourseDto>> CreateAsync(CreateCourseDto createCourseDto);
        Task<Response<NoContent>> UpdateAsync(UpdateCourseDto updateCourseDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
