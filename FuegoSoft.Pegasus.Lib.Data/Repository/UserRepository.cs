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
                    var getUserLogin = AyudaContext.UserLogin.Where(w => w.UserId == getUserByUsername.UserId).FirstOrDefault();
                    if(getUserLogin.UserId > 0)
                    {
                        var getUserProfile = AyudaContext.UserProfile.Where(w => w.UserId == getUserByUsername.UserId).FirstOrDefault();
                        if(getUserProfile.UserId > 0)
                        {
                            userCredential.UserID = getUserByUsername.UserId;
                            userCredential.Username = getUserByUsername.Username;
                            userCredential.EmailAddress = getUserByUsername.EmailAddress;
                            userCredential.ContactNumber = getUserByUsername.ContactNumber;
                            userCredential.UserKey = new Guid(getUserByUsername.UserKey.ToString());
                            userCredential.LoginKey = new Guid(getUserLogin.LoginKey.ToString());
                            userCredential.UserType = (int)getUserByUsername.UserType;
                            userCredential.BirthDate = getUserProfile.BirthDate;
                        }
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
    }
}
