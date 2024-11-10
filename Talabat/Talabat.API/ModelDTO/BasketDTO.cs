using System.ComponentModel.DataAnnotations;
using Talabat.Core.Models;

namespace Talabat.API.ModelDTO
{
    public class BasketDTO
    {
        [Required(ErrorMessage ="Id Of Basket Is Required")]
        public string Id { get; set; }
        public List<BasketItemDTO> Items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSectet { get; set; }
        public int? DeliveryId { get; set; }
    }
}
