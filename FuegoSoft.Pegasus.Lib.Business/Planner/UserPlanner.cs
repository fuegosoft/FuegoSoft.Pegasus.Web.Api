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

        public UserCredential GetUserCredential() => _userLoginStrategy.GetUserCredentialByUsernameAndPassword();

        public int GetUserId() => _userLoginStrategy.GetUserIdByUsernameAndPassword();

        public Guid GetUserKey() => _userLoginStrategy.GetUserKeyByUsernameAndPassword();

        public bool IsUserHasBeenBanned() => _userLoginStrategy.IsUserHasBeenBanned();

        public bool IsUserHasBeenDeleted() => _userLoginStrategy.IsUserHasBeenDeleted();
    }
}
