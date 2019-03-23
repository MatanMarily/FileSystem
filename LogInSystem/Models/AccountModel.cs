using System.ComponentModel.DataAnnotations;

namespace LogInSystem.Models
{
    public class AccountModel
    {
        public class LoginViewModel
        {
            [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
            [Display(Name = "Email")]
            [EmailAddress(ErrorMessage = "Invalid Email")]
            public string Email { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            [MinLength(4, ErrorMessage = "Minimun 4 charecther required")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public class User
        {
            [Required]
            [Display(Name = "Email")]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }

        public class RegisterViewModel
        {
            [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
            [Display(Name = "Email")]
            [EmailAddress(ErrorMessage = "Invalid Email")]
            public string Email { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
            [MinLength(4, ErrorMessage = "Minimun 4 charecther required")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "password are not match")]
            public string ConfirmPassword { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Phone Number")]
            [MinLength(7 , ErrorMessage = "must be minimum 7 number")]
            [MaxLength(15, ErrorMessage = "maximum 15 number")]
            [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number must be numeric")]
            public string Phone { get; set; }
        }

        public class ForgotPassword
        {
            [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
            [Display(Name = "Email")]
            [EmailAddress(ErrorMessage = "Invalid Email")]
            public string Email { get; set; }
        }
    }
}