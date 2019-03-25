using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class ContractLinkExpiration
    {
        public int ContractExpirationId { get; set; }
        public int UserId { get; set; }
        public int ContractId { get; set; }
        public string ExtractKey { get; set; }
        public string Base64Link { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool? IsUsed { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
