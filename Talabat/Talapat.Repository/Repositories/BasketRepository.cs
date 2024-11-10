using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Talabat.Core.Interfaces;
using Talabat.Core.Models;

namespace Talabat.Repository.Repositories
{
    public class BasketRepository : IRedisRepository
    {
        private readonly IDatabase _Database;

        public BasketRepository(IConnectionMultiplexer connection)
        {
            _Database = connection.GetDatabase();
        }
        public async Task<Basket> AddOrUpdateBasketAsync(Basket basket)
        {
            var update = await _Database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(21));
            return  update is false?null:await GetBasketAsync(basket.Id);
        }

        public async Task<bool> DeleteBasketAsync(string basketid)
        {
            var delete =await _Database.KeyDeleteAsync(basketid);
            return delete is true ? true : false;
        }

        public async Task<Basket> GetBasketAsync(string basketid)
        {
            var basket = await _Database.StringGetAsync(basketid);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Basket>(basket);
        }
    }
}
