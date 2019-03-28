using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FuegoSoft.Pegasus.Web.Service.Models
{
    public class UserProfileModel
    {
        [DataType(DataType.PhoneNumber)]
        [StringLength(100)]
        public string ContactNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(100)]
        public string EmailAddress { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string MiddleName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
