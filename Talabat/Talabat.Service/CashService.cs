using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.ServicesContract;

namespace Talabat.Service
{
    public class CashService : IcashService
    {
        private readonly IDatabase _multiplexer;

        public CashService(IConnectionMultiplexer multiplexer)
        {
            _multiplexer = multiplexer.GetDatabase();
        }
        public async Task CashAsync(string cashkey, object response, TimeSpan expiretime)
        {
            if (response is null) return;
            var option = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            await _multiplexer.StringSetAsync(cashkey,JsonSerializer.Serialize(response,option),expiretime);
        }

        public async Task<string?> GetCashAsync(string cashkey)
        {
            var response= await _multiplexer.StringGetAsync(cashkey);
            if (response.IsNullOrEmpty) return null;
            return response;
        }
    }
}
