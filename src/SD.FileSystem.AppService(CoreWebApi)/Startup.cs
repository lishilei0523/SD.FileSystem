using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SD.IdentitySystem.AspNetCore.Authentication.Filters;
using SD.Infrastructure.AspNetCore.Server.Middlewares;
using SD.Infrastructure.Constants;
using SD.Toolkits.AspNet;
using SD.Toolkits.AspNetCore.Filters;
using SD.Toolkits.OwinCore.Middlewares;
using System;
using System.IO;
using System.Reflection;

namespace SD.FileSystem.AppService
{
    /// <summary>
    /// 应用程序启动器
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            //添加跨域策略
            services.AddCors(options => options.AddPolicy(typeof(Startup).FullName,
                policyBuilder =>
                {
                    policyBuilder.AllowAnyMethod();
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.AllowCredentials();
                    policyBuilder.SetIsOriginAllowed(_ => true);
                }));

            //添加Swagger
            services.AddSwaggerGen(options =>
            {
                OpenApiInfo apiInfo = new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "文件管理系统 WebApi 接口文档"
                };

                string xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFileName);

                options.SwaggerDoc("v1.0", apiInfo);
                options.IncludeXmlComments(xmlFilePath);
            });

            //添加过滤器
            services.AddControllers(options =>
            {
                options.Filters.Add<WebApiAuthenticationFilter>();
                options.Filters.Add<WebApiExceptionFilter>();
            }).AddNewtonsoftJson(options =>
            {
                //Camel命名设置
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                //日期时间格式设置
                IsoDateTimeConverter dateTimeConverter = new IsoDateTimeConverter()
                {
                    DateTimeFormat = CommonConstants.DateTimeFormat
                };
                options.SerializerSettings.Converters.Add(dateTimeConverter);
            });

            //表单设置
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = int.MaxValue;
            });
        }

        /// <summary>
        /// 配置应用程序
        /// </summary>
        public void Configure(IApplicationBuilder appBuilder)
        {
            //配置中间件
            appBuilder.UseMiddleware<GlobalMiddleware>();
            appBuilder.UseMiddleware<CacheOwinContextMiddleware>();

            //配置跨域
            appBuilder.UseCors(typeof(Startup).FullName);

            //配置Swagger
            appBuilder.UseSwagger();
            appBuilder.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1.0/swagger.json", "文件管理系统 WebApi 接口文档 v1.0"));

            //配置路由
            appBuilder.UseRouting();
            appBuilder.UseEndpoints(routeBuilder => routeBuilder.MapControllers());

            //配置服务器
            string staticFilesRoot = Path.Combine(AppContext.BaseDirectory, AspNetSetting.StaticFilesPath);
            string fileServerRoot = Path.Combine(AppContext.BaseDirectory, AspNetSetting.FileServerPath);
            Directory.CreateDirectory(staticFilesRoot);
            Directory.CreateDirectory(fileServerRoot);
            StaticFileOptions staticFileOptions = new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFilesRoot)
            };
            FileServerOptions fileServerOptions = new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(fileServerRoot),
                EnableDirectoryBrowsing = true
            };
            appBuilder.UseStaticFiles(staticFileOptions);
            appBuilder.UseFileServer(fileServerOptions);
        }
    }
}
