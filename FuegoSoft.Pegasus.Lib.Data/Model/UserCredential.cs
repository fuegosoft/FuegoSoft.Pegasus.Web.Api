using System;
namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class UserCredential
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public int UserType { get; set; }
        public Guid UserKey { get; set; }
        public Guid LoginKey { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
