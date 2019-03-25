using System;
using FuegoSoft.Pegasus.Lib.Data.Model;
using FuegoSoft.Pegasus.Lib.Data.Repository;
using FuegoSoft.Pegasus.Lib.Data.Interface.Repository;
using FuegoSoft.Pegasus.Lib.Data.Interface.UnitOfWork;

namespace FuegoSoft.Pegasus.Lib.Data.UnitOfWork
{
    public class UserUnitOfWork : IUserUnitOfWork
    {
        private readonly AyudaContext Context;

        public UserUnitOfWork(AyudaContext context)
        {
            Context = context;
            Users = new UserRepository(Context);
            UserTokens = new UserTokenRepository(context);
            UserLogins = new UserLoginRepository(context);
        }

        public IUserRepository Users { get; private set; }

        public IUserTokenRepository UserTokens { get; private set; }

        public IUserLoginRepository UserLogins { get; private set; }

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
