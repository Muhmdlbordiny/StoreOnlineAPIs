using StackExchange.Redis;
using StoreCore.G02.RepositriesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StoreService.G02.Services.Cashes
{
    public class CashService : ICashService
    {
        private readonly IDatabase database;

        public CashService(IConnectionMultiplexer redis)
        {
           database = redis.GetDatabase();
        }
        public async Task<string> GetCashkeyAsync(string key)
        {
            var cashresponse = await database.StringGetAsync(key);
            if (cashresponse.IsNullOrEmpty) return null;
            return cashresponse.ToString();
        }

        public async Task SetCashAsync(string key, object response, TimeSpan expiretime)
        {
            if (response is null) return;
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            await database.StringSetAsync(key, JsonSerializer.Serialize(response,options), expiretime);
        }
    }
}
