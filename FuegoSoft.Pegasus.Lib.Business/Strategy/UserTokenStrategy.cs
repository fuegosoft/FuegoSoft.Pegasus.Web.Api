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
        int userId;
        int userLoginId;

        public UserTokenStrategy(int _userLoginId, int _userId, string _token)
        {
            this.userLoginId = _userLoginId;
            this.userId = _userId;
            this.token = _token;
        }

        public override bool InsertUserToken()
        {
            bool result = false;
            if(!string.IsNullOrEmpty(token))
            {
                using(var userUnitOfWork = new UserUnitOfWork(new AyudaContext()))
                {
                    userUnitOfWork.UserTokens.Add(new UserToken
                    {
                        Token = token,
                        ExpirationDate = DateTime.Now.AddMinutes(Convert.ToInt32(JsonHelper.GetJsonValue("Token:ExpirationMinutes"))),
                        UserId = userId,
                        UserLoginId = userLoginId
                    });
                    userUnitOfWork.Complete();
                    userUnitOfWork.Dispose();
                    result = true;
                }
            }
            return result;
        }

        public override UserToken InsertUserTokenAndRetrieveInsertedId()
        {
            var userToken = new UserToken();
            if(!string.IsNullOrEmpty(token))
            {
                using(var userUnitOfWork = new UserUnitOfWork(new AyudaContext()))
                {
                    userToken = new UserToken
                    {
                        UserId = userId,
                        UserLoginId = userLoginId,
                        Token = token,
                        ExpirationDate = DateTime.Now.AddMinutes(Convert.ToInt32(JsonHelper.GetJsonValue("Token:ExpirationMinutes")))
                    };
                    userUnitOfWork.UserTokens.Add(userToken);
                    userUnitOfWork.Complete();
                    userUnitOfWork.Dispose();
                }
            }
            return userToken;
        }
    }
}
