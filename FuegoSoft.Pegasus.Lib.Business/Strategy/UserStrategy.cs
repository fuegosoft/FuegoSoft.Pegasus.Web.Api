using System;
using FuegoSoft.Pegasus.Lib.Business.Abstract;
using FuegoSoft.Pegasus.Lib.Data.Model;
using FuegoSoft.Pegasus.Lib.Data.UnitOfWork;

namespace FuegoSoft.Pegasus.Lib.Business.Strategy
{
    public class UserStrategy : UserBase
    {
        private string username;
        private string password;
        private string emailAddress;
        private string contactNumber;
        private Guid userKey;

        public UserStrategy(Guid userKey)
        {
            this.userKey = userKey;
        }

        public UserStrategy(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public UserStrategy(string username, string password, string emailAddress, string contactNumber)
        {
            this.username = username;
            this.password = password;
            this.emailAddress = emailAddress;
            this.contactNumber = contactNumber;
        }

        public UserStrategy(string username, string emailAddress, string contactNumber)
        {
            this.username = username;
            this.emailAddress = emailAddress;
            this.contactNumber = contactNumber;
        }

        public override UserCredential GetUserCredentialByUsernameAndPassword()
        {

            var userCredential = new UserCredential();
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                using (var userUnitOfWork = new UserUnitOfWork(new AyudaContext()))
                {
                    userCredential = userUnitOfWork.Users.GetUserCredentialByUsernameAndPassword(username, password);
                }
            }
            return userCredential;
            
        }

        public override int GetUserIdByUsernameAndPassword()
        {
            int userId = 0;
            if(!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                using(var userUnitOfWork = new UserUnitOfWork(new AyudaContext()))
                {
                    userId = userUnitOfWork.Users.GetUserIdByUsernameAndPassword(username, password);
                }
            }
            return userId;
        }

        public override Guid GetUserKeyByUsernameAndPassword()
        {
            Guid userKey = new Guid();
            if(!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                using(var userUnitOfWork = new UserUnitOfWork(new AyudaContext()))
                {
                    userKey = userUnitOfWork.Users.GetUserKeyByUsernameAndPassword(username, password);
                }
            }
            return userKey;
        }

        public override bool IsUserHasBeenBanned()
        {
            var result = false;
            if(userKey.ToString().Length == 36)
            {
                using(var userUnitOfWork = new UserUnitOfWork(new AyudaContext()))
                {
                    result = userUnitOfWork.Users.IsUserBanned(userKey);
                }
            }
            return result;
        }

        public override bool IsUserHasBeenDeleted()
        {
            var result = false;
            if (userKey.ToString().Length == 36)
            {
                using (var userUnitOfWork = new UserUnitOfWork(new AyudaContext()))
                {
                    result = userUnitOfWork.Users.IsUserDeleted(userKey);
                }
            }
            return result;
        }

        public override UserCredential GetUserCredentialByUserKey()
        {
            var userCredential = new UserCredential();
            if(userKey.ToString().Length == 36)
            {
                using(var userUnitOfWork = new UserUnitOfWork(new AyudaContext()))
                {
                    userCredential = userUnitOfWork.Users.GetUserCredentialByUserKey(userKey);
                }
            }
            return userCredential;
        }

        public override int CreateUserAndRetrieveUserId()
        {
            int userId = 0;
            if(!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                using(var userUnitOfWOrk = new UserUnitOfWork(new AyudaContext()))
                {
                    var user = new User
                    {
                        Username = username,
                        Password = password,
                        EmailAddress = emailAddress,
                        ContactNumber = contactNumber
                    };
                    userUnitOfWOrk.Users.Add(user);
                    userUnitOfWOrk.Complete();

                    userId = user.UserId;
                    userUnitOfWOrk.Dispose();
                }
            }
            return userId;
        }

        public override bool IsUsernameIsAlreadyTaken()
        {
            bool result = false;
            if(!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(emailAddress) && !string.IsNullOrEmpty(contactNumber))
            {
                using(var userUnitOfWork = new UserUnitOfWork(new AyudaContext()))
                {
                    result = userUnitOfWork.Users.IsUsernameIsAlreadyTaken(username, emailAddress, contactNumber);
                }
            }
            return result;
        }
    }
}
