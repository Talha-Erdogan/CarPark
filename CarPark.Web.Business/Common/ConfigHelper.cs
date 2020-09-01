using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.Web.Business.Common
{
    public static class ConfigHelper
    {
        private static IConfiguration _configuration { get; set; }

        public static void Configure(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string ApiUrl
        {
            get
            {
                //return _configuration.GetValue<string>("AppSettings:ApiUrl");
                return _configuration.GetSection("AppSettings:ApiUrl").Value;
            }
        }

        public static string ApiBaseUrl
        {
            get
            {
                //return System.Configuration.ConfigurationManager.AppSettings["PortalApiUrl"];
                // todo: appsettings.json dosyasından okunması sağlanacak
                //return _configuration.GetValue<string>("AppSettings:ApiBaseUrl");
                return _configuration.GetSection("AppSettings:ApiBaseUrl").Value;
            }
        }

       
    }
}
