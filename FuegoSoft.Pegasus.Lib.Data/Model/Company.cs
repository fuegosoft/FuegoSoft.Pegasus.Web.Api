using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class Company
    {
        public int CompanyId { get; set; }
        public Guid? CompanyKey { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string BusinessLine { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public int ZipCode { get; set; }
        public string TelephoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Fax { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
