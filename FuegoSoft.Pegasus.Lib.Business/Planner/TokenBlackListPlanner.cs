using FuegoSoft.Pegasus.Lib.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuegoSoft.Pegasus.Lib.Business.Planner
{
    public class TokenBlackListPlanner
    {
        private TokenBlackListBase tokenBlackListBase;
        public void SetTokenBlackListPlanner(TokenBlackListBase _tokenBlackListBase)
        {
            this.tokenBlackListBase = _tokenBlackListBase;
        }

        public bool InsertTokenBlackList()
        {
            return tokenBlackListBase.InsertTokenBlackList();
        }
    }
}
