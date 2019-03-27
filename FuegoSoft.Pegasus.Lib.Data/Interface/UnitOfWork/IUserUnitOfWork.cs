using System;
using FuegoSoft.Pegasus.Lib.Data.Interface.Repository;

namespace FuegoSoft.Pegasus.Lib.Data.Interface.UnitOfWork
{
    public interface IUserUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IUserTokenRepository UserTokens { get; }
        IUserLoginRepository UserLogins { get; }
        IUserProfileRepository UserProfiles { get; }
        int Complete();
    }
}
