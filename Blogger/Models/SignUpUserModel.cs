using System.ComponentModel.DataAnnotations;

namespace Blogger.Models
{
    public class SignUpUserModel : LogInViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;

    }
}
