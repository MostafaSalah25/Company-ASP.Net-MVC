using System.ComponentModel.DataAnnotations;

namespace Company.Presentation_Layer.Models
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is Invalid")]
        public string Email { get; set; }
    }
}
