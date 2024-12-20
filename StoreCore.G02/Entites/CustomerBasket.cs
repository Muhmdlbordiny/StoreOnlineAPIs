using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Entites
{
    public class CustomerBasket
    {
        public  string Id { get; set; }
        public List<BasketItem> Items { get; set; }
        public int? DeliveryMethodId {  get; set; }
        public string? PaymentIntentId {  get; set; }
        public string? ClientSecret {  get; set; }//Token

    }
}
