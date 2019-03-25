using System;
using FuegoSoft.Pegasus.Lib.Core.Utilities;
using FuegoSoft.Pegasus.Lib.Data.Interface.Repository;
using FuegoSoft.Pegasus.Lib.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace FuegoSoft.Pegasus.Lib.Data.Repository
{
    public class UserTokenRepository : Repository<UserToken>, IUserTokenRepository
    {
        public AyudaContext AyudaContext
        {
            get { return Context as AyudaContext; }
        }

        public UserTokenRepository(DbContext context) : base(context)
        {
        }
    }
}
