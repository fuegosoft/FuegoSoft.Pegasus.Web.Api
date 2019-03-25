using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class UserLogin
    {
        public int UserLoginId { get; set; }
        public int UserId { get; set; }
        public Guid? LoginKey { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool? LoginAttempt { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
