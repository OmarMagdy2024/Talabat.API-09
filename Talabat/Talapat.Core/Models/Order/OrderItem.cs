using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Models.Order
{
    public class OrderItem :BaseModel
    {
        public OrderItem()
        {
            
        }
        public OrderItem(int productId, string productName, string productPictureURL, decimal productPrice, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPictureURL = productPictureURL;
            ProductPrice = productPrice;
            Quantity = quantity;
            Amount = GetAmount();
        }

        [Required(ErrorMessage = "ProductId Is Required")]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductPictureURL { get; set; }
        public decimal ProductPrice { get; set; }

        [Required(ErrorMessage = "Quantity Is Required")]
        public int Quantity { get; set; }
        public decimal Amount { get; set; }

        public decimal GetAmount()
            => ProductPrice*Quantity;
    }
}
