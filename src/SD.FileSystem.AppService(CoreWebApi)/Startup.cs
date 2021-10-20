using Microsoft.AspNetCore.Builder;
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
    /// Ӧ�ó���������
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ���÷���
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            //��ӿ������
            services.AddCors(options => options.AddPolicy(typeof(Startup).FullName,
                policyBuilder =>
                {
                    policyBuilder.AllowAnyMethod();
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.AllowCredentials();
                    policyBuilder.SetIsOriginAllowed(_ => true);
                }));

            //���Swagger
            services.AddSwaggerGen(options =>
            {
                OpenApiInfo apiInfo = new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "�ļ�����ϵͳ WebApi �ӿ��ĵ�"
                };

                string xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFileName);

                options.SwaggerDoc("v1.0", apiInfo);
                options.IncludeXmlComments(xmlFilePath);
            });

            //��ӹ�����
            services.AddControllers(options =>
            {
                options.Filters.Add<WebApiAuthenticationFilter>();
                options.Filters.Add<WebApiExceptionFilter>();
            }).AddNewtonsoftJson(options =>
            {
                //Camel��������
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                //����ʱ���ʽ����
                IsoDateTimeConverter dateTimeConverter = new IsoDateTimeConverter()
                {
                    DateTimeFormat = CommonConstants.TimeFormat
                };
                options.SerializerSettings.Converters.Add(dateTimeConverter);
            });
        }

        /// <summary>
        /// ����Ӧ�ó���
        /// </summary>
        public void Configure(IApplicationBuilder appBuilder)
        {
            //�����м��
            appBuilder.UseMiddleware<GlobalMiddleware>();
            appBuilder.UseMiddleware<CacheOwinContextMiddleware>();

            //���ÿ���
            appBuilder.UseCors(typeof(Startup).FullName);

            //����Swagger
            appBuilder.UseSwagger();
            appBuilder.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1.0/swagger.json", "�ļ�����ϵͳ WebApi �ӿ��ĵ� v1.0"));

            //����·��
            appBuilder.UseRouting();
            appBuilder.UseEndpoints(routeBuilder => routeBuilder.MapControllers());

            //���÷�����
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
