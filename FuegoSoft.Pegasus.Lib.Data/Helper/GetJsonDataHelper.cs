using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace FuegoSoft.Pegasus.Lib.Data.Helper
{
    public class GetJsonDataHelper
    {
        public static IConfiguration Configuration { get; private set; }

        string jsonRequest;
        public GetJsonDataHelper(string _jsonRequest)
        {
            this.jsonRequest = _jsonRequest;
        }

        public string GetJsonValue()
        {
            string result = "";
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            result = Configuration[jsonRequest];
            return result;
        }
    }
}
