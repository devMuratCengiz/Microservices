using Microservice.Shared.ControllerBases;
using Microservices.Services.Catalog.Dtos.CategoryDtos;
using Microservices.Services.Catalog.Services.CategoryServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoiesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoiesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
            return CreateActionResultInstance(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(string id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }
        [HttpPost]
        public async Task<IActionResult>Create(CreateCategoryDto createCategoryDto)
        {
            var response = await _categoryService.CreateAsync(createCategoryDto);
            return CreateActionResultInstance(response);
            
        }
    }
}
