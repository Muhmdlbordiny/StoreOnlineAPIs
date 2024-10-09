using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.Internal;
using Microsoft.Extensions.Configuration;
using StoreCore.G02.Dto.Products;
using StoreCore.G02.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Mapping.Products
{
    public class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public PictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["BaseUrl"]}{source.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
