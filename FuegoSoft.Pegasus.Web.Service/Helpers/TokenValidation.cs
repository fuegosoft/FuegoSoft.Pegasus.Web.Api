using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FuegoSoft.Pegasus.Web.Service.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CheckTokenIsValidAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        //UserRepository _userRepository = new UserRepository();

        //public void OnAuthorization(AuthorizationFilterContext context)
        //{
        //    var user = context.HttpContext.User;
        //    if(user.Identity.IsAuthenticated)
        //    {
        //        var request = context.HttpContext.Request;
        //        var getLoginKey = user.Claims.Where(c => c.Type == "jti").Select(c => c.Value).FirstOrDefault();
        //        var header = request.Headers["Authorization"].ToString().Split(" ");
        //        if (getLoginKey.Length == 36 && header[0] == "Bearer")
        //        {
        //            var isTokenActive = _userRepository.CheckTokenIsNotExpired(new Guid(getLoginKey), header[1]);
        //            if (!isTokenActive.Response)
        //                context.Result = new UnauthorizedResult();
        //            return;
        //        }
        //    }
        //    context.Result = new UnauthorizedResult();
        //    return;
        //}
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }
    }
}
