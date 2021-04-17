using SD.Infrastructure.EntityBase;
using System;
using System.Text;

namespace SD.FileSystem.Domain.Entities
{
    /// <summary>
    /// 文件
    /// </summary>
    public class File : AggregateRootEntity
    {
        #region # 构造器

        #region 00.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected File() { }
        #endregion

        #region 01.创建文件构造器
        /// <summary>
        /// 创建文件构造器
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="extensionName">扩展名</param>
        /// <param name="size">文件大小</param>
        /// <param name="use">用途</param>
        /// <param name="uploadedDate">上传日期</param>
        /// <param name="description">描述</param>
        public File(string fileName, string extensionName, long size, string use, DateTime uploadedDate, string description)
            : this()
        {
            base.Number = $"{base.Id}{extensionName}";
            base.Name = fileName;
            this.ExtensionName = extensionName;
            this.Size = size;
            this.Use = use;
            this.UploadedDate = uploadedDate.Date;
            this.Description = description;

            //初始化关键字
            this.InitKeywords();
        }
        #endregion

        #endregion

        #region # 属性

        #region 扩展名 —— string ExtensionName
        /// <summary>
        /// 扩展名
        /// </summary>
        public string ExtensionName { get; private set; }
        #endregion

        #region 文件大小 —— long Size
        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get; private set; }
        #endregion

        #region 相对路径 —— string RelativePath
        /// <summary>
        /// 相对路径
        /// </summary>
        public string RelativePath { get; private set; }
        #endregion

        #region 绝对路径 —— string AbsolutePath
        /// <summary>
        /// 绝对路径
        /// </summary>
        public string AbsolutePath { get; private set; }
        #endregion

        #region 主机名称 —— string HostName
        /// <summary>
        /// 主机名称
        /// </summary>
        public string HostName { get; private set; }
        #endregion

        #region 链接地址 —— string Url
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; private set; }
        #endregion

        #region 用途 —— string Use
        /// <summary>
        /// 用途
        /// </summary>
        public string Use { get; private set; }
        #endregion

        #region 上传日期 —— DateTime UploadedDate
        /// <summary>
        /// 上传日期
        /// </summary>
        public DateTime UploadedDate { get; private set; }
        #endregion

        #region 描述 —— string Description
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; private set; }
        #endregion

        #endregion

        #region # 方法

        #region 保存文件 —— void Save(string relativePath...
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="absolutePath">绝对路径</param>
        /// <param name="hostName">主机名称</param>
        /// <param name="url">链接地址</param>
        public void Save(string relativePath, string absolutePath, string hostName, string url)
        {
            this.RelativePath = relativePath;
            this.AbsolutePath = absolutePath;
            this.HostName = hostName;
            this.Url = url;

            //初始化关键字
            this.InitKeywords();
        }
        #endregion

        #region 修改文件 —— void UpdateInfo(string fileName...
        /// <summary>
        /// 修改文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="extensionName">扩展名</param>
        /// <param name="size">文件大小</param>
        /// <param name="relativePath">相对路径</param>
        /// <param name="absolutePath">绝对路径</param>
        /// <param name="hostName">主机名称</param>
        /// <param name="url">链接地址</param>
        /// <param name="use">用途</param>
        /// <param name="uploadedDate">上传日期</param>
        /// <param name="description">描述</param>
        public void UpdateInfo(string fileName, string extensionName, long size, string relativePath, string absolutePath, string hostName, string url, string use, DateTime uploadedDate, string description)
        {
            base.Number = $"{base.Id}{extensionName}";
            base.Name = fileName;
            this.ExtensionName = extensionName;
            this.Size = size;
            this.RelativePath = relativePath;
            this.AbsolutePath = absolutePath;
            this.HostName = hostName;
            this.Url = url;
            this.Use = use;
            this.UploadedDate = uploadedDate.Date;
            this.Description = description;

            //初始化关键字
            this.InitKeywords();
        }
        #endregion

        #region 初始化关键字 —— void InitKeywords()
        /// <summary>
        /// 初始化关键字
        /// </summary>
        private void InitKeywords()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(base.Name);
            builder.Append(this.Use);
            builder.Append(this.Description);

            base.SetKeywords(builder.ToString());
        }
        #endregion

        #endregion
    }
}
