using System;
using FuegoSoft.Pegasus.Lib.Business.Abstract;
using FuegoSoft.Pegasus.Lib.Data.Model;

namespace FuegoSoft.Pegasus.Lib.Business.Planner
{
    public class UserPlanner
    {
        private UserAbstract _userLoginStrategy;

        public void SetUserStrategy(UserAbstract userLoginStrategy)
        {
            _userLoginStrategy = userLoginStrategy;

        }

        public UserCredential GetUserCredential()
        {
            return _userLoginStrategy.GetUserCredentialByUsernameAndPassword();
        }

        public int GetUserId()
        {
            return _userLoginStrategy.GetUserIdByUsernameAndPassword();
        }

        public Guid GetUserKey()
        {
            return _userLoginStrategy.GetUserKeyByUsernameAndPassword();
        }
    }
}
