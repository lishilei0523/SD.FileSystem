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

        #region 扩展名 —— string ExtensionName
        /// <summary>
        /// 扩展名
        /// </summary>
        [MessageHeader]
        public string ExtensionName { get; set; }
        #endregion

        #region 文件大小 —— long Size
        /// <summary>
        /// 文件大小
        /// </summary>
        [MessageHeader]
        public long Size { get; set; }
        #endregion

        #region 哈希值 —— string HashValue
        /// <summary>
        /// 哈希值
        /// </summary>
        [MessageHeader]
        public string HashValue { get; set; }
        #endregion

        #region 相对路径 —— string RelativePath
        /// <summary>
        /// 相对路径
        /// </summary>
        [MessageHeader]
        public string RelativePath { get; set; }
        #endregion

        #region 绝对路径 —— string AbsolutePath
        /// <summary>
        /// 绝对路径
        /// </summary>
        [MessageHeader]
        public string AbsolutePath { get; set; }
        #endregion

        #region 主机名称 —— string HostName
        /// <summary>
        /// 主机名称
        /// </summary>
        [MessageHeader]
        public string HostName { get; set; }
        #endregion

        #region 链接地址 —— string Url
        /// <summary>
        /// 链接地址
        /// </summary>
        [MessageHeader]
        public string Url { get; set; }
        #endregion

        #region 上传日期 —— DateTime UploadedDate
        /// <summary>
        /// 上传日期
        /// </summary>
        [MessageHeader]
        public DateTime UploadedDate { get; set; }
        #endregion

        #region 用途 —— string Use
        /// <summary>
        /// 用途
        /// </summary>
        [MessageHeader]
        public string Use { get; set; }
        #endregion

        #region 描述 —— string Description
        /// <summary>
        /// 描述
        /// </summary>
        [MessageHeader]
        public string Description { get; set; }
        #endregion
    }
}
