using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using SD.Toolkits.AspNet;
using System;
using System.IO;

namespace SD.FileSystem.WebClient
{
    /// <summary>
    /// 应用程序启动器
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 配置应用程序
        /// </summary>
        public void Configure(IApplicationBuilder appBuilder)
        {
            //配置服务器
            string staticFilesPath = Path.IsPathRooted(AspNetSetting.StaticFilesPath)
                ? AspNetSetting.StaticFilesPath
                : Path.Combine(AppContext.BaseDirectory, AspNetSetting.StaticFilesPath);
            Directory.CreateDirectory(staticFilesPath);
            StaticFileOptions staticFileOptions = new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFilesPath)
            };
            appBuilder.UseStaticFiles(staticFileOptions);
        }
    }
}
