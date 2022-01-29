using System;
using System.ServiceModel;

namespace SD.FileSystem.IAppService.DTOs.Inputs
{
    /// <summary>
    /// 下载请求
    /// </summary>
    [MessageContract]
    public class DownloadRequest
    {
        #region 文件Id —— Guid FileId
        /// <summary>
        /// 文件Id
        /// </summary>
        [MessageHeader]
        public Guid FileId { get; set; }
        #endregion
    }
}
