using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using EventPoint.Services.Api.Models;

namespace EventPoint.Samples.Clients.ApiConsole
{
    class Program
    {

        private static string FileData = "C:\\staging\\EventPointData\\";

        static void Main(string[] args)
        {
            Init();
            Console.Read();
        }

        private static async Task Init()
        {
            Directory.CreateDirectory(FileData);

            //retrieves a whole bunch of data from the eventpoint api and saves it locally as .json
            //await GetRawData();

            //await GetEvalForSession("8d8ea6ef-52c5-e511-ab0e-00155d5066d7");

            //shows joining sessions to session types
            //await Sample01(true);

            //retrieves registrant responses to the overall satisfaction question from the overall eval
            //await Sample02("Eval_Overall", 10, true);

        }

        private static async Task GetEvalForSession(string sessionid)
        {
            Console.WriteLine("retrieving eval for sessionid {0}...", sessionid);

            //gets the Api.Client 
            var client = new EventPoint.Services.Api.Client
            {
                ApiKey = ConfigurationManager.AppSettings.Get("ApiKey"),
                AppName = ConfigurationManager.AppSettings.Get("AppName"),
                BaseUrl = ConfigurationManager.AppSettings.Get("BaseUrl")
            };

            var registrantkey = ConfigurationManager.AppSettings.Get("RegistrantKey");
            var result = await client.GetEvalForSessionAsync(registrantkey, sessionid);
            File.WriteAllText(String.Format("{0}{1}", FileData, String.Format("eval_for_session_{0}.json", sessionid)), JsonConvert.SerializeObject(result));
            Console.WriteLine("done");
        }

        private static async Task GetRawData()
        {
            //set a value to true in order to retrieve and save that data type
            bool attendance = false;
            bool categories = false;
            bool evals = false;
            bool registrants = false;
            bool speakers = false;
            bool surveys = true;
            bool topics = true;

            //gets the Api.Client 
            var client = new EventPoint.Services.Api.Client
            {
                ApiKey = ConfigurationManager.AppSettings.Get("ApiKey"),
                AppName = ConfigurationManager.AppSettings.Get("AppName"),
                BaseUrl = ConfigurationManager.AppSettings.Get("BaseUrl")
            };


            if (attendance)
            {
                Console.WriteLine("retrieving attendance...");
                var result = await Helper.GetAllSessionMonitoringRecords();
                File.WriteAllText(String.Format("{0}{1}", FileData, "attendance.json"), JsonConvert.SerializeObject(result));
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

        private static async Task Sample01(bool savedata = false)
        {
            var topics = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<Topic>>(File.ReadAllText(String.Format("{0}{1}", FileData, "topics.json"))));
            var categories = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<Category>>(File.ReadAllText(String.Format("{0}{1}", FileData, "categories.json"))));

            string parentId = categories.First(x => x.Name == "Session Type").Id;
            var stypes = categories.Where(x => x.ParentId == parentId).ToList();

            var query =
                from topic in topics
                from stype in stypes
                where topic.CategoryIds.Any(c => c.Contains(stype.Id))
                select new
                {
                    Code = topic.Code,
                    Title = topic.Title,
                    SessionType = stype.Name
                };

            foreach (var item in query)
            {
                Console.WriteLine("{0}, {1}", item.SessionType, item.Code);
            }

            if (savedata)
            {
                File.WriteAllText(String.Format("{0}{1}", FileData, "sample01.json"), JsonConvert.SerializeObject(query));
            }

        }

        private static async Task Sample02(string surveyCode, int questionNumber, bool savedata = false)
        {
            var registrants = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<Registrant>>(File.ReadAllText(String.Format("{0}{1}", FileData, "registrants.json"))));
            var surveys = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<Survey>>(File.ReadAllText(String.Format("{0}{1}", FileData, "surveys.json"))));
            var evalresults = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<RegistrantEvalResult>>(File.ReadAllText(String.Format("{0}{1}", FileData, "evalresults.json"))));

            var questionText = surveys.First(x => x.Code == surveyCode).Questions.First(x => x.Num == questionNumber).Text;
            Console.WriteLine("{0}", questionText);
            await Task.Delay(2500);

            var query = from question in surveys.First(x => x.Code == surveyCode).Questions.Where(x => x.Num == questionNumber)
                        join evalresult in evalresults on question.Id equals evalresult.QuestionId
                        join registrant in registrants on evalresult.RegistrantKey equals registrant.Key
                        select new
                        {
                            Registrant = registrant.Email,
                            Answer = evalresult.Answer
                        };

            foreach (var item in query)
            {
                Console.WriteLine("{0}: {1}", item.Registrant, item.Answer);
            }

            if (savedata)
            {
                File.WriteAllText(String.Format("{0}{1}", FileData, "sample02.json"), JsonConvert.SerializeObject(query));
            }

        }

    }
}
