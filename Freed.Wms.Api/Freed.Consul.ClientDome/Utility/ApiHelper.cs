using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Freed.Consul.ClientDome.Utility
{
    public class ApiHelper
    {
        /// <summary>
        /// 后端完成请求调用
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string InvokeApi(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage message = new HttpRequestMessage();
                message.Method = HttpMethod.Get;
                message.RequestUri = new Uri(url);


                var result = httpClient.SendAsync(message).Result;
                string content = result.Content.ReadAsStringAsync().Result;
                return content;
            }
        }
    }
}
