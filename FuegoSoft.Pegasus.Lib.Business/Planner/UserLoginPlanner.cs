using FuegoSoft.Pegasus.Lib.Business.Abstract;
using FuegoSoft.Pegasus.Lib.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuegoSoft.Pegasus.Lib.Business.Planner
{
    public class UserLoginPlanner
    {
        private UserLoginAbstract userLoginAbstract;

        public void SetUserLoginPlanner(UserLoginAbstract _userLoginAbstract)
        {
            this.userLoginAbstract = _userLoginAbstract;
        }
    }
}
