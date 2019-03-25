using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class ContractorSkill
    {
        public int ContractorSkillId { get; set; }
        public int ContractorId { get; set; }
        public int SkillId { get; set; }
        public decimal? YearsOfExperience { get; set; }
        public string Description { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
