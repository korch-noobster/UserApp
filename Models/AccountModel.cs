using System.ComponentModel.DataAnnotations;

namespace UserApp.Models
{
    public class AccountModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
