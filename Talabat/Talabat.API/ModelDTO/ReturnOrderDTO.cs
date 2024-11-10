using System.ComponentModel.DataAnnotations;
using Talabat.Core.Models.Order;

namespace Talabat.API.ModelDTO
{
    public class ReturnOrderDTO
    {
        public int OrderId { get; set; }
        public string CustomerEmail { get; set; }
        public DateTimeOffset DateTime { get; set; } 
        public string Status { get; set; }
        public string DeliveryName { get; set; }
        public decimal DeliveryCost { get; set; }
        public CustomerInformation CustomerInformation { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public decimal SupTotal { get; set; }
        public decimal Total { get; set; }
        public int PayMentId { get; set; } = 1;
    }
}
