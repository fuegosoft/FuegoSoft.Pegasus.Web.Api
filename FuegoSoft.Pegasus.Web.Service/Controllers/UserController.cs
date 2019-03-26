using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using FuegoSoft.Pegasus.Lib.Business.Planner;
using FuegoSoft.Pegasus.Lib.Business.Strategy;
using FuegoSoft.Pegasus.Lib.Core.Helpers;
using FuegoSoft.Pegasus.Lib.Data.Model;
using FuegoSoft.Pegasus.Web.Service.Helpers;
using FuegoSoft.Pegasus.Web.Service.Interface;
using FuegoSoft.Pegasus.Web.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuegoSoft.Pegasus.Web.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly IConfiguration configuration;
        private ILoginAuthHelper loginAuthHelper;
        private UserPlanner userPlanner;
        private UserLoginPlanner userLoginPlanner;
        private UserTokenPlanner userTokenPlanner;
        private TokenBlackListPlanner tokenBlackListPlanner;

        public UserController(IConfiguration _configuration)
        {
            configuration = _configuration;
            loginAuthHelper = new LoginAuthHelper(configuration);
            userPlanner = new UserPlanner();
            userLoginPlanner = new UserLoginPlanner();
            userTokenPlanner = new UserTokenPlanner();
            tokenBlackListPlanner = new TokenBlackListPlanner();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult PerformUserLogin([FromBody] Login login)
        {
            if(ModelState.IsValid)
            {
                userPlanner.SetUserStrategy(new UserStrategy(login.Username,login.Password));
                var userCredentials = userPlanner.GetUserCredential();
                if(userCredentials.UserID > 0)
                {
                    userPlanner.SetUserStrategy(new UserStrategy(userCredentials.UserKey));
                    if(!userPlanner.IsUserHasBeenBanned() || !userPlanner.IsUserHasBeenDeleted())
                    {
                        var generatedToken = loginAuthHelper.GetUserToken(userCredentials);
                        userTokenPlanner.SetUserTokenPlanner(new UserTokenStrategy(userCredentials.UserLoginId, userCredentials.UserID, generatedToken));
                        if (userTokenPlanner.InsertUserToken())
                        {
                            return Ok(new { token = generatedToken, loginKey = userCredentials.LoginKey });
                        }
                    }
                    return Unauthorized(new { message = "Requested user has been disabled or deleted by the administrator." }); 
                }
                return NotFound(new { message = "Requested user was not found." });
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPost("logout/{loginKey}")]
        public IActionResult PerformUserLogout([Required]Guid loginKey)
        {
            if(ModelState.IsValid)
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
                if(!string.IsNullOrEmpty(token))
                {
                    userTokenPlanner.SetUserTokenPlanner(new UserTokenStrategy(token));
                    if (userTokenPlanner.CheckUserTokenIsStillActive())
                    {
                        userTokenPlanner.UpdateUserTokenDateUpdated();
                        userLoginPlanner.SetUserLoginPlanner(new UserLoginStrategy(loginKey));
                        if (userLoginPlanner.UpdateUserLoginLogoutTime())
                        {
                            tokenBlackListPlanner.SetTokenBlackListPlanner(new TokenBlackListStrategy(token, loginKey));
                            var isLogout = tokenBlackListPlanner.InsertTokenBlackList();
                            return Ok(new { logout = isLogout });
                        }
                    }
                }
                return NoContent();
            }
            return BadRequest(ModelState);
        }
        /// <summary>
        /// MISC Functions Mainly for checking credentials
        /// </summary>
        /// <returns>The test.</returns>
        /* Checking claims. 
        [Authorize(Roles = "Contractor")]
        [AutoValidateAntiforgeryToken]
        [HttpGet("test")]
        public IActionResult Test()
        {
            var x = HttpContext.User.Claims.Select(c => new { Type = c.Type, Value = c.Value });
            var getUsername = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).FirstOrDefault();
            var getExpiration = HttpContext.User.Claims.Where(c => c.Type == "exp").Select(c => c.Value).FirstOrDefault();
            var formatedTime = StringHelper.UnixTimeStampToDateTime(Convert.ToInt64(getExpiration));
            var getLoginKey = HttpContext.User.Claims.Where(c => c.Type == "jti").Select(c => c.Value).FirstOrDefault();
            return Ok(new { username = getUsername, claims = x, exp = formatedTime.ToString("yyyy-MM-dd hh:mm:ss tt"), jti = getLoginKey});
        }
        */

    }
}
