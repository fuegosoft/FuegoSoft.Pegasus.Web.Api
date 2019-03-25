using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public int? UserType { get; set; }
        public Guid? UserKey { get; set; }
        public bool? IsDisabled { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
