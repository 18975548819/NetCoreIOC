using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freed.Wms.Api.Utility
{
    /// <summary>
    /// Consul注册
    /// </summary>
    public static class ConsulHelper
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="configuration"></param>
        public static void ConsulRegist(this IConfiguration configuration)
        {
            try
            {
                string ip = configuration["ip"];
                string port = configuration["port"];
                string weight = configuration["weight"];
                string consulAddress = configuration["ConsulAddress"];
                string consulCenter = configuration["ConsulCenter"];

                ConsulClient client = new ConsulClient(c =>
                {
                    c.Address = new Uri(consulAddress);
                    c.Datacenter = consulCenter;
                });

                client.Agent.ServiceRegister(new AgentServiceRegistration()
                {
                    //ID = "service " + ip + ":" + port,//Ray--唯一的
                    ID = Guid.NewGuid().ToString(),
                    Name = "Freed",//分组
                    Address = ip,
                    Port = int.Parse(port),
                    Tags = new string[] { weight.ToString() },
                    Check = new AgentServiceCheck()  //健康检查
                    {
                        Interval = TimeSpan.FromSeconds(12),  //间隔多久一次
                        HTTP = $"http://10.19.87.203:8011/api/Health/Index",  //心跳检查：代码调试可用，如果是正式环境需要在启动consul客户端时配置注册文件
                        Timeout = TimeSpan.FromSeconds(5),  //多久检查一次
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(60)  //失败多久移除
                    }
                }); ;
                Console.WriteLine($"{ip}:{port}--weight:{weight}"); //命令行参数获取
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Consul注册：{ex.Message}");
            }
        }
    }
}
