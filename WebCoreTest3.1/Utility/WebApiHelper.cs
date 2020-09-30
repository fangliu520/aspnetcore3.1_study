using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace WebCoreTest3._1.Utility
{
    public static class WebApiHelper
    {
        public static string InvokeApi(string url)
        {

            using (HttpClient client = new HttpClient())
            {

                HttpRequestMessage message = new HttpRequestMessage();
                message.Method = HttpMethod.Get;
                message.RequestUri = new Uri(url);
                var r = client.SendAsync(message).Result;
                string content = r.Content.ReadAsStringAsync().Result;
                return content;
            }


        }
    }
}
