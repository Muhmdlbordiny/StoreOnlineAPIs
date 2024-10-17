using AutoMapper;
using StoreCore.G02.Dto.Basket;
using StoreCore.G02.Entites;
using StoreCore.G02.RepositriesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreService.G02.Services.Basket
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepositry _basketRepositry;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepositry basketRepositry,IMapper mapper)
        {
            _basketRepositry = basketRepositry;
            _mapper = mapper;
        }
        public async Task<CustomerBasketDto?> GetBasketAsync(string basketId)
        {
            var basket = await _basketRepositry.GetBasketAsync(basketId);
            if (basket is null)
                return _mapper.Map<CustomerBasketDto>(new CustomerBasket() { Id = basketId });
            return _mapper.Map<CustomerBasketDto>(basket);
        }

        public async Task<CustomerBasketDto?> UpdateBasketAsync(CustomerBasketDto basketdto)
        {
            var basket = await _basketRepositry.UpdateBasketAsync(_mapper.Map<CustomerBasket>(basketdto));
            if (basket is null) return null;
            return _mapper.Map<CustomerBasketDto>(basket);
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _basketRepositry.DeleteBasketAsync(basketId);
        }

        
    }
}
