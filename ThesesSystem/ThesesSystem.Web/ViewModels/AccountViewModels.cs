using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThesesSystem.Web.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Е-поща")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Е-поща")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [Display(Name = "Запомни ме")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "{0} трябва да е поне {2} символа дълга.", MinimumLength = 2)]
        [Display(Name = "Първо име")]
        public string FirstName { get; set; }

        [StringLength(20)]
        [Display(Name = "Бащино име")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Фамилно име")]
        [StringLength(20, ErrorMessage = "{0} трябва да е поне {2} символа дълга.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Телефонен номер")]
        public string Number { get; set; }

        [Required]
        [Display(Name = "ЕГН")]
        public long EGN { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Е-поща")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} трябва да е поне {2} символа дълга.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторете паролата")]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
