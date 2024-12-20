using Microservices.Web.Models.Order;
using Microservices.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;

        public OrderController(IOrderService orderService, IBasketService basketService)
        {
            _orderService = orderService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.Get();
            ViewBag.basket = basket;
            
            return View(new CheckoutInfoInput());
        }
        [HttpPost]
        public async Task<IActionResult>Checkout(CheckoutInfoInput checkoutInfoInput)
        {
            //var orderStatus = await _orderService.CreateOrder(checkoutInfoInput);

            var orderSuspend = await _orderService.SuspendOrder(checkoutInfoInput);
            

            if (!orderSuspend.IsSuccessful)
            {
                var basket = await _basketService.Get();
                ViewBag.basket = basket;
                ViewBag.error = orderSuspend.Error;
                return View();
            }
            return RedirectToAction(nameof(SuccessfulCheckout), new {orderId = new Random().Next(1,1000)});

        }
        public IActionResult SuccessfulCheckout(int orderId)
        {
            ViewBag.orderId = orderId;
            return View();
        }
        public async Task<IActionResult> CheckoutHistory()
        {
            return View(await _orderService.GetOrder());
        }
    }
}
