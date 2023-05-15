﻿using CoreWCF.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SD.Toolkits.AspNet;

namespace SD.FileSystem.AppService.Host
{
    class Program
    {
        static void Main()
        {
            IHostBuilder hostBuilder = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder();

            //WebHost配置
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel(options =>
                {
                    foreach (int httpPort in AspNetSetting.HttpPorts)
                    {
                        options.ListenAnyIP(httpPort);
                    }

                    options.Limits.MaxRequestLineSize = short.MaxValue;
                    options.Limits.MaxRequestBodySize = int.MaxValue;
                    options.Limits.MaxRequestBufferSize = int.MaxValue;
                    options.Limits.MaxResponseBufferSize = int.MaxValue;
                });
                foreach (int netTcpPort in AspNetSetting.NetTcpPorts)
                {
                    webBuilder.UseNetTcp(netTcpPort);
                }
                webBuilder.UseStartup<Startup>();
            });

            //依赖注入配置
            ServiceLocator serviceLocator = new ServiceLocator();
            hostBuilder.UseServiceProviderFactory(serviceLocator);

            IHost host = hostBuilder.Build();
            host.Run();
        }
    }
}
