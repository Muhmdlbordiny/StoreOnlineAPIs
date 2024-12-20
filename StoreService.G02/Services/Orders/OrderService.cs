using StoreCore.G02.Entites;
using StoreCore.G02.Entites.Orders;
using StoreCore.G02.RepositriesContract;
using StoreCore.G02.ServiceContract;
using StoreCore.G02.Specifications.orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace StoreService.G02.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;
        private readonly IPaymentService _paymentService;

        public OrderService(IUnitOfWork unitOfWork,IBasketService basketService,IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _basketService = basketService;
            _paymentService = paymentService;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address ShippingAddress)
        {
           var basket = await _basketService.GetBasketAsync(basketId);
            if (basket is null) return null;
            var orderitems = new List<OrderItem>();
            if (basket.Items.Count() > 0)
            {
                foreach (var item in basket.Items) 
                {
                    var product = await _unitOfWork.Repositry<Product, int>().GetAsync(item.Id);    
                    var ProductOrderedItem = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
                    var orderitem = new OrderItem(ProductOrderedItem, product.Price, item.Quantity);
                    orderitems.Add(orderitem);
                }
            }

           var deliverymethod = await _unitOfWork.Repositry<DeliveryMethod, int>().GetAsync(deliveryMethodId);
            var SubTotal = orderitems.Sum(I => I.Price * I.Quantity);
            //ToDo
            if (!string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var spec = new OrderSpecificationwithPaymentintentId(basket.PaymentIntentId); 
                var ExOrder = await _unitOfWork.Repositry<Order, int>().GetWithSpecAsync(spec);
                _unitOfWork.Repositry<Order, int>().Delete(ExOrder);
            }
            var basketdto = await _paymentService.CreateOrUpdatePaymentIntentIdAsync(basketId);
           
            var order = new Order(buyerEmail, ShippingAddress, deliverymethod, orderitems, SubTotal, basketdto?.PaymentIntentId??"Invaild Operation");
            await _unitOfWork.Repositry<Order,int>().AddAsync(order);
           var result = await _unitOfWork.CompleteAsync();
            if (result <= 0)
                return null;
            return order;
        }
        public async Task<IEnumerable<Order>?> GetOrderForSpecificUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecifications(buyerEmail);
          var orders = await _unitOfWork.Repositry<Order, int>().GetAllWithSpecAsync(spec);
            if (orders == null) return null;
            return orders;
        }
        public async Task<Order> GetOrderByIdForSpecificUserAsync(string buyerEmail, int orderId)
        {
            var spec = new OrderSpecifications(buyerEmail, orderId);

          var order = await _unitOfWork.Repositry<Order, int>().GetWithSpecAsync(spec);
            if(order is null) return null;
         return order;
        }

    }
}
