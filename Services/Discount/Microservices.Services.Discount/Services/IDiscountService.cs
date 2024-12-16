using Microservice.Shared.Dtos;
using Microservices.Services.Discount.Dtos;

namespace Microservices.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<Response<List<ResultDiscountDto>>> GetAll();
        Task<Response<ResultDiscountDto>> GetById(int id);
        Task<Response<NoContent>> Save(CreateDiscountDto createDiscountDto);
        Task<Response<NoContent>> Update(UpdateDiscountDto updateDiscountDto);
        Task<Response<NoContent>> Delete(int id);
        Task<Response<ResultDiscountDto>> GetByCodeAndUserId(string code, string userId);
    }
}
