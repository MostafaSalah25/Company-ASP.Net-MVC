using System.ComponentModel.DataAnnotations;

namespace Company.Presentation_Layer.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Password is Required")]
        [MinLength(6, ErrorMessage = "Minimum Length of password is 6 chars")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [MinLength(6, ErrorMessage = "Minimum Length of password is 6 chars")]
        [Compare("Password", ErrorMessage = "Confirm password does not match password")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
