using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json.Serialization;
using Owin;
using SD.IdentitySystem.WebApi.Authentication.Filters;
using SD.Infrastructure.WebApi.SelfHost.Server.Middlewares;
using SD.IOC.Integration.WebApi.SelfHost;
using SD.Toolkits.AspNet;
using SD.Toolkits.Owin.Middlewares;
using SD.Toolkits.WebApi.Extensions;
using SD.Toolkits.WebApi.Filters;
using Swashbuckle.Application;
using System;
using System.IO;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SD.FileSystem.AppService.Host
{
    /// <summary>
    /// OWIN启动器
    /// </summary>
    public class Startup : StartupBase
    {
        /// <summary>
        /// 配置应用程序
        /// </summary>
        /// <param name="appBuilder">应用程序建造者</param>
        /// <param name="httpConfiguration">Http配置</param>
        protected override void Configuration(IAppBuilder appBuilder, HttpConfiguration httpConfiguration)
        {
            //配置中间件
            appBuilder.Use<GlobalMiddleware>();
            appBuilder.Use<CacheOwinContextMiddleware>();

            //配置Swagger
            httpConfiguration.EnableSwagger(config =>
            {
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
                config.SingleApiVersion("v1.0", "文件管理系统 WebApi 接口文档");
            }).EnableSwaggerUi();

            //配置路由
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Routes.MapHttpRoute(
                "DefaultApi",
                "Api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional }
            );

            //注册参数绑定
            httpConfiguration.RegisterWrapParameterBindingRule();
            httpConfiguration.RegisterFileParameterBindingRule();

            //允许跨域
            httpConfiguration.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            //添加过滤器
            httpConfiguration.Filters.Add(new WebApiAuthenticationFilter());
            httpConfiguration.Filters.Add(new WebApiExceptionFilter());

            //返回值驼峰命名序列化
            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //配置文件服务器
            string staticFilesPath = Path.IsPathRooted(AspNetSetting.StaticFilesPath)
                ? AspNetSetting.StaticFilesPath
                : Path.Combine(AppContext.BaseDirectory, AspNetSetting.StaticFilesPath);
            string fileServerPath = Path.IsPathRooted(AspNetSetting.FileServerPath)
                ? AspNetSetting.FileServerPath
                : Path.Combine(AppContext.BaseDirectory, AspNetSetting.FileServerPath);
            Directory.CreateDirectory(staticFilesPath);
            Directory.CreateDirectory(fileServerPath);
            StaticFileOptions staticFileOptions = new StaticFileOptions
            {
                FileSystem = new PhysicalFileSystem(staticFilesPath)
            };
            FileServerOptions fileServerOptions = new FileServerOptions
            {
                FileSystem = new PhysicalFileSystem(fileServerPath),
                EnableDirectoryBrowsing = true
            };
            appBuilder.UseStaticFiles(staticFileOptions);
            appBuilder.UseFileServer(fileServerOptions);
        }
    }
}
