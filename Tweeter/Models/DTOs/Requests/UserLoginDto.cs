using System.ComponentModel.DataAnnotations;

namespace Tweeter.Models.DTOs.Requests
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "User name is required")]
        public string User { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
