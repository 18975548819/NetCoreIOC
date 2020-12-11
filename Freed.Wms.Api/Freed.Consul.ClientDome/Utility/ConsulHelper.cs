using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freed.Consul.ClientDome.Utility
{
    /// <summary>
    /// consul工具类
    /// </summary>
    public static class ConsulHelper
    {
        /// <summary>
        /// 获取consul服务  使用consul客户端+配置文件时使用
        /// </summary>
        /// <returns></returns>
        public async static Task<List<string>> GetConsulInfoList()
        {
            //1.0创建连接
            var consulClient = new ConsulClient(config =>
            {
                //1.1 建立客户端和服务端连接
                config.Address = new Uri("http://10.0.40.16:8500");
            });
            //2.0查询服务
            var result = await consulClient.Catalog.Service("Freed");
            //3.0将服务进行拼接
            var list = new List<string>();
            foreach (var catalogService in result.Response)
            {
                //int weig = Convert.ToInt32(catalogService.ServiceTags[0]);
                //1、拼接连接地址
                list.Add(catalogService.ServiceAddress + ":" + catalogService.ServicePort);
            }
            return list;
        }


        /// <summary>
        /// 获取consul注册信息  运行代码注册consul时使用
        /// </summary>
        /// <returns></returns>
        public static string GetConsulInfoList2()
        {
            #region 本地调用-单体架构
            //base.ViewBag.Users = this._iUserService.UserAll();
            #endregion
            //开启进化---当下写的是单体架构---分布式架构--服务实例来提供数据---也看到可用性的重要---怎么去保障可用性

            #region 分布式
            string url = null;
            //代码的一小步，架构的一大步
            //string url = "http://localhost:5726/api/users/all";
            //string url = "http://localhost:5727/api/users/all";
            //string url = "http://localhost:5728/api/users/all";

            #region Nginx
            //url = "http://localhost:8080/api/users/all";//只知道nginx地址
            #endregion

            #region Consul
            //Consul能提供的就只有服务的Ip:Port--DNS
            //url = "http://Freed/v1/api/Account/get_wms_pda_config";
            url = "http://Freed/api/users/all";
            ConsulClient client = new ConsulClient(c =>
            {
                c.Address = new Uri("http://10.0.40.16:8500/");
                c.Datacenter = "dc1";
            });
            var response = client.Agent.Services().Result.Response;
            //foreach (var item in response)
            //{
            //    Console.WriteLine("***************************************");
            //    Console.WriteLine(item.Key);
            //    var service = item.Value;
            //    Console.WriteLine($"{service.Address}--{service.Port}--{service.Service}");
            //    Console.WriteLine("***************************************");
            //}

            Uri uri = new Uri(url);
            string groupName = uri.Host;
            AgentService agentService = null;

            var serviceDictionary = response.Where(s => s.Value.Service.Equals(groupName, StringComparison.OrdinalIgnoreCase)).ToArray();//得到3个实例
            {
                agentService = serviceDictionary[0].Value;
            }
            url = $"{uri.Scheme}://{agentService.Address}:{agentService.Port}{uri.PathAndQuery}";
            #endregion

            string content = ApiHelper.InvokeApi(url);
            //base.ViewBag.Users = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<User>>(content);
            Console.WriteLine($"This is {url} Invoke");
            #endregion

            return content;
        }
    }
}
