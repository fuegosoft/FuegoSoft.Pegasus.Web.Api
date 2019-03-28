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
        private UserProfilePlanner userProfilePlanner;

        public UserController(IConfiguration _configuration)
        {
            configuration = _configuration;
            loginAuthHelper = new LoginAuthHelper(configuration);
            userPlanner = new UserPlanner();
            userLoginPlanner = new UserLoginPlanner();
            userTokenPlanner = new UserTokenPlanner();
            tokenBlackListPlanner = new TokenBlackListPlanner();
            userProfilePlanner = new UserProfilePlanner();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult PerformUserLogin([FromBody] LoginModel login)
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
                            return Ok(new { token = generatedToken });
                        }
                    }
                    return Unauthorized(new { message = "Requested user has been disabled or deleted by the administrator." }); 
                }
                return NotFound(new { message = "Requested user was not found." });
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [ValidateTokenIsActive]
        [HttpPost("logout")]
        public IActionResult PerformUserLogout()
        {
            if(ModelState.IsValid)
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
                if(!string.IsNullOrEmpty(token))
                {
                    userTokenPlanner.SetUserTokenPlanner(new UserTokenStrategy(token));
                    if (userTokenPlanner.CheckUserTokenIsStillActive())
                    {
                        var getLoginKey = HttpContext.User.Claims.Where(c => c.Type == "jti").Select(c => c.Value).FirstOrDefault();
                        userTokenPlanner.UpdateUserTokenDateUpdated();
                        userLoginPlanner.SetUserLoginPlanner(new UserLoginStrategy(new Guid(getLoginKey)));
                        if (userLoginPlanner.UpdateUserLoginLogoutTime())
                        {
                            tokenBlackListPlanner.SetTokenBlackListPlanner(new TokenBlackListStrategy(token, new Guid(getLoginKey)));
                            var isLogout = tokenBlackListPlanner.InsertTokenBlackList();
                            return Ok(new { logout = isLogout });
                        }
                    }
                }
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [ValidateTokenIsActive]
        [HttpPost("token/renew")]
        public IActionResult RenewToken()
        {
            var getUserKey = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Actor).Select(c => c.Value).FirstOrDefault();
            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            if (!string.IsNullOrEmpty(getUserKey) || !string.IsNullOrEmpty(token))
            {
                userPlanner.SetUserStrategy(new UserStrategy(new Guid(getUserKey.ToString())));
                var userCredential = userPlanner.GetUserCredentialByUserKey();
                if(userCredential.UserID > 0)
                {
                    var loginKey = HttpContext.User.Claims.Where(c => c.Type == "jti").Select(c => c.Value).FirstOrDefault();
                    userTokenPlanner.SetUserTokenPlanner(new UserTokenStrategy(token));
                    if (userTokenPlanner.UpdateUserTokenDateUpdated())
                    {
                        tokenBlackListPlanner.SetTokenBlackListPlanner(new TokenBlackListStrategy(token, new Guid(loginKey)));
                        var insertPreviousToken = tokenBlackListPlanner.InsertTokenBlackList();
                        if (insertPreviousToken)
                        {
                            var generatedToken = loginAuthHelper.GetUserToken(userCredential);
                            userTokenPlanner.SetUserTokenPlanner(new UserTokenStrategy(userCredential.UserLoginId, userCredential.UserID, generatedToken));
                            if (userTokenPlanner.InsertUserToken())
                            {
                                return Ok(new { token = generatedToken });
                            }
                        }
                    }
                }
                return NotFound("User request data was not found.");
            }
            return BadRequest("Invalid user token request.");
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] UserModel user)
        {
            if (ModelState.IsValid)
            {
                userPlanner.SetUserStrategy(new UserStrategy(user.Username, user.EmailAddress, user.ContactNumber));
                if (!userPlanner.IsUsernameIsAlreadyTaken())
                {
                    userPlanner.SetUserStrategy(new UserStrategy(user.Username, user.Password, user.EmailAddress, user.ContactNumber));
                    var userId = userPlanner.CreateUser();
                    if (userId > 0)
                    {
                        userProfilePlanner.SetUserProfilePlanner(new UserProfileStrategy(userId, user.FirstName, user.MiddleName, user.LastName, user.Gender, user.BirthDate));
                        if (userProfilePlanner.CreateUserProfile())
                            return Created("", "User is successfully created.");
                    }
                }
                return BadRequest("Data already exist.");
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [ValidateTokenIsActive]
        [HttpGet("get/userprofile")]
        public IActionResult GetUserProfile()
        {
            var userId = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault());
            if (userId > 0)
            {
                userProfilePlanner.SetUserProfilePlanner(new UserProfileStrategy(userId));
                var userProfile = userProfilePlanner.GetUserProfile();
                if(userProfile.UserProfileId > 0)
                {
                    return Ok(userProfile);
                }
                return NotFound("User's profile not found.");
            }
            return BadRequest("Invalid requested token.");
        }

        [Authorize]
        [ValidateTokenIsActive]
        [HttpPut("update/password")]
        public IActionResult UserUpdatePassword([FromBody] PasswordModel password)
        {
            if(ModelState.IsValid)
            {
                var getUserKey = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Actor).Select(c => c.Value).FirstOrDefault();
                if(!string.IsNullOrEmpty(getUserKey))
                {
                    userPlanner.SetUserStrategy(new UserStrategy(new Guid(getUserKey), password.OldPassword, password.NewPassword));
                    var isUserPasswordUpdated = userPlanner.UpdateUserPassword();
                    if (isUserPasswordUpdated)
                        return Ok(new { isPasswordUpdated = isUserPasswordUpdated });
                }
                return BadRequest("Invalid user token request.");
            }
            return BadRequest(ModelState);
        }
        /// <summary>
        /// MISC Functions Mainly for checking credentials
        /// </summary>
        /// <returns>The test.</returns>
        /* Checking claims. 
        [Authorize(Roles = "User")]
        [AutoValidateAntiforgeryToken]
        [HttpGet("test")]
        public IActionResult Test()
        {
            var x = HttpContext.User.Claims.Select(c => new { Type = c.Type, Value = c.Value });
            var getUsername = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).FirstOrDefault();
            var getExpiration = HttpContext.User.Claims.Where(c => c.Type == "exp").Select(c => c.Value).FirstOrDefault();
            var formatedTime = StringHelper.UnixTimeStampToDateTime(Convert.ToInt64(getExpiration));
            var getLoginKey = HttpContext.User.Claims.Where(c => c.Type == "jti").Select(c => c.Value).FirstOrDefault();
            var getUserId = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault();
            var getUserKey = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Actor).Select(c => c.Value).FirstOrDefault();
            return Ok(new { username = getUsername, claims = x, exp = formatedTime.ToString("yyyy-MM-dd hh:mm:ss tt"), jti = getLoginKey});
        }
         */
    }
}
