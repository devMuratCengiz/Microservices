using Microservice.Shared.Services;
using Microservices.Web.Models.Catalog.Course;
using Microservices.Web.Models.Catalog.Feature;
using Microservices.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Microservices.Web.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CoursesController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCoursesByUserIdAsync(_sharedIdentityService.GetUserId));
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(CreateCourseInput createCourseInput)
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            createCourseInput.UserId = _sharedIdentityService.GetUserId;
            createCourseInput.Picture = "as";

            if (!ModelState.IsValid)
            {
                return View();
            }
            
            await _catalogService.CreateCourseAsync(createCourseInput);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult>Update(string id)
        {

            var course = await _catalogService.GetByCourseId(id);
            var categories = await _catalogService.GetAllCategoryAsync();
            
            if (course == null)
            {
                RedirectToAction(nameof(Index));
            }
            ViewBag.categoryList = new SelectList(categories, "Id", "Name",course.Id);
            UpdateCourseInput updateCourseInput = new()
            {
                Id = course.Id,
                Name = course.Name,
                Price = course.Price,
                Feature = course.Feature,
                CategoryId = course.CategoryId,
                Description = course.Description,
                Picture = course.Picture,
                UserId = course.UserId
            };
            return View(updateCourseInput);
            
        }
        [HttpPost]
        public async Task<IActionResult>Update(UpdateCourseInput updateCourseInput)
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name",updateCourseInput.Id);
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _catalogService.UpdateCourseAsync(updateCourseInput);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult>Delete(string id)
        {
            await _catalogService.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
