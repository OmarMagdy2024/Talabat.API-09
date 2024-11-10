using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.API.Helpers;
using Talabat.Core.Interfaces;
using Talabat.Core.Models;
using Talabat.Core.Models.Order;
using Talabat.Core.ServicesContract;
using Talabat.Core.Specification;

namespace Talabat.Service
{
    public class OrderServices : IOrderServices
    {
        private readonly IRedisRepository _redisRepository;
        private readonly IGenaricRepository<Product> _productrepository;
        private readonly IGenaricRepository<DeliveryType> _deliveryrepository;
        private readonly IGenaricRepository<Order> _orderrepository;
        private readonly IPaymentService _paymentService;

        public OrderServices(IRedisRepository redisRepository,
                            IGenaricRepository<Product> productrepository,
                            IGenaricRepository<DeliveryType> deliveryrepository,
                            IGenaricRepository<Order> orderrepository,
                            IPaymentService paymentService)
        {
            _redisRepository = redisRepository;
            _productrepository = productrepository;
            _deliveryrepository = deliveryrepository;
            _orderrepository = orderrepository;
            _paymentService = paymentService;
        }
        public async Task<Order> CreateOrderAsync(string email, string basketid, int deliveryid, CustomerInformation customerInformation)
        {
            var basketitem = await _redisRepository.GetBasketAsync(basketid);
            var orderitems = new List<OrderItem>();
            if(basketitem?.Items?.Count()>0)
            {
                foreach (var item in basketitem.Items)
                {
                    var prducts=await _productrepository.GetByIdAsync(item.Id);
                    var items = new OrderItem(item.Id,prducts.Name,prducts.PictureUrl,prducts.Price,item.Quantity);
                    orderitems.Add(items);
                }
            }
            var deliverymethod=await _deliveryrepository.GetByIdAsync(deliveryid);
            var param = new OrderParams()
            {
                PaymentIntentId = basketitem?.PaymentIntentId,
            };
            var spec = new OrderSpecification(param);
            var exorder = await _orderrepository.GetByIdWithSpecAsync(spec);
            if (exorder != null)
            { 
                await _orderrepository.DeleteAsync(exorder);
                await _paymentService.CreateOrUpdatePaymentIntent(basketid);
            }
            var order = new Order(email, deliverymethod, customerInformation, orderitems, 1,basketitem?.PaymentIntentId);
            var result= await _orderrepository.CreateAsync(order);
            if (result > 0)
                return order;
            else
                return null;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
          return await _orderrepository.GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string email)
        {
            var param = new OrderParams() { Email = email };
            var spec = new OrderSpecification(param);
            return await _orderrepository.GetAllWithSpecAsync(spec);
        }

        public async Task<Order> GetOrderForUser(string email,int id)
        {
            var param = new OrderParams() { Email = email, Id = id };
            var spec = new OrderSpecification(param);
           return await _orderrepository.GetByIdWithSpecAsync(spec);
        }
    }
}
