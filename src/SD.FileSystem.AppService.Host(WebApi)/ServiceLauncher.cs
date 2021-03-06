using Microsoft.Owin.Hosting;
using SD.Toolkits.AspNet;
using System;

namespace SD.FileSystem.AppService.Host
{
    /// <summary>
    /// 服务启动器
    /// </summary>
    public class ServiceLauncher
    {
        /// <summary>
        /// Web应用程序
        /// </summary>
        private IDisposable _webApp;

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            StartOptions startOptions = new StartOptions();
            foreach (string url in AspNetSetting.OwinUrls)
            {
                Console.WriteLine($"Listening: {url}");
                startOptions.Urls.Add(url);
            }

            //开启服务
            this._webApp = WebApp.Start<Startup>(startOptions);

            Console.WriteLine("服务已启动...");
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            //关闭服务 
            this._webApp.Dispose();

            Console.WriteLine("服务已关闭...");
        }
    }
}
