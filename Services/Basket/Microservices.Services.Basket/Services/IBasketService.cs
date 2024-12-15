using Microservice.Shared.Dtos;
using Microservices.Services.Basket.Dtos;

namespace Microservices.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Response<bool>>Delete(string userId);
    }
}
