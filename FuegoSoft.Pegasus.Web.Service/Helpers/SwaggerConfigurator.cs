using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace FuegoSoft.Pegasus.Web.Service.Helpers
{
    public class SwaggerConfigurator
    {
        private IConfiguration Configuration { get; set; }

        public string GetJsonData(string connectionString)
        {
            string result = "";
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            result = Configuration[connectionString];
            return result;
        }

        public bool IsSwaggerEnable()
        {
            bool result = false;
            result = GetJsonData("EnableSwaggerUI:IsEnable").ToLower() != "false";
            return result;
        }
    }
}
