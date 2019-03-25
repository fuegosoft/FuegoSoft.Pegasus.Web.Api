using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class Region
    {
        public int RegionId { get; set; }
        public string Psgcode { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
