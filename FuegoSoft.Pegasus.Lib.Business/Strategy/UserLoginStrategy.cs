using System;
using FuegoSoft.Pegasus.Lib.Business.Abstract;
using FuegoSoft.Pegasus.Lib.Core.Helpers;
using FuegoSoft.Pegasus.Lib.Data.Model;
using FuegoSoft.Pegasus.Lib.Data.UnitOfWork;

namespace FuegoSoft.Pegasus.Lib.Business.Strategy
{
    public class UserLoginStrategy : UserLoginBase
    {
        int userId;
        private Guid loginKey;
        public UserLoginStrategy(int _userid)
        {
            this.userId = _userid;
        }

        public UserLoginStrategy(Guid loginKey)
        {
            this.loginKey = loginKey;
        }

        public override bool InsertUserLogin()
        {
            bool result = false;
            if(userId > 0)
            {
                using(var userUnitOfWork = new UserUnitOfWork(new AyudaContext()))
                {
                    userUnitOfWork.UserLogins.Add(new UserLogin
                    {
                        UserId = userId,
                        ExpirationDate = DateTime.Now.AddMinutes(Convert.ToInt32(JsonHelper.GetJsonValue("Token:ExpirationMinutes")))
                    });
                    userUnitOfWork.Complete();
                    userUnitOfWork.Dispose();
                }
            }
            return result;
        }

        public override bool UpdateUserLogoutTime()
        {
            bool result = false;
            if(loginKey.ToString().Length == 36)
            {
                using(var userUnitOfWork = new UserUnitOfWork(new AyudaContext()))
                {
                    var userLogins = userUnitOfWork.UserLogins.GetUserLoginByLoginKey(loginKey);
                    userLogins.DateUpdated = DateTime.Now;
                    userLogins.LogoutTime = DateTime.Now;
                    userUnitOfWork.UserLogins.Update(userLogins);
                    result = userUnitOfWork.Complete() > 0;
                    userUnitOfWork.Dispose();
                }
            }
            return result;
        }
    }
}
