using System;
using FuegoSoft.Pegasus.Lib.Business.Abstract;
using FuegoSoft.Pegasus.Lib.Core.Helpers;
using FuegoSoft.Pegasus.Lib.Data.Model;
using FuegoSoft.Pegasus.Lib.Data.UnitOfWork;

namespace FuegoSoft.Pegasus.Lib.Business.Strategy
{
    public class UserTokenStrategy : UserTokenAbstract
    {
        string token;
        Guid loginKey;
        int userId;
        int userLoginId;

        public UserTokenStrategy(int _userLoginId, int _userId, Guid _loginKey, string _token)
        {
            this.userLoginId = _userLoginId;
            this.userId = _userId;
            this.token = _token;
            this.loginKey = _loginKey;
        }

        public override bool InsertUserToken()
        {
            bool result = false;
            if(!string.IsNullOrEmpty(token) && loginKey.ToString().Length == 36)
            {
                using(var userUnitOfWork = new UserUnitOfWork(new AyudaContext()))
                {
                    userUnitOfWork.UserTokens.Add(new UserToken
                    {
                        Token = token,
                        ExpirationDate = DateTime.UtcNow.AddMinutes(Convert.ToInt32(JsonHelper.GetJsonValue("Token:ExpirationMinutes"))),
                        UserId = userId,
                        UserLoginId = userLoginId
                    });
                    userUnitOfWork.Complete();
                    userUnitOfWork.Dispose();
                }
            }
            return result;
        }
    }
}
