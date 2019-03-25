using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class UserBanned
    {
        public int UserBannedId { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public DateTime? Duration { get; set; }
        public DateTime? DateUpdate { get; set; }
        public DateTime? DateCreate { get; set; }
    }
}
