using FuegoSoft.Pegasus.Lib.Data.Interface.Repository;
using FuegoSoft.Pegasus.Lib.Data.Interface.UnitOfWork;
using FuegoSoft.Pegasus.Lib.Data.Model;
using FuegoSoft.Pegasus.Lib.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuegoSoft.Pegasus.Lib.Data.UnitOfWork
{
    public class TokenBlackListUnitOfWork : ITokenBlackListUnitOfWork
    {
        private readonly AyudaContext Context;
        public TokenBlackListUnitOfWork(AyudaContext context)
        {
            Context = context;
            TokenBlackLists = new TokenBlackListRepository(Context);
        }

        public ITokenBlackListRepository TokenBlackLists { get; private set; }

        public int Complete()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
             Context.Dispose();
        }
    }
}
