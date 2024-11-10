using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Interfaces
{
    public interface IRedisRepository
    {
        public Task<Basket> GetBasketAsync(string basketid);
        public Task<Basket> AddOrUpdateBasketAsync(Basket basket);
        public Task<bool> DeleteBasketAsync(string basketid);
    }
}
