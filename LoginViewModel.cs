using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace crud.Models
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
