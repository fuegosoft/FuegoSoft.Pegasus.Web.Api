using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class TokenBlackList
    {
        public int TokenBlackListId { get; set; }
        public string Token { get; set; }
        public Guid LoginKey { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
