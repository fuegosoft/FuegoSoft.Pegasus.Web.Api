using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class ContractorProfile
    {
        public int ContractorProfileId { get; set; }
        public int ContractorId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string MotherMaidenName { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string Citizenship { get; set; }
        public string Spouse { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string CivilStatus { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
