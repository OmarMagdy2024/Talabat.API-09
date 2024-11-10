using System.ComponentModel.DataAnnotations;

namespace Talabat.API.ModelDTO
{
    public class RegisterDTO
    {
        [Required]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        public string password { get; set; }

        [Required(ErrorMessage = "confirmpassword is required")]
        [Compare(nameof(password), ErrorMessage = "confirmpassword doesn't match password")]
        public string comfirmpassword { get; set; }
    }
}