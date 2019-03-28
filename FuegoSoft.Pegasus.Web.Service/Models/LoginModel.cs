using System;
using System.ComponentModel.DataAnnotations;

namespace FuegoSoft.Pegasus.Web.Service.Models
{
    public class LoginModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Username has a maximum length of 50.")]
        [MinLength(5, ErrorMessage = "Username must have at least 5 characters.")]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessage = "Must contains ONLY Upper and Lower letters and numbers.")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50, ErrorMessage = "Password has a maxumum length of 50.")]
        [MinLength(8, ErrorMessage = "Password must have at least 8 characters.")]
        [RegularExpression("^((?=.*[a-z])(?=.*[A-Z])(?=.*\\d)).+$", ErrorMessage = "Password must contains atleast 1 lowercase, 1 uppercase letter and 1 number.")]
        public string Password { get; set; }
    }
}
