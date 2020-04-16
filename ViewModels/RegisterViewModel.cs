using System.ComponentModel.DataAnnotations;

namespace FirstBlog.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter Username")]
        [StringLength(255, ErrorMessage = "Username should have from 5 to 50 characters", MinimumLength = 5)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [StringLength(255, ErrorMessage = "Password should have from 8 to 25 characters", MinimumLength = 8)]
        [RegularExpression("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,25}$", ErrorMessage = "Password should contain at least 1 uppaercase leter, 1 lowcase letter and digits")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm Password")]
        [Compare("Password", ErrorMessage = "Data in confirm password field is incorrect")]
        public string ConfirmPassword { get; set; }
    }
}
