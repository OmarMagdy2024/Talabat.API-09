using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Models
{
    public class Basket
    {
        public Basket(string id)
        {
            Id = id;
            Items = new List<BasketItem>();

        }
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSectet { get; set; }
        public int? DeliveryId { get; set; }
    }
}
