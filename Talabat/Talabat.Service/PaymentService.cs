using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Interfaces;
using Talabat.Core.Models;
using Talabat.Core.Models.Order;
using static System.Formats.Asn1.AsnWriter;
using Product = Talabat.Core.Models.Product;

namespace Talabat.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IRedisRepository _basketrepository;
        private readonly IGenaricRepository<DeliveryType> _deliveryrepository;
        private readonly IGenaricRepository<Product> _productrepository;

        public PaymentService(IConfiguration configuration,IRedisRepository basketrepository,IGenaricRepository<DeliveryType> Deliveryrepository,IGenaricRepository<Product> productrepository)
        {
            _configuration = configuration;
            _basketrepository = basketrepository;
            _deliveryrepository = Deliveryrepository;
            _productrepository = productrepository;
        }
        public async Task<Basket?> CreateOrUpdatePaymentIntent(string basketid)
        {
            StripeConfiguration.ApiKey = _configuration["Scripe:Secretkey"];
            var basket=await _basketrepository.GetBasketAsync(basketid);
            if (basket == null) { return null; }
            var Shipprice = 0M;
            if (basket.DeliveryId.HasValue)
            {
                var delivery =await _deliveryrepository.GetByIdAsync(basket.DeliveryId.Value);
                Shipprice = delivery.Cost;
            }
            if (basket.Items.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var products =await _productrepository.GetByIdAsync(item.Id);
                    if (products.Price != item.Price)
                    { item.Price = products.Price; }
                }
            }
            var suptotal =basket.Items.Sum(x => x.Price*x.Quantity);
            PaymentIntent paymentIntent;
            var service =new PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var option = new PaymentIntentCreateOptions()
                {
                    Amount=(long) (suptotal*100+Shipprice*100),
                    Currency = "usd",
                    PaymentMethodTypes=new List<string>() { "card"}
                };
                paymentIntent = await service.CreateAsync(option);
                basket.PaymentIntentId=paymentIntent.Id;
                basket.ClientSectet=paymentIntent.ClientSecret;
            }
            else 
            {
                var option = new PaymentIntentUpdateOptions() { Amount = (long)(suptotal * 100 + Shipprice * 100), };
                paymentIntent = await service.UpdateAsync(basket.PaymentIntentId, option);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSectet = paymentIntent.ClientSecret;
            };
            await _basketrepository.AddOrUpdateBasketAsync(basket);
            return basket;
        }
    }
}
