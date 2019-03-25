using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class ContractorSkillStatistics
    {
        public int ContractorSkillStatisticsId { get; set; }
        public int UserId { get; set; }
        public int ContractorId { get; set; }
        public int SkillId { get; set; }
        public decimal Rating { get; set; }
        public string Comment { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
