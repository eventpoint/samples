using EventPoint.Services.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EventPoint.Services.Api
{
    public class Client
    {
        public string ApiKey { get; set; }
        public string AppName { get; set; }
        public string BaseUrl { get; set; }
        public bool UseGzipForRequests { get; set; }


        public Client(string apikey = "", string appname = "", string baseurl = "", bool usegzipforrequests = false)
        {
            ApiKey = apikey;
            AppName = appname;
            BaseUrl = baseurl;
            UseGzipForRequests = usegzipforrequests;
        }

        private HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            if (!string.IsNullOrWhiteSpace(ApiKey)) client.DefaultRequestHeaders.Add("apikey", ApiKey);
            if (!string.IsNullOrWhiteSpace(AppName)) client.DefaultRequestHeaders.Add("appname", AppName);

            return client;
        }

        private async Task<string> MakeAsyncWebRequest(string url, Dictionary<string, string> data)
        {
            using (var client = GetHttpClient())
            {
                string responseData = null;
                if (data != null && data.Count > 0)
                {
                    var content = new FormUrlEncodedContent(data);
                    //var response = await client.PostAsync(url, content);
                    var response = client.PostAsync(url, content).Result;
                    var bytes = await response.Content.ReadAsByteArrayAsync();
                    responseData = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                }
                else
                {
                    if (UseGzipForRequests)
                    {
                        client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
                    }

                    byte[] bytes = null;
                    try
                    {
                        //var response = await client.GetAsync(url);
                        var response = client.GetAsync(url).Result;
                        bytes = await response.Content.ReadAsByteArrayAsync();
                        if (UseGzipForRequests)
                        {
                            using (var compressedBytes = new MemoryStream(bytes))
                            {
                                using (var gzip = new GZipStream(compressedBytes, CompressionMode.Decompress))
                                {
                                    using (var uncompressedStream = new MemoryStream())
                                    {
                                        gzip.CopyTo(uncompressedStream);
                                        responseData = System.Text.Encoding.UTF8.GetString(uncompressedStream.ToArray());
                                    }
                                }
                            }
                        }
                        else
                        {
                            responseData = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                        }
                    }
                    catch (Exception ex1)
                    {
                        var m = ex1;
                        responseData = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                    }
                }
                return responseData;
            }
        }

        public async Task<Category> GetCategoryAsync(string category)
        {
            var res = await MakeAsyncWebRequest(String.Format("Program/Category?find={0}", category), null);
            return FromJson<Category>(res);
        }

        public async Task<List<Category>> GetCategories()
        {
            var res = await MakeAsyncWebRequest("Program/Categories?parent=", null);
            return FromJson<List<Category>>(res);
        }

        public async Task<List<Category>> GetChildCategoriesAsync(string parent)
        {
            var res = await MakeAsyncWebRequest(String.Format("Program/Categories?parent={0}", parent), null);
            return FromJson<List<Category>>(res);
        }

        public async Task<List<Registrant>> GetRegistrantsAsync()
        {
            var res = await MakeAsyncWebRequest("Program/Registrants", null);
            return FromJson<List<Registrant>>(res);
        }

        public async Task<IntegratedSurveyModel> GetEvalForSessionAsync(string registrantKey, string sessionId)
        {
            var res = await MakeAsyncWebRequest(String.Format("Eval/Get-For-Session?sessionid={0}&registrantkey={1}", sessionId, registrantKey), null);
            return FromJson<IntegratedSurveyModel>(res);
        }

        public async Task<List<RegistrantEvalResult>> GetEvalResultsAsync()
        {
            var res = await MakeAsyncWebRequest("Eval/Results", null);
            return FromJson<List<RegistrantEvalResult>>(res);
        }

        public async Task<List<SessionMonitoringRecord>> GetSessionMonitoringRecordsAsync(string topicid = "", string timeslotid = "", string roomid = "")
        {
            var res = await MakeAsyncWebRequest(String.Format("SessionMonitor/Session-Scans?topicid={0}&timeslotid={1}&roomid={2}", topicid, timeslotid, roomid), null);
            return FromJson<List<SessionMonitoringRecord>>(res);
        }

        public async Task<List<Speaker>> GetSpeakersAsync()
        {
            var res = await MakeAsyncWebRequest("Program/Speakers", null);
            return FromJson<List<Speaker>>(res);
        }

        public async Task<List<Survey>> GetSurveysAsync()
        {
            var res = await MakeAsyncWebRequest("Program/Surveys", null);
            return FromJson<List<Survey>>(res);
        }

        public async Task<List<Timeslot>> GetTimeslotsAsync()
        {
            var res = await MakeAsyncWebRequest("Program/Timeslots", null);
            return FromJson<List<Timeslot>>(res);
        }

        public async Task<List<Topic>> GetTopicsAsync()
        {
            var res = await MakeAsyncWebRequest("Program/Topics", null);
            return FromJson<List<Topic>>(res);
        }

        public T FromJson<T>(string res)
        {
            if (!String.IsNullOrEmpty(res))
            {
                try
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(res);
                }
                catch { }
            }
            return default(T);
        }

        public string ToJson(object value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }
    }
}
