using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using SD.Toolkits.AspNet;
using System;
using System.IO;

namespace SD.FileSystem.WebClient
{
    /// <summary>
    /// OWIN启动器
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 配置应用程序
        /// </summary>
        public void Configuration(IAppBuilder appBuilder)
        {
            //配置Web服务器
            string staticFilesPath = Path.IsPathRooted(AspNetSetting.StaticFilesPath)
                ? AspNetSetting.StaticFilesPath
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AspNetSetting.StaticFilesPath);
            Directory.CreateDirectory(staticFilesPath);
            StaticFileOptions staticFileOptions = new StaticFileOptions
            {
                FileSystem = new PhysicalFileSystem(staticFilesPath)
            };
            appBuilder.UseStaticFiles(staticFileOptions);
        }
    }
}
