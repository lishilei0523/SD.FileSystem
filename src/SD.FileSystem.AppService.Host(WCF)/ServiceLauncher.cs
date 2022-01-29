using SD.FileSystem.AppService.Implements;
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
        /// 文件上传/下载服务契约主机
        /// </summary>
        private readonly ServiceHost _loadContractHost;

        /// <summary>
        /// 构造器
        /// </summary>
        public ServiceLauncher()
        {
            this._loadContractHost = new ServiceHost(typeof(LoadContract));
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            //启动WCF服务
            this._loadContractHost.Open();

            Console.WriteLine("服务已启动...");
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            //关闭WCF服务
            this._loadContractHost.Close();

            Console.WriteLine("服务已关闭...");
        }
    }
}
