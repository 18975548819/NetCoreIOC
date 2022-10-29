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
            //֧���������д�����
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
            //.UseStartup<Startup>().UseUrls("http://*:5088;https://*:5081");  //������Ի���ʹ�ã���Ŀ����=������=����Ŀ�������ַ��Ӧ��URL��ַ��Ҫ��Ϊһ��
        .UseStartup<Startup>().UseUrls("http://127.0.0.1:5088");  //ע��consulʱʹ��  �������
    }
}
