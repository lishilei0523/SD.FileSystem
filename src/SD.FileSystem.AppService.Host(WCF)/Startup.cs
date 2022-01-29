using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using SD.Toolkits.AspNet;
using System;
using System.IO;

namespace SD.FileSystem.AppService.Host
{
    /// <summary>
    /// OWIN启动器
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 配置应用程序
        /// </summary>
        /// <param name="appBuilder">应用程序建造者</param>
        public void Configuration(IAppBuilder appBuilder)
        {
            //配置文件服务器
            string fileServerPath = Path.IsPathRooted(AspNetSetting.FileServerPath)
                ? AspNetSetting.FileServerPath
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AspNetSetting.FileServerPath);
            Directory.CreateDirectory(fileServerPath);
            FileServerOptions fileServerOptions = new FileServerOptions
            {
                FileSystem = new PhysicalFileSystem(fileServerPath),
                EnableDirectoryBrowsing = true
            };
            appBuilder.UseFileServer(fileServerOptions);
        }
    }
}
