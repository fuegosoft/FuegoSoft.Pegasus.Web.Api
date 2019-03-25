﻿using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class ContractorCareer
    {
        public int ContractorCareerId { get; set; }
        public int ContractorId { get; set; }
        public int CareerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? TotalYearOfExperience { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
