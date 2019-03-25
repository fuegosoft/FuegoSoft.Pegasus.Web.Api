using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FuegoSoft.Pegasus.Lib.Data.Model;
using FuegoSoft.Pegasus.Web.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FuegoSoft.Pegasus.Web.Service.Helpers
{
    public class LoginAuthHelper : ILoginAuthHelper
    {
        public IConfiguration configuration;


        public LoginAuthHelper(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        internal SigningCredentials UserSignInCredentials()
        {
            var getSecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecretKey"]));
            return new SigningCredentials(getSecretKey, SecurityAlgorithms.HmacSha512);
        }

        internal Claim[] Claims(UserCredential user)
        {
            return new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress),
                new Claim(JwtRegisteredClaimNames.NameId, user.UserID.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, user.LoginKey.ToString()),
                new Claim(JwtRegisteredClaimNames.Actort, user.UserKey.ToString()),
                new Claim(JwtRegisteredClaimNames.Birthdate, user.BirthDate.ToString()),
                new Claim(ClaimTypes.Role, GetUserType(user.UserType))
            };
        }

        protected JwtSecurityToken SecurityToken(UserCredential user)
        {
            return new JwtSecurityToken(
                issuer: configuration["Token:Issuer"],
                audience: configuration["Token:Issuer"],
                claims: Claims(user),
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["Token:ExpirationMinutes"])),
                notBefore: DateTime.UtcNow,
                signingCredentials: UserSignInCredentials()
            );
        }

        public string GetUserToken(UserCredential user)
        {
            return new JwtSecurityTokenHandler().WriteToken(SecurityToken(user));
        }

        internal string GetUserType(int userType)
        {
            string result = string.Empty;
            if(userType > 0 && userType <= 4)
            {
                switch(userType)
                {
                    case 2:
                        result = "Contractor";
                        break;
                    case 3:
                        result = "Moderator";
                        break;
                    case 4:
                        result = "Administrator";
                        break;
                    default:
                        result = "User";
                        break;
                }
            }
            return result;
        }
    }
}
