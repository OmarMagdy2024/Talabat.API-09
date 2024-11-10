using System.ComponentModel.DataAnnotations;

namespace Talabat.API.ModelDTO
{
    public class OrderDTO
    {
        [EmailAddress(ErrorMessage ="Email Address is not Right")]
        [Required]
        public string email { get; set; }
        [Required]
        public string basketid { get; set; }
        [Required]
        public int deliveryid { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string FristName { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "City Is Required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Street Is Required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Country Is Required")]
        public string Country { get; set; }
    }
}
