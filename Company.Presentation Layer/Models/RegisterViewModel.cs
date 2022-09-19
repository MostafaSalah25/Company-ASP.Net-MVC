using System.ComponentModel.DataAnnotations;

namespace Company.Presentation_Layer.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Email is Invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(5 , ErrorMessage = "Minimum Password length is 5 ")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password does not match password")]
        [Required(ErrorMessage = "Confirm Password is required")]
        [MinLength(5, ErrorMessage = "Minimum Confirm Password length is 5 ")]
        public string ConfirmPassword { get; set; } 

        public bool IsAgree   { get; set; }

    }
}
