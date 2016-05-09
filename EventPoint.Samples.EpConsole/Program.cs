using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace EventPoint.Samples.EpConsole
{
    class Program
    {

        private static string FileData = String.Format("C:\\staging\\{0}\\", ConfigurationManager.AppSettings.Get("ProgramCode"));

        static void Main(string[] args)
        {
            Init().Wait();
            Console.Read();
        }

        private static async Task Init()
        {
            Directory.CreateDirectory(FileData);
            //retrieves a whole bunch of data from the eventpoint api and saves it locally as .json
            await GetRawData();
        }
        
        private static async Task GetRawData()
        {
            //set a value to true in order to retrieve and save that data type
            bool attendance = false;
            bool categories = true;
            bool evals = false;
            bool registrants = false;
            bool speakers = true;
            bool surveys = false;
            bool topics = true;

            //gets the Api.Client 
            var client = new EventPoint.Services.Api.Client
            {
                BaseUrl = ConfigurationManager.AppSettings.Get("BaseUrl"),
                ApiKey = ConfigurationManager.AppSettings.Get("ApiKey"),
                AppName = ConfigurationManager.AppSettings.Get("AppName")
            };

            if (attendance)
            {
                Console.WriteLine("retrieving attendance...");
                var timeslots = await client.GetTimeslotsAsync();
                timeslots = timeslots.OrderBy(x => x.Start).ToList();
                var results = new List<EventPoint.Services.Api.Models.SessionMonitoringRecord>();
                foreach (var timeslot in timeslots)
                {
                    var records = await client.GetSessionMonitoringRecordsAsync(String.Empty, timeslot.Id, String.Empty);
                    if (records != null)
                    {
                        results.AddRange(records);
                    }
                }
                File.WriteAllText(String.Format("{0}{1}", FileData, "attendance.json"), JsonConvert.SerializeObject(results));
            }

            if (categories)
            {
                Console.WriteLine("retrieving categories...");
                var result = await client.GetCategories();
                File.WriteAllText(String.Format("{0}{1}", FileData, "categories.json"), JsonConvert.SerializeObject(result));
            }

            if (registrants)
            {
                Console.WriteLine("retrieving registrants...");
                var result = await client.GetRegistrantsAsync();
                File.WriteAllText(String.Format("{0}{1}", FileData, "registrants.json"), JsonConvert.SerializeObject(result));
            }

            if (speakers)
            {
                Console.WriteLine("retrieving speakers...");
                var result = await client.GetSpeakersAsync();
                File.WriteAllText(String.Format("{0}{1}", FileData, "speakers.json"), JsonConvert.SerializeObject(result));
            }

            if (surveys)
            {
                Console.WriteLine("retrieving surveys...");
                var result = await client.GetSurveysAsync();
                File.WriteAllText(String.Format("{0}{1}", FileData, "surveys.json"), JsonConvert.SerializeObject(result));
            }

            if (topics)
            {
                Console.WriteLine("retrieving topics...");
                var result = await client.GetTopicsAsync();
                File.WriteAllText(String.Format("{0}{1}", FileData, "topics.json"), JsonConvert.SerializeObject(result));
            }

            if (evals)
            {
                Console.WriteLine("retrieving eval results...");
                var result = await client.GetEvalResultsAsync();
                File.WriteAllText(String.Format("{0}{1}", FileData, "evalresults.json"), JsonConvert.SerializeObject(result));
            }
            Console.WriteLine("done");
        }               
    }
}
