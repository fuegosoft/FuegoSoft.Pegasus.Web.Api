using FuegoSoft.Pegasus.Lib.Business.Abstract;
using FuegoSoft.Pegasus.Lib.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuegoSoft.Pegasus.Lib.Business.Planner
{
    public class UserProfilePlanner
    {
        private UserProfileBase userProfileBase;
        public void SetUserProfilePlanner(UserProfileBase userProfileBase)
        {
            this.userProfileBase = userProfileBase;
        }

        public UserProfile GetUserProfile() => userProfileBase.GetUserProfileByUserId();

        public bool CreateUserProfile() => userProfileBase.CreateUserProfile();
    }
}
