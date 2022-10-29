using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Freed.Wms.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //支持命令行中传参数
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddCommandLine(args)
                .Build();

            //CreateHostBuilder(args).Build().Run();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
            //.UseStartup<Startup>();
            //.UseStartup<Startup>().UseUrls("http://*:5088;https://*:5081");  //代码调试环境使用，项目属性=》调试=》项目浏览器地址、应用URL地址需要改为一致
        .UseStartup<Startup>().UseUrls("http://127.0.0.1:5088");  //注册consul时使用  健康检查
    }
}
