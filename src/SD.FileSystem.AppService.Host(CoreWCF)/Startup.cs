using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SD.Common;
using SD.FileSystem.AppService.Implements;
using SD.IdentitySystem.WCF.Authentication;
using SD.Infrastructure.WCF.Server;
using SD.IOC.Integration.WCF.Behaviors;
using SD.Toolkits.AspNet;
using System.Collections.Generic;
using System.Configuration;

namespace SD.FileSystem.AppService.Host
{
    public class Startup
    {
        /// <summary>
        /// ���÷���
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            //���WCF����
            services.AddServiceModelServices();

            //���WCF����
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            services.AddServiceModelConfigurationManagerFile(configuration.FilePath);
        }

        /// <summary>
        /// ����Ӧ�ó���
        /// </summary>
        public void Configure(IApplicationBuilder appBuilder)
        {

            //����WCF����
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
                builder.ConfigureServiceHostBase<LoadContract>(host => host.Description.Behaviors.AddRange(serviceBehaviors));
            });
        }
    }
}
