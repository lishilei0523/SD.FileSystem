using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SD.Toolkits.AspNet;

namespace SD.FileSystem.WebClient
{
    class Program
    {
        static void Main()
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder();

            //WebHost配置
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel(options =>
                {
                    foreach (int httpPort in AspNetSetting.HttpPorts)
                    {
                        options.ListenAnyIP(httpPort);
                    }
                });

                webBuilder.UseWebRoot(AspNetSetting.StaticFilesPath);
                webBuilder.UseStartup<Startup>();
            });

            IHost host = hostBuilder.Build();
            host.Run();
        }
    }
}
