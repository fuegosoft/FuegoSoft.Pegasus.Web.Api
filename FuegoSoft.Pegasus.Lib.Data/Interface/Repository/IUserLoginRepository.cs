using System;
using FuegoSoft.Pegasus.Lib.Core.Utilities.Interface;
using FuegoSoft.Pegasus.Lib.Data.Model;

namespace FuegoSoft.Pegasus.Lib.Data.Interface.Repository
{
    public interface IUserLoginRepository : IRepository<UserLogin>
    {
        UserLogin GetUserLoginByLoginKey(Guid loginKey);
    }
}
