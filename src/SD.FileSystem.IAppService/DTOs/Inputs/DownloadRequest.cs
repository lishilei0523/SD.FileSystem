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
        #region 构造器

        /// <summary>
        /// 无参构造器
        /// </summary>
        public DownloadRequest() { }

        /// <summary>
        /// 创建下载请求构造器
        /// </summary>
        /// <param name="fileId">文件Id</param>
        public DownloadRequest(Guid fileId)
            : this()
        {
            this.FileId = fileId;
        }

        #endregion

        #region 文件Id —— Guid FileId
        /// <summary>
        /// 文件Id
        /// </summary>
        [MessageHeader]
        public Guid FileId { get; set; }
        #endregion
    }
}
