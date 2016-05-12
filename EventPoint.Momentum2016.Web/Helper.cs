using Microsoft.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EventPoint.Samples.Web
{
    public class Helper
    {
        public static async Task<EventPoint.Services.Api.Client> GetApiClient()
        {
            return new EventPoint.Services.Api.Client
            {
                BaseUrl = CloudConfigurationManager.GetSetting("BaseUrl"),
                ApiKey = CloudConfigurationManager.GetSetting("ApiKey"),
                AppName = CloudConfigurationManager.GetSetting("AppName")
            };
        }
    }
}