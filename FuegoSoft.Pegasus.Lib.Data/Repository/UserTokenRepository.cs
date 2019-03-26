using System;
using System.Linq;
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

        public UserTokenRepository(DbContext context) : base(context) { }

        public UserToken GetUserTokenByToken(string token) => AyudaContext.UserToken.Where(w => w.Token == token).FirstOrDefault();

        public bool IsTokenStillActiveByToken(string token) => AyudaContext.UserToken.Where(w => w.Token == token && (w.ExpirationDate - DateTime.Now).Minutes > 0).FirstOrDefault().UserTokenId > 0;
    }
}
