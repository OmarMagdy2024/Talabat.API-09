using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Models.Order
{
    public class Order : BaseModel
    {
        public Order()
        {
            
        }
        public Order(string customerEmail, DeliveryType deliveryType, CustomerInformation customerInformation, ICollection<OrderItem> orderItems, int payMentId, string? paymentIntentId)
        {
            CustomerEmail = customerEmail;
            DeliveryType = deliveryType;
            CustomerInformation = customerInformation;
            OrderItems = orderItems;
            PayMentId = payMentId;
            Total = GetTotal();
            PaymentIntentId = paymentIntentId;
        }

        [Required(ErrorMessage = "YourEmail Is Required")]
        public string CustomerEmail { get; set; }
        public DateTimeOffset DateTime { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = (OrderStatus)1;
        public int? DeliveryId { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public CustomerInformation CustomerInformation { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public decimal SupTotal { get; }
        public decimal Total { get; }
        public int PayMentId {get;set;}
        public string? PaymentIntentId { get; set; }

        public decimal GetTotal()
            => DeliveryType.Cost+OrderItems.Sum(item=>item.Amount);
    }
}
