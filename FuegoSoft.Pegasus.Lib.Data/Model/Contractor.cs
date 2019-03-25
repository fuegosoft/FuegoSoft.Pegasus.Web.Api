using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class Contractor
    {
        public int ContractorId { get; set; }
        public int UserId { get; set; }
        public string SubscriberKey { get; set; }
        public Guid? ContractorKey { get; set; }
        public DateTime StartEffectivityDate { get; set; }
        public DateTime EndEffectivityDate { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
