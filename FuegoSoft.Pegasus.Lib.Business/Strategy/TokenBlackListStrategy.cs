using FuegoSoft.Pegasus.Lib.Business.Abstract;
using FuegoSoft.Pegasus.Lib.Data.Model;
using FuegoSoft.Pegasus.Lib.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuegoSoft.Pegasus.Lib.Business.Strategy
{
    public class TokenBlackListStrategy : TokenBlackListBase
    {
        private string token;
        private Guid loginKey;
        public TokenBlackListStrategy(string token, Guid loginKey)
        {
            this.token = token;
            this.loginKey = loginKey;
        }

        public TokenBlackListStrategy(string token)
        {
            this.token = token;
        }

        public override bool InsertTokenBlackList()
        {
            bool result = false;
            if(!string.IsNullOrEmpty(token) && loginKey.ToString().Length == 36)
            {
                using(var tokenBlackListUnitOfWork = new TokenBlackListUnitOfWork(new AyudaContext()))
                {
                    var tokenBlackList = new TokenBlackList
                    {
                        LoginKey = loginKey,
                        Token = token
                    };
                    tokenBlackListUnitOfWork.TokenBlackLists.Add(tokenBlackList);
                    result = tokenBlackListUnitOfWork.Complete() > 0;
                    tokenBlackListUnitOfWork.Dispose();
                }
            }
            return result;
        }

        public override bool IsTokenInBlackList()
        {
            bool result = false;
            if(!string.IsNullOrEmpty(token))
            {
                using(var tokenBlackListUnitOfWork = new TokenBlackListUnitOfWork(new AyudaContext()))
                {
                    result = tokenBlackListUnitOfWork.TokenBlackLists.IsTokenInBlackList(token);
                }
            }
            return result;
        }
    }
}
