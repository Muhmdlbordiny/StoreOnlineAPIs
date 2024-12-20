using StoreCore.G02.Dto.Autho;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Dto.orders
{
    public class OrderDto
    {
        public string BasketId {  get; set; }
        public int DeliveryMethodId {  get; set; }
        public AddressDto ShipToAddress {  get; set; }
         
    }
}
