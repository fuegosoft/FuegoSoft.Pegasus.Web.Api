using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FuegoSoft.Pegasus.Web.Service.Models
{
    public class User
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length for username is up to 100 characters only.")]
        [MinLength(5, ErrorMessage = "Minimum length for username is at least 5 characters.")]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessage = "Must only contains Upper and Lower letters and numbers.")]
        public string Username { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length for password is up to 100 characters only.")]
        [MinLength(8, ErrorMessage = "Minimum length for password is at least 8 characters.")]
        [RegularExpression("^((?=.*[a-z])(?=.*[A-Z])(?=.*\\d)).+$", ErrorMessage = "Password must contains atleast 1 lowercase, 1 uppercase letter and 1 number.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string ContactNumber { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length for firstname is up to 100 characters only.")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length for firstname is up to 100 characters only.")]
        public string LastName { get; set; }

        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
