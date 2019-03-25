using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace FuegoSoft.Pegasus.Web.Service.Helpers
{
    public class JwtAuthorizationHandler : AuthorizationHandler<JwtUtilities>
    {
        private IConfiguration _configuration;

        public JwtAuthorizationHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, JwtUtilities requirement)
		{
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth && c.Issuer == _configuration["Token:Issuer"]))
            {
                return Task.CompletedTask;
            }

            var dateOfBirth = Convert.ToDateTime(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth && c.Issuer == _configuration["Token:Issuer"]));
            int calculatedAge = DateTime.Today.Year - dateOfBirth.Year;

            if(dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
            {
                calculatedAge--;
            }

            if(calculatedAge >= requirement.MinimumAge)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
		}
    }
}
