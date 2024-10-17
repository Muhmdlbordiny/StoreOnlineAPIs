using AutoMapper;
using StoreCore.G02.Dto.Basket;
using StoreCore.G02.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Mapping.Basket
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap(); 
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
        }
    }
}
