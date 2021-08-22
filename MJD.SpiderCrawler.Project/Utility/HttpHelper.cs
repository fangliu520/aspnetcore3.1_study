using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MJD.SpiderCrawler.Project.Utility
{
    public class HttpHelper
    {

        public static string DownLoad(string url, Encoding encode)
        {
            string htmlContent = string.Empty;
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.Timeout = 30 * 1000;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
            request.ContentType = "text/html; charset=utf-8";
            //request.Headers.Add("Cookie", @"pgv_pvid=1893047479; pgv_pvi=2468069376; RK=5MaRVbp2VO; ptcz=db248cfd981880dd37172927f5c526ca861bb6888e1b62c4065ffc0466fe57d7; XWINDEXGREY=0; o_cookie=190931822; pac_uid=1_190931822; iip=0; ts_uid=4599879820; tvfe_boss_uuid=c48f647e17989def; iswebp=1; _pathcode=0.6556344148526858; tdw_auin_data=-; tdw_data_testid=; tdw_data_flowid=; tdw_first_visited=1; pgv_info=ssid=s9405032156; ts_refer=www.baidu.com/link; Hm_lvt_0c196c536f609d373a16d246a117fd44=1627051274,1629040867,1629621404; _qpsvr_localtk=0.800021911995566; tdw_data={"ver4":"www.baidu.com","ver5":"","ver6":"","refer":"www.baidu.com","from_channel":"","path":"d - 0.6556344148526858","auin":" - ","uin":"","real_uin":""}; tdw_data_new_2={"auin":" - ","sourcetype":"","sourcefrom":"","ver9":"","uin":"","visitor_id":"1692484262787879","ver10":"","url_page":"search","url_module":"pc_popout","url_position":""}; tdw_data_sessionid=162962147901232457968374; ts_last=ke.qq.com/course/list/%E6%9C%9D%E5%A4%95%E6%95%99%E8%82%B2; Hm_lpvt_0c196c536f609d373a16d246a117fd44=1629621479");
            // request.Headers.Add("referer", "https://ke.qq.com/course/list/朝夕教育");
            request.CookieContainer = new CookieContainer();//多准备几个cookie
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine($"抓取{url}地址返回失败：{response.StatusCode}");
                }
                else
                {
                    try
                    {
                        //string sessionValue = response.Cookies[""].Value;
                        StreamReader sr = new StreamReader(response.GetResponseStream(), encode);
                        htmlContent = sr.ReadToEnd();
                        sr.Close();


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"抓取{url}地址返回失败：{response.StatusCode}|{ex.StackTrace}|{ex.Message}");
                    }
                }

            }
            return htmlContent;
        }

        #region 请求帮助类方法
        public static string GetUrl(string url)
        {

            using (HttpClient client = new HttpClient())
            {

                HttpRequestMessage message = new HttpRequestMessage();
                message.Method = HttpMethod.Get;
                message.RequestUri = new Uri(url);
                var r = client.SendAsync(message).Result;
                var requrl = r.RequestMessage.RequestUri.AbsoluteUri;

                return requrl;
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        /// <param name="josnContent"></param>
        /// <returns></returns>
        public static HttpResponseMessage HttpRequestJson(string url, HttpMethod httpMethod, string josnContent)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage message = new HttpRequestMessage()
                {
                    Method = httpMethod,
                    RequestUri = new Uri(url),
                    Content = new StringContent(josnContent, Encoding.UTF8, "application/json")
                };
                return httpClient.SendAsync(message).Result;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static HttpResponseMessage HttpRequest(string url, HttpMethod httpMethod, Dictionary<string, string> parameter)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage message = new HttpRequestMessage()
                {
                    Method = httpMethod,
                    RequestUri = new Uri(url)
                };
                if (parameter != null)
                {
                    var encodedContent = new FormUrlEncodedContent(parameter);
                    message.Content = encodedContent;
                }
                return httpClient.SendAsync(message).Result;
            }
        }
        #endregion
    }
}
