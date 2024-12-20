using AutoMapper;
using StoreCore.G02.Dto.DeliveryMethods;
using StoreCore.G02.Entites.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Mapping.DeliveryMethods
{
    public class DeliveryMethodProfile:Profile
    {
        public DeliveryMethodProfile()
        {
            CreateMap<DeliveryMethod,DeliveryMethodDto>().ReverseMap();
        }
    }
}
