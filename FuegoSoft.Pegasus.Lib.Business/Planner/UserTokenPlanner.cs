using FuegoSoft.Pegasus.Lib.Business.Abstract;
using FuegoSoft.Pegasus.Lib.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuegoSoft.Pegasus.Lib.Business.Planner
{
    public class UserTokenPlanner
    {
        private UserTokenAbstract userTokenAbstract;
        public void SetUserTokenPlanner(UserTokenAbstract _userTokenAbstract)
        {
            this.userTokenAbstract = _userTokenAbstract;
        }

        public UserToken InsertUserTokenAndRetrieveInsertedId()
        {
            return userTokenAbstract.InsertUserTokenAndRetrieveInsertedId();
        }

        public bool InsertUserToken()
        {
            return userTokenAbstract.InsertUserToken();
        }
    }
}
