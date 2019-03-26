using System;
using System.Linq;
using FuegoSoft.Pegasus.Lib.Core.Utilities;
using FuegoSoft.Pegasus.Lib.Data.Interface.Repository;
using FuegoSoft.Pegasus.Lib.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace FuegoSoft.Pegasus.Lib.Data.Repository
{
    public class UserLoginRepository : Repository<UserLogin>, IUserLoginRepository
    {
        public AyudaContext AyudaContext
        {
            get { return Context as AyudaContext; }
        }

        public UserLoginRepository(DbContext context) : base(context) { }

        public UserLogin GetUserLoginByLoginKey(Guid loginKey) => AyudaContext.UserLogin.Where(w => w.LoginKey == loginKey).FirstOrDefault();
    }
}
