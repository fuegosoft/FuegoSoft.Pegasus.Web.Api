using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class Subscriber
    {
        public int SubscriberId { get; set; }
        public int ContractId { get; set; }
        public int UserId { get; set; }
        public string UserKey { get; set; }
        public string ContractKey { get; set; }
        public bool? IsAgreedTermsAndCondition { get; set; }
        public Guid? SubscriberKey { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
