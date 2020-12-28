using Freed.IdentityServer4.AuthenticationCenter.Unittiy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freed.IdentityServer4.AuthenticationCenter
{
    public class Startup
    {
        // dotnet Freed.IdentityServer4.AuthenticationCenter.dll --urls="http://10.19.87.203:6299" --ip="10.19.87.203" --port="6299"
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //客户端模式--怎么执行Ids4
            services.AddIdentityServer()//怎么处理
              .AddDeveloperSigningCredential()//临时生成的证书--即时生成的
              .AddInMemoryClients(ClientInitConfig.GetClients())//InMemory 内存模式
              .AddInMemoryApiResources(ClientInitConfig.GetApiResources());//能访问啥资源
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();//使用Ids4 请求会传过Ids4的中间件

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
