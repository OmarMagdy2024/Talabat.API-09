using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Models.Order
{
    public class CustomerInformation
    {
        public CustomerInformation()
        {
            
        }
        public CustomerInformation(string fristName, string lastName, string city, string street, string country)
        {
            FristName = fristName;
            LastName = lastName;
            City = city;
            Street = street;
            Country = country;
        }

        [Required(ErrorMessage ="Name Is Required")]
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
