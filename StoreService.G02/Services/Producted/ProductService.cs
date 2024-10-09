using AutoMapper;
using StoreCore.G02.Dto.Products;
using StoreCore.G02.Entites;
using StoreCore.G02.Helper;
using StoreCore.G02.RepositriesContract;
using StoreCore.G02.Specifications;
using StoreCore.G02.Specifications.Products;
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
        public async Task<PaginationResponse<ProductDto>> GetAllProductAsync(Productspecparms productspec)
        {
            var spec = new ProductSpecfication( productspec);
            var product = await _unitOfWork.Repositry<Product, int>().GetAllWithSpecAsync(spec);
            var mapped = _mapper.Map<IEnumerable<ProductDto>>(product);
            var countspec= new ProductwithCountSpecfication(productspec);
            var count = await _unitOfWork.Repositry<Product, int>().GetCountasync(countspec);
            return new PaginationResponse<ProductDto>(productspec.Pagesize,productspec.PageIndex,count,mapped);

        }
        public async Task<ProductDto> GetProductById(int id)
        {
            var spec = new ProductSpecfication(id);
            var product = await _unitOfWork.Repositry<Product, int>().GetWithSpecAsync(spec);
            var mappedproduct = _mapper.Map<ProductDto>(product);
            return mappedproduct;
        }

        public async Task<IEnumerable<TypeBrandDto>> GetAllTypeAsync()
        {
          var types = await _unitOfWork.Repositry<ProductType,int>().GetAllAsync();
            var mappedtypes = _mapper.Map<IEnumerable<TypeBrandDto>>(types);
            return mappedtypes;
        }

        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
          return _mapper.Map<IEnumerable<TypeBrandDto>>
                ( await _unitOfWork.Repositry<ProductBrand,int>().GetAllAsync());
        }

       

       
    }
}
