using FirebirdSql.Data.FirebirdClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using SD.IdentitySystem.WebApiCore.Authentication.Filters;
using SD.Infrastructure.AspNetCore.Server.Middlewares;
using SD.Toolkits.AspNet;
using SD.Toolkits.OwinCore.Middlewares;
using SD.Toolkits.WebApiCore.Filters;
using System;
using System.Data.Common;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

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
                    DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
                };
                options.SerializerSettings.Converters.Add(dateTimeConverter);
            });

            //ע��ADO.NET Provider
            DbProviderFactories.RegisterFactory("FirebirdSql.Data.FirebirdClient", FirebirdClientFactory.Instance);
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
            string staticFilesRoot = Path.Combine(AppContext.BaseDirectory, AspNetSection.Setting.StaticFiles.Value);
            string fileServerRoot = Path.Combine(AppContext.BaseDirectory, AspNetSection.Setting.FileServer.Value);
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
