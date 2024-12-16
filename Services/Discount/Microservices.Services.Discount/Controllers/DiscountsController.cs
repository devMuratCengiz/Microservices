using Microservice.Shared.ControllerBases;
using Microservice.Shared.Services;
using Microservices.Services.Discount.Dtos;
using Microservices.Services.Discount.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomBaseController
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public DiscountsController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _discountService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResultInstance(await _discountService.GetById(id));
        }
        [HttpGet]
        [Route("/api/[controller]/[action]/{code}")]
        public async Task<IActionResult>GetByCode(string code)
        {
            var userId = _sharedIdentityService.GetUserId;
            var discount = await _discountService.GetByCodeAndUserId(code, userId);
            return CreateActionResultInstance(discount);

        }
        [HttpPost]
        public async Task<IActionResult>Save(CreateDiscountDto createDiscountDto)
        {
            return CreateActionResultInstance(await _discountService.Save(createDiscountDto));
        }
        [HttpPut]
        public async Task<IActionResult>Update(UpdateDiscountDto updateDiscountDto)
        {
            return CreateActionResultInstance(await _discountService.Update(updateDiscountDto));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            return CreateActionResultInstance(await _discountService.Delete(id));
        }
    }
}
