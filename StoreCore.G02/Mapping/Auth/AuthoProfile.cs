using AutoMapper;
using StoreCore.G02.Dto.Autho;
using StoreCore.G02.Dto.orders;
using StoreCore.G02.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Mapping.Auth
{
    public class AuthoProfile:Profile
    {
        public AuthoProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
