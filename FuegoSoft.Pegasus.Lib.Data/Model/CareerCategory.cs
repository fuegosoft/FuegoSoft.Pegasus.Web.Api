using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class CareerCategory
    {
        public int CareerCategoryId { get; set; }
        public int CareerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
