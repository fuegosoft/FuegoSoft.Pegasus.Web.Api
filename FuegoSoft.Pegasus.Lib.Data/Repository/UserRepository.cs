using System;
using System.Collections.Generic;
using FuegoSoft.Pegasus.Lib.Core.Utilities;
using FuegoSoft.Pegasus.Lib.Data.Model;
using System.Linq;
using FuegoSoft.Pegasus.Lib.Core.Helpers;
using FuegoSoft.Pegasus.Lib.Data.Interface.Repository;

namespace FuegoSoft.Pegasus.Lib.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public AyudaContext AyudaContext
        {
            get { return Context as AyudaContext; }
        }

        public UserRepository(AyudaContext context) : base(context) { }

        public int GetUserIdByUsernameAndPassword(string username, string password)
        {
            int userId = 0;
            var getUserByUsername = AyudaContext.User.Where(w => w.Username == username).FirstOrDefault();
            if(getUserByUsername != null)
            {
                if(SecurePasswordHelper.Verify(password, getUserByUsername.Password))
                {
                    userId = getUserByUsername.UserId;
                }
            }
            return userId;
        }

        public UserCredential GetUserCredentialByUsernameAndPassword(string username, string password)
        {
            var userCredential = new UserCredential();
            var getUserByUsername = AyudaContext.User.Where(w => w.Username == username).FirstOrDefault();
            if (getUserByUsername.UserKey.ToString().Length == 36)
            {
                if (SecurePasswordHelper.Verify(password, getUserByUsername.Password))
                {
                    var userLogin = new UserLogin
                    {
                        UserId = getUserByUsername.UserId,
                        LoginTime = DateTime.Now,
                        LogoutTime = null
                    };
                    AyudaContext.UserLogin.Add(userLogin);
                    if (AyudaContext.SaveChanges() > 0)
                    {
                        var getUserProfile = AyudaContext.UserProfile.Where(w => w.UserId == getUserByUsername.UserId).FirstOrDefault();
                        if(getUserProfile.UserProfileId > 0)
                            userCredential.UserID = getUserByUsername.UserId;
                            userCredential.Username = getUserByUsername.Username;
                            userCredential.UserKey = new Guid(getUserByUsername.UserKey.ToString());
                            userCredential.EmailAddress = getUserByUsername.EmailAddress;
                            userCredential.ContactNumber = getUserByUsername.ContactNumber;
                            userCredential.LoginKey = new Guid(userLogin.LoginKey.ToString());
                            userCredential.UserLoginId = userLogin.UserLoginId;
                            userCredential.UserType = (int)getUserByUsername.UserType;
                            userCredential.BirthDate = getUserProfile.BirthDate;
                    }
                        
                        
                }
            }
            return userCredential;
        }

        public Guid GetUserKeyByUsernameAndPassword(string username, string password)
        {
            Guid userKey = new Guid();
            var getUserByUsername = AyudaContext.User.Where(w => w.Username == username).FirstOrDefault();
            if (getUserByUsername != null)
            {
                if (SecurePasswordHelper.Verify(password, getUserByUsername.Password))
                {
                    userKey = new Guid(getUserByUsername.UserKey.ToString());
                }
            }
            return userKey;
        }

        public bool IsUserBanned(Guid userKey)
        {
            bool result = false;
            var user = AyudaContext.User.Where(w => w.UserKey == userKey && w.IsDisabled == true).FirstOrDefault();
            if (user != null)
                result = !string.IsNullOrEmpty(AyudaContext.UserBanned.Where(w => w.UserId == user.UserId).FirstOrDefault().Reason);
            return result;
        }

        public bool IsUserDeleted(Guid userKey)
        {
            bool result = false;
            var user = AyudaContext.User.Where(w => w.UserKey == userKey && w.IsDeleted == true).FirstOrDefault();
            if(user != null)
                result = !string.IsNullOrEmpty(AyudaContext.UserBanned.Where(w => w.UserId == user.UserId).FirstOrDefault().Reason);
            return result;
        }
    }
}
