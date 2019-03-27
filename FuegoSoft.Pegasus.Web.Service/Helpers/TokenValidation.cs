using System;
using System.Linq;
using FuegoSoft.Pegasus.Lib.Business.Planner;
using FuegoSoft.Pegasus.Lib.Business.Strategy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FuegoSoft.Pegasus.Web.Service.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ValidateTokenIsActiveAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly UserTokenPlanner userTokenPlanner;
        private readonly TokenBlackListPlanner tokenBlackListPlanner;
        public ValidateTokenIsActiveAttribute()
        {
            this.userTokenPlanner = new UserTokenPlanner();
            this.tokenBlackListPlanner = new TokenBlackListPlanner();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if(user.Identity.IsAuthenticated)
            {
                var request = context.HttpContext.Request;
                var header = request.Headers["Authorization"].ToString().Split(" ");
                if(header[0] == "Bearer")
                {
                    var loginKey = user.Claims.Where(w => w.Type == "jti").Select(c => c.Value).FirstOrDefault();
                    userTokenPlanner.SetUserTokenPlanner(new UserTokenStrategy(header[1].Trim()));
                    if (!userTokenPlanner.CheckUserTokenIsStillActive())
                    {
                        tokenBlackListPlanner.SetTokenBlackListPlanner(new TokenBlackListStrategy(header[1].Trim(), new Guid(loginKey)));
                        tokenBlackListPlanner.InsertTokenBlackList();
                        if (!tokenBlackListPlanner.InsertTokenBlackList())
                            context.Result = new UnauthorizedResult();
                        return;
                    }
                }
            }
        }
    }
}
