using FuegoSoft.Pegasus.Lib.Data.Model;
using System;
namespace FuegoSoft.Pegasus.Lib.Business.Abstract
{
    public abstract class UserTokenBase
    {
        public abstract bool InsertUserToken();
        public abstract UserToken InsertUserTokenAndRetrieveInsertedId();
        public abstract bool UpdateUserTokenDateUpdated();
        public abstract bool CheckTokenIsStillActive();
    }
}
