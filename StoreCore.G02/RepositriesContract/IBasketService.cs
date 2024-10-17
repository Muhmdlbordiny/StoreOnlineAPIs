using StoreCore.G02.Dto.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.RepositriesContract
{
    public interface IBasketService
    {
        Task<CustomerBasketDto?> GetBasketAsync(string basketId);
        Task<CustomerBasketDto?> UpdateBasketAsync(CustomerBasketDto basketdto);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
