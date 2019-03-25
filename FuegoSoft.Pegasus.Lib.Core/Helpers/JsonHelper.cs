using System.IO;
using Microsoft.Extensions.Configuration;

namespace FuegoSoft.Pegasus.Lib.Core.Helpers
{
    public static class JsonHelper
    {
        public static IConfiguration Configuration { get; set; }
        public static string GetJsonValue(string jsonData)
        {
            string result = "";
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            result = Configuration[jsonData];
            return result;
        }
    }
}
