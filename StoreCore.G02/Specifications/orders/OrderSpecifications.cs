using StoreCore.G02.Entites.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Specifications.orders
{
    public class OrderSpecifications:BaseSpecification<Order,int>
    {
        public OrderSpecifications(string buyerEmail, int orderId)
            :base(O=>O.BuerEmail == buyerEmail&& O.Id == orderId) 
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
        }
        public OrderSpecifications(string buyerEmail)
           : base(O => O.BuerEmail == buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
        }
    }
}
