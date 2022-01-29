using Microsoft.Owin.Hosting;
using SD.FileSystem.AppService.Implements;
using SD.Toolkits.AspNet;
using System;
using System.ServiceModel;

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
        /// 文件管理服务契约主机
        /// </summary>
        private readonly ServiceHost _fileContractHost;

        /// <summary>
        /// 文件上传/下载服务契约主机
        /// </summary>
        private readonly ServiceHost _loadContractHost;

        /// <summary>
        /// 构造器
        /// </summary>
        public ServiceLauncher()
        {
            this._fileContractHost = new ServiceHost(typeof(FileContract));
            this._loadContractHost = new ServiceHost(typeof(LoadContract));
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            //启动文件服务
            StartOptions startOptions = new StartOptions();
            foreach (string url in AspNetSetting.OwinUrls)
            {
                Console.WriteLine($"Listening: {url}");
                startOptions.Urls.Add(url);
            }
            this._webApp = WebApp.Start<Startup>(startOptions);

            //启动WCF服务
            this._fileContractHost.Open();
            this._loadContractHost.Open();

            Console.WriteLine("服务已启动...");
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            //关闭文件服务 
            this._webApp.Dispose();

            //关闭WCF服务
            this._fileContractHost.Close();
            this._loadContractHost.Close();

            Console.WriteLine("服务已关闭...");
        }
    }
}
