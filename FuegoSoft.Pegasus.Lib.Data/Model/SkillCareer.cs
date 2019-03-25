using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class SkillCareer
    {
        public int SkillCareerId { get; set; }
        public int SkillId { get; set; }
        public int CareerId { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
