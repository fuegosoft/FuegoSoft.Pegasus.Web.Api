using System;
using FuegoSoft.Pegasus.Lib.Data.Model;

namespace FuegoSoft.Pegasus.Lib.Business.Abstract
{
    public abstract class UserAbstract
    {
        public abstract UserCredential GetUserCredentialByUsernameAndPassword();
        public abstract int GetUserIdByUsernameAndPassword();
        public abstract Guid GetUserKeyByUsernameAndPassword();
    }
}
