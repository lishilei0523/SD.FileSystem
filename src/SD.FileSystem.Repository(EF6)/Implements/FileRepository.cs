using SD.FileSystem.Domain.Entities;
using SD.FileSystem.Domain.IRepositories;
using SD.Infrastructure.Repository.EntityFramework;
using SD.Toolkits.EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SD.FileSystem.Repository.Implements
{
    /// <summary>
    /// 文件仓储实现
    /// </summary>
    public class FileRepository : EFAggRootRepositoryProvider<File>, IFileRepository
    {
        #region # 根据哈希值获取默认文件 —— File DefaultByHash(string hashValue)
        /// <summary>
        /// 根据哈希值获取默认文件
        /// </summary>
        /// <param name="hashValue">哈希值</param>
        /// <returns>文件</returns>
        /// <remarks>如果无，则返回null</remarks>
        public File DefaultByHash(string hashValue)
        {
            File file = base.FirstOrDefault(x => x.HashValue == hashValue);

            return file;
        }
        #endregion

        #region # 根据哈希值获取文件列表 —— ICollection<File> FindByHash(string hashValue)
        /// <summary>
        /// 根据哈希值获取文件列表
        /// </summary>
        /// <param name="hashValue">哈希值</param>
        /// <returns>文件列表</returns>
        public ICollection<File> FindByHash(string hashValue)
        {
            IQueryable<File> files = base.Find(x => x.HashValue == hashValue);

            return files.ToList();
        }
        #endregion

        #region # 分页获取文件列表 —— ICollection<File> FindByPage(string keywords, string extensionName...
        /// <summary>
        /// 分页获取文件列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="extensionName">扩展名</param>
        /// <param name="hashValue">哈希值</param>
        /// <param name="uploadedDate">上传日期</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>文件列表</returns>
        public ICollection<File> FindByPage(string keywords, string extensionName, string hashValue, DateTime? uploadedDate, DateTime? startTime, DateTime? endTime, int pageIndex, int pageSize, out int rowCount, out int pageCount)
        {
            QueryBuilder<File> queryBuilder = QueryBuilder<File>.Affirm();
            if (!string.IsNullOrWhiteSpace(keywords))
            {
                queryBuilder.And(x => x.Keywords.Contains(keywords));
            }
            if (!string.IsNullOrWhiteSpace(extensionName))
            {
                queryBuilder.And(x => x.ExtensionName == extensionName);
            }
            if (!string.IsNullOrWhiteSpace(hashValue))
            {
                queryBuilder.And(x => x.HashValue == hashValue);
            }
            if (uploadedDate.HasValue)
            {
                DateTime uploadedDate_ = uploadedDate.Value.Date;
                queryBuilder.And(x => x.UploadedDate == uploadedDate_);
            }
            if (startTime.HasValue)
            {
                DateTime startTime_ = startTime.Value.Date;
                queryBuilder.And(x => x.AddedTime >= startTime_);
            }
            if (endTime.HasValue)
            {
                DateTime endTime_ = endTime.Value.Date;
                queryBuilder.And(x => x.AddedTime <= endTime_);
            }

            Expression<Func<File, bool>> condition = queryBuilder.Build();
            IQueryable<File> files = base.FindByPage(condition, pageIndex, pageSize, out rowCount, out pageCount);

            return files.ToList();
        }
        #endregion

        #region # 给定哈希的文件数量 —— int CountByHash(string hashValue)
        /// <summary>
        /// 给定哈希的文件数量
        /// </summary>
        /// <param name="hashValue">哈希</param>
        /// <returns>文件数量</returns>
        public int CountByHash(string hashValue)
        {
            return base.Count(x => x.HashValue == hashValue);
        }
        #endregion

        #region # 是否存在哈希 —— bool ExsitsHash(string hashValue
        /// <summary>
        /// 是否存在哈希
        /// </summary>
        /// <param name="hashValue">哈希值</param>
        /// <returns>是否存在</returns>
        public bool ExsitsHash(string hashValue)
        {
            return base.Exists(x => x.HashValue == hashValue);
        }
        #endregion
    }
}
