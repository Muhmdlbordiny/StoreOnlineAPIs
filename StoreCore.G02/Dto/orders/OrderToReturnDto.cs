
using StoreCore.G02.Entites.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Dto.orders
{
    public class OrderToReturnDto
    {
       public int Id { get; set; }
        public string BuerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public string Status { get; set; } 
        public AddressDto ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string? PaymentIntendId { get; set; } = string.Empty;

    }
}
