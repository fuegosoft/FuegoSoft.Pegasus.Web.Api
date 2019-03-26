using FuegoSoft.Pegasus.Lib.Core.Utilities;
using FuegoSoft.Pegasus.Lib.Data.Interface.Repository;
using FuegoSoft.Pegasus.Lib.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuegoSoft.Pegasus.Lib.Data.Repository
{
    public class TokenBlackListRepository : Repository<TokenBlackList>, ITokenBlackListRepository
    {
        public AyudaContext AyudaContext { get { return Context as AyudaContext; } }

        public TokenBlackListRepository(DbContext context) : base(context)
        {
        }
    }
}
