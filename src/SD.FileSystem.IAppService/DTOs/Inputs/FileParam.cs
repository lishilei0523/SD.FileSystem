using System;
using System.Runtime.Serialization;

namespace SD.FileSystem.IAppService.DTOs.Inputs
{
    /// <summary>
    /// 文件参数模型
    /// </summary>
    [DataContract(Name = "http://SD.FileSystem.IAppService.DTOs.Inputs")]
    public struct FileParam
    {
        /// <summary>
        /// 文件Id
        /// </summary>
        [DataMember]
        public Guid fileId;

        /// <summary>
        /// 文件名
        /// </summary>
        [DataMember]
        public string fileName;

        /// <summary>
        /// 扩展名
        /// </summary>
        [DataMember]
        public string extensionName;

        /// <summary>
        /// 文件大小
        /// </summary>
        [DataMember]
        public long size;

        /// <summary>
        /// 哈希值
        /// </summary>
        [DataMember]
        public string hashValue;

        /// <summary>
        /// 相对路径
        /// </summary>
        [DataMember]
        public string relativePath;

        /// <summary>
        /// 绝对路径
        /// </summary>
        [DataMember]
        public string absolutePath;

        /// <summary>
        /// 主机名称
        /// </summary>
        [DataMember]
        public string hostName;

        /// <summary>
        /// 链接地址
        /// </summary>
        [DataMember]
        public string url;

        /// <summary>
        /// 上传日期
        /// </summary>
        [DataMember]
        public DateTime uploadedDate;

        /// <summary>
        /// 用途
        /// </summary>
        [DataMember]
        public string use;

        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public string description;
    }
}
