﻿using SD.FileSystem.AppService.Implements;
using System;
using System.ServiceModel;

namespace SD.FileSystem.AppService.Host
{
    /// <summary>
    /// 服务启动器
    /// </summary>
    public class ServiceLauncher
    {
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
            this._loadContractHost.Open();

            Console.WriteLine("服务已启动...");
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            this._loadContractHost.Close();

            Console.WriteLine("服务已关闭...");
        }
    }
}
