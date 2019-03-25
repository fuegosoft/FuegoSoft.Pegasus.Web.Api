using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class CareerType
    {
        public int CareerTypeId { get; set; }
        public int CareerCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
