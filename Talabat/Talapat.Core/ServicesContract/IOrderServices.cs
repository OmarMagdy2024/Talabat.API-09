using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Models.Order;

namespace Talabat.Core.ServicesContract
{
    public interface IOrderServices
    {
        public Task<Order> CreateOrderAsync(string Email,string BasketId,int DeliveryMethod,CustomerInformation customerInformation);
        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string email);
        public Task<Order> GetOrderByIdAsync(int id);
        public Task<Order> GetOrderForUser(string email, int id);
    }
}
