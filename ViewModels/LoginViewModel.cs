using System.ComponentModel.DataAnnotations;

namespace FirstBlog.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }
    }
}
