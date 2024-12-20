using StoreCore.G02.Entites.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Specifications.orders
{
    public class OrderSpecificationwithPaymentintentId:BaseSpecification<Order,int>
    {
        public OrderSpecificationwithPaymentintentId(string paymentintentId):
            base(o=>o.PaymentIntendId==paymentintentId)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
        }
    }
}
