using System;
using System.ServiceModel;

namespace SD.FileSystem.IAppService.DTOs.Outputs
{
    /// <summary>
    /// 上传响应
    /// </summary>
    [MessageContract]
    public class UploadResponse
    {
        #region 文件Id —— Guid FileId
        /// <summary>
        /// 文件Id
        /// </summary>
        [MessageHeader]
        public Guid FileId { get; set; }
        #endregion

        #region 文件名称 —— string FileName
        /// <summary>
        /// 文件名称
        /// </summary>
        [MessageHeader]
        public string FileName { get; set; }
        #endregion

        #region 哈希值 —— string HashValue
        /// <summary>
        /// 哈希值
        /// </summary>
        [MessageHeader]
        public string HashValue { get; set; }
        #endregion

        #region 链接地址 —— string Url
        /// <summary>
        /// 链接地址
        /// </summary>
        [MessageHeader]
        public string Url { get; set; }
        #endregion
    }
}
