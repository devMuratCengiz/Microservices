using Microservice.Shared.Dtos;
using Microservices.Web.Helpers;
using Microservices.Web.Models.Catalog.Category;
using Microservices.Web.Models.Catalog.Course;
using Microservices.Web.Services.Interfaces;

namespace Microservices.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<bool> CreateCourseAsync(CreateCourseInput createCourseInput)
        {
            var resultPhoto = await _photoStockService.UploadPhoto(createCourseInput.PhotoFormFile);
            if (resultPhoto != null)
            {
                createCourseInput.Picture = resultPhoto.Url;
            }
            var response = await _httpClient.PostAsJsonAsync<CreateCourseInput>("courses", createCourseInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"courses/{courseId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _httpClient.GetAsync("categories");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();
            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            var response = await _httpClient.GetAsync("courses");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
            responseSuccess.Data.ForEach(x =>
            {
                x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
            });
            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCoursesByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"courses/GetAllCoursesByUserId/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            responseSuccess.Data.ForEach(x =>
            {
                x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
            });
            return responseSuccess.Data;
        }

        public async Task<CourseViewModel> GetByCourseId(string courseId)
        {
            var response = await _httpClient.GetAsync($"courses/{courseId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            responseSuccess.Data.StockPictureUrl = _photoHelper.GetPhotoStockUrl(responseSuccess.Data.Picture);

            return responseSuccess.Data;
        }

        public async Task<bool> UpdateCourseAsync(UpdateCourseInput updateCourseInput)
        {
            var resultPhoto = await _photoStockService.UploadPhoto(updateCourseInput.PhotoFormFile);
            if (resultPhoto != null)
            {
                await _photoStockService.DeletePhoto(updateCourseInput.Picture);
                updateCourseInput.Picture = resultPhoto.Url;
            }
            var response = await _httpClient.PutAsJsonAsync<UpdateCourseInput>("courses", updateCourseInput);
            return response.IsSuccessStatusCode;
        }
    }
}
