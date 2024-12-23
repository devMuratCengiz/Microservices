﻿using Microservices.Web.Models.Order;

namespace Microservices.Web.Models.FakePayment
{
    public class PaymentInfoInput
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public decimal TotalPrice { get; set; }
        public CreateOrderInput Order { get; set; }
    }
}
