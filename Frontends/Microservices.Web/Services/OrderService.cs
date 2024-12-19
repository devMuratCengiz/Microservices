using Microservice.Shared.Dtos;
using Microservice.Shared.Services;
using Microservices.Web.Models.FakePayment;
using Microservices.Web.Models.Order;
using Microservices.Web.Services.Interfaces;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;

namespace Microservices.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentService _paymentService;
        private readonly HttpClient _httpClient;
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;
        

        public OrderService(IPaymentService paymentService, HttpClient httpClient, IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _paymentService = paymentService;
            _httpClient = httpClient;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();
            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                Expiration = checkoutInfoInput.Expiration,
                CVV = checkoutInfoInput.CVV,
                TotalPrice = basket.TotalPrice
            };

            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

            if (!responsePayment)
            {
                return new OrderCreatedViewModel() { Error="Ödeme Alınamadı",IsSuccessful=false };
            }

            var createOrderInput = new CreateOrderInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new CreateAddressInput { Province=checkoutInfoInput.Province,District=checkoutInfoInput.District,Street=checkoutInfoInput.Street,Line = checkoutInfoInput.Line,ZipCode=checkoutInfoInput.ZipCode}
                
            };
            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new CreateOrderItemInput
                {
                    ProductId = x.CourseId,
                    Price = x.GetCurrentPrice,
                    PictureUrl = "",
                    ProductName = x.CourseName
                };
                createOrderInput.OrderItems.Add(orderItem);
            });

            var response = await _httpClient.PostAsJsonAsync("orders", createOrderInput);

            if (!response.IsSuccessStatusCode)
            {
                return new OrderCreatedViewModel() { Error = "Sipariş oluşturulamadı", IsSuccessful = false };
            }
            var orderCreateViewModel = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();
            orderCreateViewModel.Data.IsSuccessful = true;
            await _basketService.Delete();
            return orderCreateViewModel.Data;
            
        }

        public async Task<List<OrderViewModel>> GetOrder()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");
            return response.Data;
        }

        public Task SuspendOrder(CheckoutInfoInput checkoutInfoInput)
        {
            throw new NotImplementedException();
        }
    }
}
