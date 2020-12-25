using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Headers.Middleware;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Middleware.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ocelot.Requester.Middleware;
using Microsoft.Net.Http.Headers;
using Ocelot.Errors.Middleware;
using Ocelot.Responder.Middleware;
using Ocelot.DownstreamRouteFinder.Middleware;
using Ocelot.Request.Middleware;
using Ocelot.RequestId.Middleware;
using Ocelot.LoadBalancer.Middleware;
using Ocelot.DownstreamUrlCreator.Middleware;
using Ocelot.Cache.Middleware;

namespace Freed.Gateway.Dome2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOcelot()
                .AddConsul();

            //services.AddOcelot(
            //     new ConfigurationBuilder()
            //     .AddJsonFile("configuration.json", optional: false, reloadOnChange: true).Build())
            //     .AddConsul()
            //     .AddConfigStoredInConsul();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseOcelot().Wait();

            #region 暂未启用
            //var configuration = new OcelotPipelineConfiguration
            //{
            //    PreErrorResponderMiddleware = async (ctx, next) =>
            //    {
            //        await next.Invoke();
            //    }
            //};
            //app.UseOcelot(configuration);

            //Ocelot跨域配置  测试未成功
            //app.UseOcelot((ocelotBuilder, pipelineConfiguration) =>
            //{
            //    // This is registered to catch any global exceptions that are not handled
            //    // It also sets the Request Id if anything is set globally
            //    ocelotBuilder.UseExceptionHandlerMiddleware();
            //    // Allow the user to respond with absolutely anything they want.
            //    if (pipelineConfiguration.PreErrorResponderMiddleware != null)
            //    {
            //        ocelotBuilder.Use(pipelineConfiguration.PreErrorResponderMiddleware);
            //    }
            //    // This is registered first so it can catch any errors and issue an appropriate response
            //    ocelotBuilder.UseResponderMiddleware();
            //    ocelotBuilder.UseDownstreamRouteFinderMiddleware();
            //    ocelotBuilder.UseDownstreamRequestInitialiser();
            //    ocelotBuilder.UseRequestIdMiddleware();
            //    ocelotBuilder.UseMiddleware<ClaimsToHeadersMiddleware>();
            //    ocelotBuilder.UseLoadBalancingMiddleware();
            //    ocelotBuilder.UseDownstreamUrlCreatorMiddleware();
            //    ocelotBuilder.UseOutputCacheMiddleware();
            //    ocelotBuilder.UseMiddleware<HttpRequesterMiddleware>();
            //    // cors headers
            //    ocelotBuilder.Use(async (context, next) =>
            //    {
            //        if (!context.DownstreamResponse.Headers.Exists(h => h.Key == HeaderNames.AccessControlAllowOrigin))
            //        {
            //            //var allowedOrigins = Configuration.GetAppSetting("AllowedOrigins")
            //            //.SplitArray<string>();
            //            //var allowedOrigins = Configuration["AllowedOrigins"];

            //            var allowedOrigins = Configuration["AllowedOrigins"];
            //            //context.DownstreamResponse.Headers.Add(new Header(HeaderNames.AccessControlAllowOrigin, allowedOrigins.Length == 0 ? new[] { "*" } : allowedOrigins));
            //            context.DownstreamResponse.Headers.Add(new Header(HeaderNames.AccessControlAllowOrigin, new[] { "*" }));
            //            context.DownstreamResponse.Headers.Add(new Header(HeaderNames.AccessControlAllowHeaders, new[] { "*" }));
            //            context.DownstreamResponse.Headers.Add(new Header(HeaderNames.AccessControlRequestMethod, new[] { "*" }));
            //        }
            //        await next();
            //    });
            //}).Wait();



            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //app.UseOcelot().Wait();//设置所有的Ocelot中间件
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseHttpsRedirection();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
            #endregion
        }
    }
}
