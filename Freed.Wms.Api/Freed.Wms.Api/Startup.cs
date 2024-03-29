using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Coravel;
using DataModel.Authorize;
using DataService.Base;
using Freed.CacheFactory.Unility;
using Freed.Schedule;
using Freed.Wms.Api.Filter;
using Freed.Wms.Api.Models;
using Freed.Wms.Api.SwaggerHeple;
using Freed.Wms.Api.Utility;
using IBusinessManage.Base;
using IDataService.Base;
using LogDashboard;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Log4Net.AspNetCore.Entities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using WatchDog;

namespace Freed.Wms.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //启用缓存（MemoryCache 第三方）
            {
                services.AddMemoryCache(options =>
                {
                    options.Clock = new LocalClock();
                    //options.SizeLimit = 1000;  //指定缓存大小
                });
            }

            services.AddSingleton<ICustomMemoryCache, CustomMemoryCache>();  //注册自定义缓存
            services.AddSingleton<FristSchedule>();

            // Coravel任务调度
            services.AddScheduler();

            // LogDashboard日志看板
            services.AddLogDashboard();

            //watchDog中间件
            services.AddWatchDogServices();

            services.AddSingleton(Configuration.GetSection("Consul").Get<ConsulOption>());  //获取consul注册所需参数
            services.AddControllers();
            //全局异常捕获
            services.AddControllers(
                op => { 
                    op.Filters.Add(typeof(CustomExceptionFilterAttribute));
                });

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Freed_Wms_APi",
                    Description = "WMS数据看板接口文档",
                });

                //JWT验证
                var security = new OpenApiSecurityScheme
                {
                    Description = "JWT授权，请输入 Bearer {Token} 进行身份验证",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };
                c.AddSecurityDefinition("oauth2", security);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement { { security, new List<string>() } });
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                // 使用反射获取xml文件。并构造出文件的路径
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // 启用xml注释. 该方法第二个参数启用控制器的注释，默认为false.
                c.IncludeXmlComments(xmlPath, true);
                c.DocumentFilter<SwaggerDocTag>();
            });
            #endregion

            #region JWT
            var _secoretKey = Configuration["SecoretKey"];
            var _signingKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_secoretKey));
            //读取JWT配置
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuserOptions));
            services.Configure<JwtIssuserOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuserOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuserOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });
            //JwtBearer验证:
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuserOptions.Issuer)];
                configureOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuserOptions.Issuer)],
                    ValidateAudience = true,
                    ValidAudience = jwtAppSettingOptions[nameof(JwtIssuserOptions.Audience)],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _signingKey,
                    RequireExpirationTime = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                configureOptions.SaveToken = true;
            });
            #endregion

            #region ICO注册
            var builder = new ContainerBuilder();//实例化 AutoFac  容器   

            //数据层注册
            var assemblysSer = Assembly.Load("DataService");//Service是继承接口的实现方法类库名称
            var baseTypeSer = typeof(IIDependencyService);//IDependency 是一个接口（所有要实现依赖注入的借口都要继承该接口）
            builder.RegisterAssemblyTypes(assemblysSer)
             .Where(m => baseTypeSer.IsAssignableFrom(m) && m != baseTypeSer)
             .AsImplementedInterfaces().InstancePerLifetimeScope();

            //业务层注册
            var assemblys = Assembly.Load("BusinessManage");//Service是继承接口的实现方法类库名称
            var baseType = typeof(IDependencyManager);//IDependency 是一个接口（所有要实现依赖注入的借口都要继承该接口）
            builder.RegisterAssemblyTypes(assemblys)
             .Where(m => baseType.IsAssignableFrom(m) && m != baseType)
             .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.Populate(services);
            var ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISqlConnection connection,ILoggerFactory loggerFactory)
        {
            ISqlConnection connModel = new SqlConnectionModel();
            connModel.CommonConnStr = Configuration["ConnectionStrings:MsSqlPdaConn"];
            connModel.EasOrclConnStr = Configuration["ConnectionStrings:OrclSqlPdaConn"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //启用跨域
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            //app.UseHttpsRedirection();

            // 执行定时任务调度
            //app.ApplicationServices.UseScheduler(scheduler =>
            //{
            //    scheduler.Schedule<FristSchedule>().EverySecond();//每秒执行一次
            //});

            // 日志
            loggerFactory.AddLog4Net(new Log4NetProviderOptions
            {
                PropertyOverrides = new List<NodeInfo> { new NodeInfo { XPath = "/log4net/appender/file[last()]", Attributes = new Dictionary<string, string> { { "value", $"{AppContext.BaseDirectory}LogFiles/" } } } }
            });

            // 启动LogDashboard日志看板
            app.UseLogDashboard();

            //配置watchDog中间件
            //配置中间件
            app.UseWatchDog(opt =>
            {
                opt.WatchPageUsername = "admin";
                opt.WatchPagePassword = "123456";
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Freed_WmsA_Pi");
                c.RoutePrefix = string.Empty; ;
            });

            app.UseRouting();
            //使用认证授权
            app.UseAuthentication();
            //启用JWT验证
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //初始化
            connection.InitService(connModel);

            #region Consul注册 
            //站点启动完成--执行且只执行一次
            this.Configuration.ConsulRegist();
            #endregion
        }
    }
}
