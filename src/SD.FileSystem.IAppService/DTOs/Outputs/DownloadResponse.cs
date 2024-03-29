﻿using System.IO;
using System.ServiceModel;

namespace SD.FileSystem.IAppService.DTOs.Outputs
{
    /// <summary>
    /// 下载响应
    /// </summary>
    [MessageContract]
    public class DownloadResponse
    {
        #region 文件名称 —— string FileName
        /// <summary>
        /// 文件名称
        /// </summary>
        [MessageHeader]
        public string FileName { get; set; }
        #endregion

        #region 文件大小 —— long Size
        /// <summary>
        /// 文件大小
        /// </summary>
        [MessageHeader]
        public long Size { get; set; }
        #endregion

        #region 流数据 —— Stream Datas
        /// <summary>
        /// 流数据
        /// </summary>
        [MessageBodyMember]
        public Stream Datas { get; set; }
        #endregion
    }
}
