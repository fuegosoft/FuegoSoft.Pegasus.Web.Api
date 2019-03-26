using FuegoSoft.Pegasus.Lib.Data.Model;
using System;
namespace FuegoSoft.Pegasus.Lib.Business.Abstract
{
    public abstract class UserTokenAbstract
    {
        public abstract bool InsertUserToken();
        public abstract UserToken InsertUserTokenAndRetrieveInsertedId();
    }
}
