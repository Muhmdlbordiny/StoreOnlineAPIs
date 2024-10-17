﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Entites.Orders
{
    public class Order:BaseEntity<int>
    {
        public string BuerEmail {  get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress {  get; set; } 
        public int DeliveryMethodId {  get; set; }//Fk
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public decimal SubTotal {  get; set; }
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;
        public string PaymentIntendId {  get; set; }

    }
}
