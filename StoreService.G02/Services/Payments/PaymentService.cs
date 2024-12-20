using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StoreCore.G02.Dto.Basket;
using StoreCore.G02.Entites;
using StoreCore.G02.Entites.Orders;
using StoreCore.G02.RepositriesContract;
using StoreCore.G02.ServiceContract;
using StoreCore.G02.Specifications.orders;
using Stripe;
using Product = StoreCore.G02.Entites.Product;

namespace StoreService.G02.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public PaymentService(IBasketService basketService,IUnitOfWork unitOfWork,IConfiguration configuration )
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntentIdAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            //GetBasket
            var basket =await _basketService.GetBasketAsync(basketId);
            if (basket is null)
                return null;

            var shippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
               var deliverymentod = await _unitOfWork.Repositry<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value);
                shippingPrice = deliverymentod.Cost;
            }
            if (basket.Items.Count() > 0)
            {
                foreach (var item in basket.Items) 
                {
                  var product = await _unitOfWork.Repositry<Product, int>().GetAsync(item.Id);
                    if(item.Price != product.Price)
                        item.Price = product.Price;

                }
            }
           var subtotal = basket.Items.Sum(I => I.Price * I.Quantity);
            var service = new PaymentIntentService();

            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                //create
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)(subtotal*100 + shippingPrice*100),
                    PaymentMethodTypes = new List<string>() { "card"},
                    Currency = "usd"
                };
                paymentIntent = await service.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                //update
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(subtotal*100 + shippingPrice*100),
                    
                };
                paymentIntent = await service.UpdateAsync(basket.PaymentIntentId,options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
           basket =  await _basketService.UpdateBasketAsync(basket);
            if (basket is null)
                return null;
            return basket;
        }

        public async Task<Order> UpdatePaymentIntentToSucceededOrFailed(string paymentIntentId, bool isSucceeded)
        {
            var spec = new OrderSpecificationwithPaymentintentId(paymentIntentId);
            var order = await _unitOfWork.Repositry<Order, int>().GetWithSpecAsync(spec);
            if (isSucceeded)
            {
                order.Status = OrderStatus.PaymentSuccess;
            }
            else
            {
                order.Status = OrderStatus.PaymentFailed;
            }
             _unitOfWork.Repositry<Order, int>().Update(order);
            await _unitOfWork.CompleteAsync();
            return order;
        }
    }
}
