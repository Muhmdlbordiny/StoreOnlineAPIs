using AutoMapper;
using StoreCore.G02.Dto.Products;
using StoreCore.G02.Entites;
using StoreCore.G02.RepositriesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreService.G02.Services.Producted
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
           _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductAsync()
        {
            return _mapper.Map<IEnumerable<ProductDto>>
                        (await _unitOfWork.Repositry<Product, int>().GetAllAsync());

        }
        public async Task<IEnumerable<TypeBrandDto>> GetAllTypeAsync()
        {
          return  _mapper.Map<IEnumerable<TypeBrandDto>>
                (_unitOfWork.Repositry<ProductType, int>().GetAllAsync());
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _unitOfWork.Repositry<Product, int>().GetAsync(id);
           var mappedproduct = _mapper.Map<ProductDto>(product);
            return mappedproduct;
        }
        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
          return _mapper.Map<IEnumerable<TypeBrandDto>>
                ( await _unitOfWork.Repositry<ProductBrand,int>().GetAllAsync());
        }

       

       
    }
}
