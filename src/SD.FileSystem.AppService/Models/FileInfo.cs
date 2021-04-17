using SD.Infrastructure.DTOBase;
using System;
using System.Runtime.Serialization;

namespace SD.FileSystem.AppService.Models
{
    /// <summary>
    /// 文件数据传输对象
    /// </summary>
    [DataContract]
    public class FileInfo : BaseDTO
    {
        #region 扩展名 —— string ExtensionName
        /// <summary>
        /// 扩展名
        /// </summary>
        [DataMember]
        public string ExtensionName { get; set; }
        #endregion

        #region 文件大小 —— long Size
        /// <summary>
        /// 文件大小
        /// </summary>
        [DataMember]
        public long Size { get; set; }
        #endregion

        #region 相对路径 —— string RelativePath
        /// <summary>
        /// 相对路径
        /// </summary>
        [DataMember]
        public string RelativePath { get; set; }
        #endregion

        #region 绝对路径 —— string AbsolutePath
        /// <summary>
        /// 绝对路径
        /// </summary>
        [DataMember]
        public string AbsolutePath { get; set; }
        #endregion

        #region 主机名称 —— string HostName
        /// <summary>
        /// 主机名称
        /// </summary>
        [DataMember]
        public string HostName { get; set; }
        #endregion

        #region 链接地址 —— string Url
        /// <summary>
        /// 链接地址
        /// </summary>
        [DataMember]
        public string Url { get; set; }
        #endregion

        #region 哈希值 —— string HashValue
        /// <summary>
        /// 哈希值
        /// </summary>
        [DataMember]
        public string HashValue { get; set; }
        #endregion

        #region 上传日期 —— DateTime UploadedDate
        /// <summary>
        /// 上传日期
        /// </summary>
        [DataMember]
        public DateTime UploadedDate { get; set; }
        #endregion

        #region 用途 —— string Use
        /// <summary>
        /// 用途
        /// </summary>
        [DataMember]
        public string Use { get; set; }
        #endregion

        #region 描述 —— string Description
        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }
        #endregion
    }
}
