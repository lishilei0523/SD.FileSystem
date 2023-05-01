using Microsoft.Owin;
using SD.FileSystem.AppService.Host;

// OWIN启动器
[assembly: OwinStartup(typeof(Startup))]
