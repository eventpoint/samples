using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventPoint.Services.Api;
using System.Configuration;

namespace EventPoint.Samples.Clients.Web
{
    public static class Helper
    {
        public static Client ApiClient()
        {
            return new EventPoint.Services.Api.Client
            {
                ApiKey = ConfigurationManager.AppSettings.Get("ApiKey"),
                AppName = ConfigurationManager.AppSettings.Get("AppName"),
                BaseUrl = ConfigurationManager.AppSettings.Get("BaseUrl")
            };
        }

        public static string RegistrantKey()
        {
            return ConfigurationManager.AppSettings.Get("RegistrantKey");
        }
    }
}