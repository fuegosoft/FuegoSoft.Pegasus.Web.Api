using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class ContractVoucher
    {
        public int ContractVoucherId { get; set; }
        public int ContractId { get; set; }
        public string VoucherCode { get; set; }
        public Guid? VoucherKey { get; set; }
        public DateTime Duration { get; set; }
        public DateTime? CodeDuration { get; set; }
        public decimal TotalDiscount { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
