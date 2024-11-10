using System.ComponentModel.DataAnnotations;

namespace Talabat.API.ModelDTO
{
    public class BasketItemDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range(0.1, double.MaxValue,ErrorMessage ="The Price Is Wrong")]
        public decimal Price { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [Range(1,int.MaxValue, ErrorMessage = "The Quantity Must Be At Least One")]
        public int Quantity { get; set; }
    }
}