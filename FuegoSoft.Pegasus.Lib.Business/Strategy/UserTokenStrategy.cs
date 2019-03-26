using System;
using FuegoSoft.Pegasus.Lib.Business.Abstract;
using FuegoSoft.Pegasus.Lib.Core.Helpers;
using FuegoSoft.Pegasus.Lib.Data.Model;
using FuegoSoft.Pegasus.Lib.Data.UnitOfWork;

namespace FuegoSoft.Pegasus.Lib.Business.Strategy
{
    public class UserTokenStrategy : UserTokenBase
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

        public UserTokenStrategy(string token)
        {
            this.token = token;
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

        public override bool UpdateUserTokenDateUpdated()
        {
            bool result = false;
            if(!string.IsNullOrEmpty(token))
            {
                using(var userUnitOfWork = new UserUnitOfWork(new AyudaContext()))
                {
                    var userToken = userUnitOfWork.UserTokens.GetUserTokenByToken(token);
                    if(userToken.UserTokenId > 0)
                    {
                        userToken.DateUpdated = DateTime.Now;
                        userUnitOfWork.UserTokens.Update(userToken);
                        result = userUnitOfWork.Complete() > 0;
                        userUnitOfWork.Dispose();
                    }
                }
            }
            return result;
        }

        public override bool CheckTokenIsStillActive()
        {
            bool result = false;
            if(!string.IsNullOrEmpty(token))
            {
                using (var userUnitOfWork = new UserUnitOfWork(new AyudaContext()))
                {
                    result = userUnitOfWork.UserTokens.IsTokenStillActiveByToken(token);
                }
            }
            return result;
        }
    }
}
