using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class UserToken
    {
        public int UserTokenId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public int UserLoginId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
