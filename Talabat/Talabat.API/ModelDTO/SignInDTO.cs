using System.ComponentModel.DataAnnotations;

namespace Talabat.API.ModelDTO
{
    public class SignInDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}