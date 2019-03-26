using FuegoSoft.Pegasus.Lib.Business.Abstract;
using FuegoSoft.Pegasus.Lib.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuegoSoft.Pegasus.Lib.Business.Planner
{
    public class UserTokenPlanner
    {
        private UserTokenBase userTokenBase;
        public void SetUserTokenPlanner(UserTokenBase _userTokenBase)
        {
            this.userTokenBase = _userTokenBase;
        }

        public UserToken InsertUserTokenAndRetrieveInsertedId() => userTokenBase.InsertUserTokenAndRetrieveInsertedId();

        public bool InsertUserToken() => userTokenBase.InsertUserToken();

        public bool UpdateUserTokenDateUpdated() => userTokenBase.UpdateUserTokenDateUpdated();

        public bool CheckUserTokenIsStillActive() => userTokenBase.CheckTokenIsStillActive();
    }
}
