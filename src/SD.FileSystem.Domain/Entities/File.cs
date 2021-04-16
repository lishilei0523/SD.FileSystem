using SD.Infrastructure.EntityBase;
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
        /// <param name="storagePath">存储路径</param>
        /// <param name="url">链接地址</param>
        /// <param name="use">用途</param>
        /// <param name="description">描述</param>
        public File(string fileName, string extensionName, long size, string storagePath, string url, string use, string description)
            : this()
        {
            base.Name = fileName;
            this.ExtensionName = extensionName;
            this.Size = size;
            this.StoragePath = storagePath;
            this.Url = url;
            this.Use = use;
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

        #region 存储路径 —— string StoragePath
        /// <summary>
        /// 存储路径
        /// </summary>
        public string StoragePath { get; private set; }
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

        #region 描述 —— string Description
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; private set; }
        #endregion

        #endregion

        #region # 方法

        #region 修改文件 —— void UpdateInfo(string fileName...
        /// <summary>
        /// 修改文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="extensionName">扩展名</param>
        /// <param name="size">文件大小</param>
        /// <param name="storagePath">存储路径</param>
        /// <param name="url">链接地址</param>
        /// <param name="use">用途</param>
        /// <param name="description">描述</param>
        public void UpdateInfo(string fileName, string extensionName, long size, string storagePath, string url, string use, string description)
        {
            base.Name = fileName;
            this.ExtensionName = extensionName;
            this.Size = size;
            this.StoragePath = storagePath;
            this.Url = url;
            this.Use = use;
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
