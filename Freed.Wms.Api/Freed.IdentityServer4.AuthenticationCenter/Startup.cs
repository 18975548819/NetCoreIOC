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

            //�ͻ���ģʽ--��ôִ��Ids4
            services.AddIdentityServer()//��ô����
              .AddDeveloperSigningCredential()//��ʱ���ɵ�֤��--��ʱ���ɵ�
              .AddInMemoryClients(ClientInitConfig.GetClients())//InMemory �ڴ�ģʽ
              .AddInMemoryApiResources(ClientInitConfig.GetApiResources());//�ܷ���ɶ��Դ
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();//ʹ��Ids4 ����ᴫ��Ids4���м��

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
