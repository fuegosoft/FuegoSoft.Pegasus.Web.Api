using FuegoSoft.Pegasus.Lib.Business.Abstract;
using FuegoSoft.Pegasus.Lib.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuegoSoft.Pegasus.Lib.Business.Planner
{
    public class UserLoginPlanner
    {
        private UserLoginBase userLoginBase;

        public void SetUserLoginPlanner(UserLoginBase _userLoginBase)
        {
            this.userLoginBase = _userLoginBase;
        }

        public bool UpdateUserLoginLogoutTime() => userLoginBase.UpdateUserLogoutTime();
    }
}
