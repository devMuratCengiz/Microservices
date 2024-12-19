using Microservices.Web.Models.Catalog.Category;
using Microservices.Web.Models.Catalog.Course;

namespace Microservices.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CourseViewModel>> GetAllCourseAsync();
        Task<List<CategoryViewModel>> GetAllCategoryAsync();
        Task<List<CourseViewModel>> GetAllCoursesByUserIdAsync(string userId);
        Task<CourseViewModel> GetByCourseId(string courseId);
        Task<bool> DeleteCourseAsync(string courseId);
        Task<bool> CreateCourseAsync(CreateCourseInput createCourseInput);
        Task<bool> UpdateCourseAsync(UpdateCourseInput updateCourseInput);
    }
}
