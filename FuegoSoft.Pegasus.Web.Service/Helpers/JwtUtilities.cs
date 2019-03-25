using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace FuegoSoft.Pegasus.Web.Service.Helpers
{
    public class JwtUtilities : IAuthorizationRequirement
    {
        public int MinimumAge { get; private set; }

        public JwtUtilities(int minimumAge)
        {
            MinimumAge = minimumAge;
        }
    }
}
