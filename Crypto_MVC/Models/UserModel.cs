using System.ComponentModel.DataAnnotations;

namespace Crypto_MVC.Models
{
    public class UserModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        public string? Name { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
