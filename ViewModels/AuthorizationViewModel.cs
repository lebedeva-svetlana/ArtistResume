using System.ComponentModel.DataAnnotations;

namespace Resume.ViewModels
{
    public class AuthorizationViewModel
    {
        [Display(Prompt = "Email")]
        [Required(ErrorMessage = "EmailErrorMessage")]
        public string Email { get; set; }

        [Display(Prompt = "Password")]
        [Required(ErrorMessage = "PasswordErrorMessage")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}