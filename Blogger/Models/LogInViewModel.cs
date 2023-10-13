using System.ComponentModel.DataAnnotations;

namespace Blogger.Models
{
    public class LogInViewModel
    {
        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string ReturnUrl { get; set; } = "/";

    }
}
