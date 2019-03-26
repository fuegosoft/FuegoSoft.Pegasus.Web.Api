using FuegoSoft.Pegasus.Lib.Data.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuegoSoft.Pegasus.Lib.Data.Interface.UnitOfWork
{
    public interface ITokenBlackListUnitOfWork : IDisposable
    {
        ITokenBlackListRepository TokenBlackLists { get; }
        int Complete();
    }
}
