using System.IO;
using System.ServiceModel;

namespace SD.FileSystem.IAppService.DTOs.Inputs
{
    /// <summary>
    /// 上传请求
    /// </summary>
    [MessageContract]
    public class UploadRequest
    {
        #region 构造器

        /// <summary>
        /// 无参构造器
        /// </summary>
        public UploadRequest() { }

        /// <summary>
        /// 创建上传请求构造器
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="datas">流数据</param>
        /// <param name="use">用途</param>
        /// <param name="description">描述</param>
        public UploadRequest(string fileName, Stream datas, string use = null, string description = null)
            : this()
        {
            this.FileName = fileName;
            this.Use = use;
            this.Description = description;
            this.Datas = datas;
        }

        #endregion

        #region 文件名称 —— string FileName
        /// <summary>
        /// 文件名称
        /// </summary>
        [MessageHeader]
        public string FileName { get; set; }
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

        #region 流数据 —— Stream Datas
        /// <summary>
        /// 流数据
        /// </summary>
        [MessageBodyMember]
        public Stream Datas { get; set; }
        #endregion
    }
}
