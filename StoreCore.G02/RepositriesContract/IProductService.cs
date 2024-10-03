using StoreCore.G02.Dto.Products;
using StoreCore.G02.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.RepositriesContract
{
    public interface IProductService
    {
       Task<IEnumerable<ProductDto>> GetAllProductAsync();
       Task<IEnumerable<TypeBrandDto>> GetAllTypeAsync();
       Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync();
        Task<ProductDto> GetProductById(int id);
        
    }
}
