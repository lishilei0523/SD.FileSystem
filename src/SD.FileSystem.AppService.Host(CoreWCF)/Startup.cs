using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using SD.Common;
using SD.FileSystem.AppService.Implements;
using SD.IdentitySystem.WCF.Authentication;
using SD.Infrastructure.WCF.Server;
using SD.IOC.Integration.WCF.Behaviors;
using SD.Toolkits.AspNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace SD.FileSystem.AppService.Host
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
            //添加WCF服务
            services.AddServiceModelServices();

            //添加WCF配置
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            services.AddServiceModelConfigurationManagerFile(configuration.FilePath);
        }

        /// <summary>
        /// 配置应用程序
        /// </summary>
        public void Configure(IApplicationBuilder appBuilder)
        {
            //配置WCF服务
            DependencyInjectionBehavior dependencyInjectionBehavior = new DependencyInjectionBehavior();
            InitializationBehavior initializationBehavior = new InitializationBehavior();
            IList<IServiceBehavior> serviceBehaviors = new List<IServiceBehavior>
            {
                dependencyInjectionBehavior, initializationBehavior
            };

            if (AspNetSetting.Authorized)
            {
                AuthenticationBehavior authenticationBehavior = new AuthenticationBehavior();
                serviceBehaviors.Add(authenticationBehavior);
            }

            appBuilder.UseServiceModel(builder =>
            {
                builder.ConfigureServiceHostBase<FileContract>(host => host.Description.Behaviors.AddRange(serviceBehaviors));
                builder.ConfigureServiceHostBase<LoadContract>(host => host.Description.Behaviors.AddRange(serviceBehaviors));
            });

            //配置文件服务器
            string fileServerPath = Path.IsPathRooted(AspNetSetting.FileServerPath)
                ? AspNetSetting.FileServerPath
                : Path.Combine(AppContext.BaseDirectory, AspNetSetting.FileServerPath);
            Directory.CreateDirectory(fileServerPath);
            FileServerOptions fileServerOptions = new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(fileServerPath),
                EnableDirectoryBrowsing = true
            };
            appBuilder.UseFileServer(fileServerOptions);
        }
    }
}
