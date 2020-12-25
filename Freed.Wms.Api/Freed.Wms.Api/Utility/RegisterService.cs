using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace Freed.Wms.Api.Utility
{
    public static class RegisterService
    {
        const string _consulIP = "localhost";
        const int _consulPort = 8500;
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IHostApplicationLifetime lifetime, IWebHostEnvironment env, IConfiguration configuration)
        {
            var server = configuration["urls"];
            if (server.Contains("/") && server.Contains(":"))
            {
                var str = server.Split('/').LastOrDefault().Split(':');
                string _ip = "localhost";
                int _port = Convert.ToInt32("8011");
                try
                {
                    if (_port > 0)
                    {
                        var consulClient = new ConsulClient(x => x.Address = new Uri($"http://{_consulIP}:{_consulPort}"));//请求注册的 Consul 地址
                        var httpCheck = new AgentServiceCheck()
                        {
                            DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(1),//服务启动多久后注册
                            Interval = TimeSpan.FromSeconds(1),//健康检查时间间隔，或者称为心跳间隔
                            HTTP = $"http://{_ip}:{_port}/api/health",//健康检查地址
                            Timeout = TimeSpan.FromSeconds(5)
                        };
                        var registration = new AgentServiceRegistration()
                        {
                            Checks = new[] { httpCheck },
                            ID = Guid.NewGuid().ToString(),
                            Name = env.ApplicationName,
                            Address = _ip,
                            Port = _port,
                            Tags = new[] { $"urlprefix-/{env.ApplicationName}" }//添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
                        };
                        consulClient.Agent.ServiceRegister(registration).Wait();//服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）
                        lifetime.ApplicationStopping.Register(() =>
                        {
                            consulClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
                        });
                        return app;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"注册服务器{_consulIP}:{_consulPort}：", ex);
                }
            }
            throw new Exception("服务（注册/发现）获取Host失败");
        }
    }
}
