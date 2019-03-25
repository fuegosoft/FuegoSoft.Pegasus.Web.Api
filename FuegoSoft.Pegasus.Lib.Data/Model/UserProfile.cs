using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class UserProfile
    {
        public int UserProfileId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
