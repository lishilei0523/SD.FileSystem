using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SD.Toolkits.AspNet;

namespace SD.FileSystem.AppService.Host
{
    public class Program
    {
        public static void Main()
        {
            IHostBuilder hostBuilder = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder();

            //WebHost≈‰÷√
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel(options =>
                {
                    foreach (int httpPort in AspNetSetting.HttpPorts)
                    {
                        options.ListenAnyIP(httpPort);
                    }

                    options.Limits.MaxRequestBodySize = int.MaxValue;
                });

                webBuilder.UseWebRoot(AspNetSetting.StaticFilesPath);
                webBuilder.UseStartup<Startup>();
            });

            //“¿¿µ◊¢»Î≈‰÷√
            ServiceLocator serviceLocator = new ServiceLocator();
            hostBuilder.UseServiceProviderFactory(serviceLocator);

            IHost host = hostBuilder.Build();
            host.Run();
        }
    }
}
