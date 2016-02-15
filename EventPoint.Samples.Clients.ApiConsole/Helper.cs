using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventPoint.Services.Api.Models;
using System.Threading.Tasks;
using System.Configuration;


namespace EventPoint.Samples.Clients.ApiConsole
{
    public static class Helper
    {
        public static async Task<List<SessionMonitoringRecord>> GetAllSessionMonitoringRecords()
        {
            var client = new EventPoint.Services.Api.Client
            {
                ApiKey = ConfigurationManager.AppSettings.Get("ApiKey"),
                AppName = ConfigurationManager.AppSettings.Get("AppName"),
                BaseUrl = ConfigurationManager.AppSettings.Get("BaseUrl")
            };

            //loop through all timeslots in program and pull attendance for each
            //aggregate and return result
            var timeslots = await client.GetTimeslotsAsync();
            timeslots = timeslots.OrderBy(x => x.Start).ToList();
            var results = new List<SessionMonitoringRecord>();
            foreach (var timeslot in timeslots)
            {
                var records = await client.GetSessionMonitoringRecordsAsync(String.Empty, timeslot.Id, String.Empty);
                if (records != null)
                {
                    results.AddRange(records);
                }
            }

            return results;
        }
    }
}
