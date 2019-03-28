using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FuegoSoft.Pegasus.Web.Service.Models
{
    public class PasswordModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Old password field is required.")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "New password field is required.")]
        public string NewPassword { get; set; }

        [Required]
        [NotMapped]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Password did not matched.")]
        public string ConfirmPassword { get; set; }
    }
}
