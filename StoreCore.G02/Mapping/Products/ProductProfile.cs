using AutoMapper;
using Microsoft.Extensions.Configuration;
using StoreCore.G02.Dto.Products;
using StoreCore.G02.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Mapping.Products
{
    public class ProductProfile:Profile
    {
        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<Product, ProductDto>()
                .ForMember
                (p => p.BrandName, option => option.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.TypeName, option => option.MapFrom(s => s.Type.Name))
                .ForMember(d => d.PictureUrl, option =>
                //option.MapFrom(s => $"{configuration["BaseUrl"]}{s.PictureUrl}"));
                option.MapFrom(new PictureUrlResolver(configuration)));
            CreateMap<ProductBrand, TypeBrandDto>();
            CreateMap<ProductType, TypeBrandDto>();
        }
    }
}
