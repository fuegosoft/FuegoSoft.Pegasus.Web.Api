using System;
using Microsoft.Extensions.Configuration;

namespace FuegoSoft.Pegasus.Lib.Data.Interface.DbContext
{
    public interface IDbContext
    {
        IConfiguration Configuration { get; }
    }
}
