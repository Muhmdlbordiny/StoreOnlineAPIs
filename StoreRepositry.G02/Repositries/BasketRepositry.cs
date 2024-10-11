using StackExchange.Redis;
using StoreCore.G02.Entites;
using StoreCore.G02.RepositriesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StoreRepositry.G02.Repositries
{
    public class BasketRepositry : IBasketRepositry
    {
        private readonly IDatabase _database;
        public BasketRepositry(IConnectionMultiplexer redis)
        {
           _database= redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
           return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
           var basket = await _database.StringGetAsync(basketId);
            return basket.IsNullOrEmpty ? null:JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
           var createdOrUpdatedBasket =await _database.StringSetAsync(basket.Id,
               JsonSerializer.Serialize (basket), TimeSpan.FromDays(30));
            if (createdOrUpdatedBasket is false ) return null;
            return await GetBasketAsync(basket.Id);
            
            
        }
    }
}
