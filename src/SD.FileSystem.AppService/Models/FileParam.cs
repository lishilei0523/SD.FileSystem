using System;
using System.Runtime.Serialization;

namespace SD.FileSystem.AppService.Models
{
    /// <summary>
    /// 文件参数模型
    /// </summary>
    [DataContract]
    public struct FileParam
    {
        /// <summary>
        /// 文件Id
        /// </summary>
        [DataMember]
        public Guid FileId;

        /// <summary>
        /// 文件名
        /// </summary>
        [DataMember]
        public string FileName;

        /// <summary>
        /// 扩展名
        /// </summary>
        [DataMember]
        public string ExtensionName;

        /// <summary>
        /// 文件大小
        /// </summary>
        [DataMember]
        public long Size;

        /// <summary>
        /// 哈希值
        /// </summary>
        [DataMember]
        public string HashValue;

        /// <summary>
        /// 相对路径
        /// </summary>
        [DataMember]
        public string RelativePath;

        /// <summary>
        /// 绝对路径
        /// </summary>
        [DataMember]
        public string AbsolutePath;

        /// <summary>
        /// 主机名称
        /// </summary>
        [DataMember]
        public string HostName;

        /// <summary>
        /// 链接地址
        /// </summary>
        [DataMember]
        public string Url;

        /// <summary>
        /// 上传日期
        /// </summary>
        [DataMember]
        public DateTime UploadedDate;

        /// <summary>
        /// 用途
        /// </summary>
        [DataMember]
        public string Use;

        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public string Description;
    }
}
