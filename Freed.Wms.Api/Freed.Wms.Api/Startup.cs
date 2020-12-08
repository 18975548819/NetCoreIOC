using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DataModel.Authorize;
using DataService.Base;
using Freed.Wms.Api.Filter;
using Freed.Wms.Api.SwaggerHeple;
using Freed.Wms.Api.Utility;
using IBusinessManage.Base;
using IDataService.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

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
            services.AddControllers();
            //ȫ���쳣����
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
                    Description = "WMS���ݿ���ӿ��ĵ�",
                });

                //JWT��֤
                var security = new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ�������� Bearer {Token} ���������֤",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };
                c.AddSecurityDefinition("oauth2", security);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement { { security, new List<string>() } });
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                // ʹ�÷����ȡxml�ļ�����������ļ���·��
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // ����xmlע��. �÷����ڶ����������ÿ�������ע�ͣ�Ĭ��Ϊfalse.
                c.IncludeXmlComments(xmlPath, true);
                c.DocumentFilter<SwaggerDocTag>();
            });
            #endregion

            #region JWT
            var _secoretKey = Configuration["SecoretKey"];
            var _signingKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_secoretKey));
            //��ȡJWT����
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuserOptions));
            services.Configure<JwtIssuserOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuserOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuserOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });
            //JwtBearer��֤:
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

            #region ICOע��
            var builder = new ContainerBuilder();//ʵ���� AutoFac  ����   

            //���ݲ�ע��
            var assemblysSer = Assembly.Load("DataService");//Service�Ǽ̳нӿڵ�ʵ�ַ����������
            var baseTypeSer = typeof(IIDependencyService);//IDependency ��һ���ӿڣ�����Ҫʵ������ע��Ľ�ڶ�Ҫ�̳иýӿڣ�
            builder.RegisterAssemblyTypes(assemblysSer)
             .Where(m => baseTypeSer.IsAssignableFrom(m) && m != baseTypeSer)
             .AsImplementedInterfaces().InstancePerLifetimeScope();

            //ҵ���ע��
            var assemblys = Assembly.Load("BusinessManage");//Service�Ǽ̳нӿڵ�ʵ�ַ����������
            var baseType = typeof(IDependencyManager);//IDependency ��һ���ӿڣ�����Ҫʵ������ע��Ľ�ڶ�Ҫ�̳иýӿڣ�
            builder.RegisterAssemblyTypes(assemblys)
             .Where(m => baseType.IsAssignableFrom(m) && m != baseType)
             .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.Populate(services);
            var ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);//������IOC�ӹ� core����DI����
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISqlConnection connection)
        {
            ISqlConnection connModel = new SqlConnectionModel();
            connModel.CommonConnStr = Configuration["ConnectionStrings:MsSqlPdaConn"];
            connModel.EasOrclConnStr = Configuration["ConnectionStrings:OrclSqlPdaConn"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //���ÿ���
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            //app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Freed_WmsA_Pi");
                c.RoutePrefix = string.Empty; ;
            });

            app.UseRouting();
            //ʹ����֤��Ȩ
            app.UseAuthentication();
            //����JWT��֤
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //��ʼ��
            connection.InitService(connModel);
            #region Consulע�� 
            //վ���������--ִ����ִֻ��һ��
            //this.Configuration.ConsulRegist();
            #endregion
        }
    }
}
