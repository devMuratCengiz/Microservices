using Microservice.Shared.ControllerBases;
using Microservices.Services.Catalog.Dtos.CourseDtos;
using Microservices.Services.Catalog.Services.CourseServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : CustomBaseController
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseService.GetAllAsync();
            return CreateActionResultInstance(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _courseService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }
        [HttpGet]
        [Route("/api/[controller]/GetAllCoursesByUserId/{userId}")]
        public async Task<IActionResult> GetAllCoursesByUserId(string userId)
        {
            var response = await _courseService.GetAllCoursesByUserIdAsync(userId);
            return CreateActionResultInstance(response);
        }
        [HttpPost]
        public async Task<IActionResult>Create(CreateCourseDto createCourseDto)
        {
            var response = await _courseService.CreateAsync(createCourseDto);
            return CreateActionResultInstance(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCourseDto updateCourseDto)
        {
            var response = await _courseService.UpdateAsync(updateCourseDto);
            return CreateActionResultInstance(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _courseService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }
    }
}
