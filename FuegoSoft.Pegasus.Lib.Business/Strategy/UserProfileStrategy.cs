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
        private string firstName;
        private string middleName;
        private string lastName;
        private string gender;
        private DateTime birthDate;

        public UserProfileStrategy(int userId)
        {
            this.userId = userId;
        }

        public UserProfileStrategy(int userId, string firstName, string middleName, string lastName, string gender, DateTime birthDate)
        {
            this.userId = userId;
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
            this.gender = gender;
            this.birthDate = birthDate;
        }

        public override bool CreateUserProfile()
        {
            bool result = false;
            if (userId > 0 && !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                using (var userUnitOfWork = new UserUnitOfWork(new AyudaContext()))
                {
                    var userProfile = new UserProfile
                    {
                        UserId = userId,
                        FirstName = firstName,
                        MiddleName = middleName,
                        LastName = lastName,
                        Gender = gender,
                        BirthDate = birthDate
                    };
                    userUnitOfWork.UserProfiles.Add(userProfile);
                    result = userUnitOfWork.Complete() > 0;
                    userUnitOfWork.Dispose();
                }
            }
            return result;
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
