﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Entites.Orders
{
    public class ProductItemOrder
    {
        public int ProductId {  get; set; }
        public string ProductName { get; set; }
        public string PictureUrl {  get; set; }
    }
}
