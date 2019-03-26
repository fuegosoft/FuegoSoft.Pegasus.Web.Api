using System;
using FuegoSoft.Pegasus.Lib.Core.Utilities.Interface;
using FuegoSoft.Pegasus.Lib.Data.Model;

namespace FuegoSoft.Pegasus.Lib.Data.Interface.Repository
{
    public interface IUserTokenRepository : IRepository<UserToken>
    {
        UserToken GetUserTokenByToken(string token);
        bool IsTokenStillActiveByToken(string token);
    }
}
