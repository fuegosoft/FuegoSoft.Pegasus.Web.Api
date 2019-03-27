using FuegoSoft.Pegasus.Lib.Business.Abstract;
using FuegoSoft.Pegasus.Lib.Data.Model;
using FuegoSoft.Pegasus.Lib.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuegoSoft.Pegasus.Lib.Business.Strategy
{
    public class UserProfileStrategy : UserProfileBase
    {
        private int userId;
        public UserProfileStrategy(int userId)
        {
            this.userId = userId;
        }

        public override UserProfile GetUserProfileByUserId()
        {
            var userProfile = new UserProfile();
            if(userId > 0)
            {
                using(var userUnitOfWork = new UserUnitOfWork(new AyudaContext()))
                {
                    userProfile = userUnitOfWork.UserProfiles.GetUserProfileByUserId(userId);
                }
            }
            return userProfile;
        }
    }
}
