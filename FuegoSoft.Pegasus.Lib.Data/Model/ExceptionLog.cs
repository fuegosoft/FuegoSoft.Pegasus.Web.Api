using System;
using System.Collections.Generic;

namespace FuegoSoft.Pegasus.Lib.Data.Model
{
    public partial class ExceptionLog
    {
        public int ExceptionLogId { get; set; }
        public string Message { get; set; }
        public string InnerException { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
