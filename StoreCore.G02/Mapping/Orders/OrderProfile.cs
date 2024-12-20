using AutoMapper;
using Microsoft.Extensions.Configuration;
using StoreCore.G02.Dto.orders;
using StoreCore.G02.Entites.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Mapping.Orders
{
    public class OrderProfile:Profile
    {
        public OrderProfile(IConfiguration configuration)
        {
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, options => options.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, options => options.MapFrom(s => s.DeliveryMethod.Cost));
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, options => options.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, options => options.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, options => options.MapFrom(s => $"{configuration["BaseUrl"]}{s.Product.PictureUrl}"));
        }
    }
}
