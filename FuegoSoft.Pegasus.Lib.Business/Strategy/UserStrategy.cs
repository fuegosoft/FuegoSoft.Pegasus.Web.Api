using System;
using FuegoSoft.Pegasus.Lib.Business.Abstract;
using FuegoSoft.Pegasus.Lib.Data.Model;
using FuegoSoft.Pegasus.Lib.Data.UnitOfWork;

namespace FuegoSoft.Pegasus.Lib.Business.Strategy
{
    public class UserStrategy : UserAbstract
    {
        private string username;
        private string password;
        public UserStrategy(string username, string password)
        {
            this.username = username;
            this.password = password;
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
    }
}
