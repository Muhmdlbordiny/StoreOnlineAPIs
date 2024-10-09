using StoreCore.G02.Dto.Products;
using StoreCore.G02.Entites;
using StoreCore.G02.Helper;
using StoreCore.G02.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.RepositriesContract
{
    public interface IProductService
    {
       Task<PaginationResponse<ProductDto>> GetAllProductAsync(Productspecparms  productspec);
       Task<IEnumerable<TypeBrandDto>> GetAllTypeAsync();
       Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync();
        Task<ProductDto> GetProductById(int id);
       
    }
}
