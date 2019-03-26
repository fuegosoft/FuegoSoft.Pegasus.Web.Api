using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuegoSoft.Pegasus.Lib.Core.Utilities;
using FuegoSoft.Pegasus.Lib.Core.Utilities.Interface;
using FuegoSoft.Pegasus.Lib.Data.Interface.Repository;
using FuegoSoft.Pegasus.Lib.Data.Interface.UnitOfWork;
using FuegoSoft.Pegasus.Lib.Data.Model;
using FuegoSoft.Pegasus.Lib.Data.Repository;
using FuegoSoft.Pegasus.Lib.Data.UnitOfWork;
using FuegoSoft.Pegasus.Web.Service.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace FuegoSoft.Pegasus.Web.Service
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private SwaggerConfigurator _swaggerConfig;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _swaggerConfig = new SwaggerConfigurator();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc(options =>
            {
                options.ReturnHttpNotAcceptable = true;
            });

            // Add swagger UI to this project
            services.AddSwaggerGen(swagg =>
            {
                swagg.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Project Pegasus",
                    Description = "Project Pegasus is a Web API that enable other devices to connect to this service.",
                    TermsOfService = "Terms of Service, You are hereby use this Web API as a warranty to our service.  Thanks you!",
                    Contact = new Contact
                    {
                        Name = "Alvin J. Quezon",
                        Email = "ajqportal@outlook.com",
                        Url = "https://www.linkedin.com/in/ajqportal"
                    }
                });

                swagg.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = Configuration["Bearer:Description"],
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                swagg.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {
                        "Bearer", Enumerable.Empty<string>()
                    }
                });
            });

            // Add Jwt service
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Token:Issuer"],
                        ValidAudience = Configuration["Token:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecretKey"]))
                    };
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                });

            services.AddAuthorization(option =>
            {
                option.AddPolicy("Atleast18", policy =>
                {
                    policy.Requirements.Add(new JwtUtilities(18));
                });
            });

            services.AddSingleton<IAuthorizationHandler, JwtAuthorizationHandler>();
            // Use MVC
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            services.AddDbContext<AyudaContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enforce app to use static files.
            app.UseStaticFiles();

            // Jwt Authentication
            app.UseAuthentication();

            //app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().AllowAnyMethod());

            if (_swaggerConfig.IsSwaggerEnable())
            {
                // Swagger Configuration
                app.UseSwagger();
                app.UseSwaggerUI(swaggUI =>
                {
                    swaggUI.SwaggerEndpoint("/swagger/v1/swagger.json", "Project Pegasus Web API");
                    swaggUI.DefaultModelsExpandDepth(-1); // removes model table from UI
                });
            }

            app.UseMvc();
            app.UseHttpsRedirection();

        }
    }
}
