using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace BOT_IT_V1.Utils
{
    internal class HttpUtils
    {
        const string url = @"http://api-bot88.winddevteam.tk/";

        public static BaseResult GetRequest(string path)
        {
            try
            {
                HttpClient client = new HttpClient();
                string data = client.GetStringAsync(url + path).Result;
                return new BaseResult(true, data);
            }
            catch (Exception ex)
            {
                return new BaseResult(false, ex.Message);
            }
        }

        public static BaseResult PostRequest(string path)
        {
            try
            {
                HttpWebRequest httpWeb = (HttpWebRequest)WebRequest.Create(url + path);
                httpWeb.Method = "POST";
                using (StreamReader reader = new StreamReader(httpWeb.GetResponse().GetResponseStream()))
                {
                    return new BaseResult(true, "");
                }
            }
            catch (WebException ex)
            {
                using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
                {
                    string data = reader.ReadToEnd();
                    JObject botJsonObj = JObject.Parse(data);
                    data = (botJsonObj["message"] ?? "").ToString();
                    return new BaseResult(false, data);
                }
            }
        }
    }

    public class BaseResult
    {
        public BaseResult(bool success, string text)
        {
            this.success = success;
            message = success ? "" : text;
            data = success ? text : "";
        }
        public bool success { set; get; }
        public string message { set; get; }
        public string data { set; get; }
    }
}
