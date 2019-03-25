using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class ContractorCompany
    {
        public int ContractorCompanyId { get; set; }
        public int ContractorId { get; set; }
        public int CompanyId { get; set; }
        public string Position { get; set; }
        public decimal? YearsOfExperience { get; set; }
        public string Description { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
