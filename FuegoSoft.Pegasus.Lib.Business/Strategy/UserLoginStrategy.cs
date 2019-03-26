using System;
using FuegoSoft.Pegasus.Lib.Business.Abstract;
using FuegoSoft.Pegasus.Lib.Core.Helpers;
using FuegoSoft.Pegasus.Lib.Data.Model;
using FuegoSoft.Pegasus.Lib.Data.UnitOfWork;

namespace FuegoSoft.Pegasus.Lib.Business.Strategy
{
    public class UserLoginStrategy : UserLoginAbstract
    {
        int userId;

        public UserLoginStrategy(int _userid)
        {
            this.userId = _userid;
        }

        public override bool InsertUserLogin()
        {
            var result = false;
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
    }
}
