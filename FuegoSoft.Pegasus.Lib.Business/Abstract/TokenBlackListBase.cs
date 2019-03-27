using System;
using System.Collections.Generic;
using System.Text;

namespace FuegoSoft.Pegasus.Lib.Business.Abstract
{
    public abstract class TokenBlackListBase
    {
        public abstract bool InsertTokenBlackList();
        public abstract bool IsTokenInBlackList();
    }
}
